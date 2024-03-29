﻿using sun.Core.Domains;
using sun.EntityFrameworkCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Services
{
    public interface IScheduleTaskService: IServiceBase<ScheduleTask>
    {
        /// <summary>
        /// 初始化定时任务
        /// </summary>
        /// <returns></returns>
        Task InitializeAsync();
    }
}
