﻿using aehyok.Core.Domains;
using aehyok.Core.Services;
using aehyok.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace aehyok.Core.HostedServices
{
    /// <summary>
    /// 应该为所有种子数据设置一个版本号，以避免每次启动的时候执行
    /// </summary>
    public class ExecuteSeedDataService(IServiceScopeFactory scopeFactory,IServiceProvider services,ILogger<ExecuteSeedDataService> logger, IConfiguration configuration) : BackgroundService
    {
        /// <summary>
        /// 当前配置文件的路径
        /// </summary>
        private string currentConfigPath = null;

        /// <summary>
        /// 更新定时任务记录
        /// </summary>  
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateCronTask(SeedDataTask model)
        {
            using var scope = scopeFactory.CreateScope();
            var cronTaskCoreService = scope.ServiceProvider.GetRequiredService<IScheduleTaskCoreService>();

            await cronTaskCoreService.UpdateAsync(model);
        }

        /// <summary>
        /// 获取任务是否需要执行的状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private bool GetExecuteStatus(SeedDataTask model)
        {
            // 为空，则表示数据没有写入在json配置文件中
            if (string.IsNullOrEmpty(currentConfigPath))
            {
                return true;
            }
            else
            {
                // 判断json文件是否保存过，通过最后保存时间来判断
                var file = new FileInfo(currentConfigPath);

                //将日期转换为秒数，读取的文件的日期中的毫秒数位数为7，写入到mysql数据库的位数只能为6
                if ((long)model.LastWriteTime.TimeOfDay.TotalSeconds < (long)file.LastWriteTime.TimeOfDay.TotalSeconds)
                {
                    model.LastWriteTime = file.LastWriteTime;
                    return true;
                }

                // 如果执行失败，则需要重新执行
                if(model.ExecuteStatus == ExecuteStatus.失败)
                {
                    return true;
                }
                return false;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation($"开始执行种子数据更新任务");

            var stopwatch = new Stopwatch();

            stopwatch.Start();
            using var scope = scopeFactory.CreateScope();
            var redisDatabaseProvider = scope.ServiceProvider.GetRequiredService<IRedisService>();

            var seedInstants = scope.ServiceProvider.GetServices<ISeedData>();

            var cronTaskCoreService = scope.ServiceProvider.GetRequiredService<IScheduleTaskCoreService>();

            var list = await cronTaskCoreService.GetListAsync();

            foreach (var seed in seedInstants.OrderBy(a => a.Order))
            {
                var taskName = seed.GetType().FullName;
                currentConfigPath = seed.ConfigPath;
                var taskId = Guid.NewGuid().ToString();
               
                var model = list.FirstOrDefault(item => item.TaskName == taskName);

                try
                {
                    //判断该任务是否启用，并且是否已同步到数据库，如果没有同步，则要写入数据库
                    if (model is not null && !model.IsEnable)
                    {
                        break; //如果任务已经禁用，则不再执行
                    }

                    if(model is null)
                    {
                        model = await cronTaskCoreService.InsertAsync(new SeedDataTask
                        {
                            TaskName = taskName,
                            ConfigPath = currentConfigPath,
                            IsEnable = true
                        });

                    }                    
                    //判断任务是否需要执行任务
                    if (GetExecuteStatus(model))
                    {
                        logger.LogInformation($"开始执行[{taskName}]");

                        if (await redisDatabaseProvider.SetAsync(taskName, taskId, TimeSpan.FromMinutes(5), CSRedis.RedisExistence.Nx))
                        {
                            await seed.ApplyAsync(model);
                            model.ExecuteStatus = ExecuteStatus.成功;
                            model.ExecuteTime = DateTime.Now;
                        }

                        logger.LogInformation($"[{taskName}]执行完成");
                    }

                }
                catch (Exception ex)
                {
                    model.ExecuteStatus = ExecuteStatus.失败;
                    model.ExecuteTime = DateTime.Now;
                    logger.LogError(ex, ex.Message);
                }
                finally
                {
                    await UpdateCronTask(model);
                    await redisDatabaseProvider.DeleteAsync(taskName);
                }
            }

            stopwatch.Stop();
            logger.LogInformation($"种子数据更新任务执行完成，耗时:{stopwatch.ElapsedMilliseconds}ms");
        }
    }
}