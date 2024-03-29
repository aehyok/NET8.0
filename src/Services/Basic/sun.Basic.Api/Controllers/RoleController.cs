﻿using sun.Basic.Domains;
using sun.Basic.Dtos;
using sun.Basic.Dtos.Create;
using sun.Basic.Dtos.Query;
using sun.Basic.Services;
using sun.Core.Domains;
using sun.Core.Dtos;
using sun.Core.Dtos.Create;
using sun.Core.Dtos.Query;
using sun.Core.Services;
using sun.EntityFrameworkCore.Repository;
using sun.Infrastructure.Enums;
using sun.Infrastructure.Exceptions;
using Ardalis.Specification;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace sun.Basic.Api.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    public class RoleController(IRoleService roleService, IPermissionService permissionService) : BasicControllerBase
    {

        /// <summary>
        /// 获取角色分页数据
        /// </summary>
        /// <param name="platformType">所属平台(传0获取所有)</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("list/{platformType}")]
        public async Task<IPagedList<RoleDto>> GetListAsync(PlatformType platformType,[FromQuery] RoleQueryDto model)
        {
            var roleId = base.CurrentUser.RoleId;
            var spec = Specifications<Role>.Create();
            spec.Query.OrderBy(a => a.Order);

            if(platformType > 0)
            {
                spec.Query.Where(a => a.PlatformType == platformType);
            }
            
            // 当前用户角色不是超级管理员时，不允许查看超级管理员角色
            var role = await roleService.GetByIdAsync(roleId);

            if(role.Code != SystemRoles.ROOT)
            {
                spec.Query.Where(a => a.Code != SystemRoles.ROOT);
            }

            if (!string.IsNullOrEmpty(model.Keyword))
            {
                spec.Query.Search(a => a.Name, $"%{model.Keyword}%").Search(a => a.Code, $"%{model.Keyword}%").Search(a => a.Remark, $"%{model.Keyword}%");
            }

            if (model.IsEnable.HasValue)
            {
                spec.Query.Where(a => a.IsEnable == model.IsEnable.Value);
            }

            return await roleService.GetPagedListAsync<RoleDto>(spec, model.Page, model.Limit);
        }

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Task<RoleDto> GetByIdAsync(long id)
        {
            return roleService.GetByIdAsync<RoleDto>(id);
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<long> PostAsync(CreateRoleDto model)
        {
            var entity = this.Mapper.Map<Role>(model);
            await roleService.InsertAsync(entity);
            return entity.Id;
        }

        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<StatusCodeResult> PutAsync(long id, CreateRoleDto model)
        {
            var entity = await roleService.GetAsync(a => a.Id == id);
            if (entity is null)
            {
                throw new ErrorCodeException(-1, "你要修改的数据不存在");
            }

            entity = this.Mapper.Map(model, entity);

            await roleService.UpdateAsync(entity);

            return Ok();
        }

        /// <summary>
        /// 启用角色
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        [HttpPut("enable/{id}")]
        public async Task<StatusCodeResult> EnableAsync(long id)
        {
            var entity = await roleService.GetAsync(a => a.Id == id);
            if (entity is null)
            {
                throw new ErrorCodeException(-1, "你要启用的数据不存在");
            }

            entity.IsEnable = true;
            await roleService.UpdateAsync(entity);

            return Ok();
        }

        /// <summary>
        /// 禁用角色
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        [HttpPut("disable/{id}")]
        public async Task<StatusCodeResult> DisableAsync(long id)
        {
            var entity = await roleService.GetAsync(a => a.Id == id);
            if (entity is null)
            {
                throw new ErrorCodeException(-1, "你要禁用的数据不存在");
            }

            if (entity.IsSystem) 
            {
                throw new ErrorCodeException(-1, "禁止禁用系统内置角色");
            }

            entity.IsEnable = false;
            await roleService.UpdateAsync(entity);

            return Ok();
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<StatusCodeResult> DeleteAsync(long id)
        {
            var entity = await roleService.GetAsync(a => a.Id == id);
            if (entity is null)
            {
                throw new ErrorCodeException(-1, "你要删除的数据不存在");
            }

            if (entity.IsSystem)
            {
                throw new ErrorCodeException(-1, "禁止删除系统内置角色");
            }

            await roleService.DeleteAsync(entity);
            return Ok();
        }

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("permission")]
        public Task<List<PermissionDto>> GetRoleAsync([FromQuery] PermissionQueryDto model)
        {
            return permissionService.GetRolePermissionAsync(model.RoleId);
        }

        /// <summary>
        /// 修改角色权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="menus">菜单id数组</param>
        /// <returns></returns>
        [HttpPost("permission/{roleId}")]
        public async Task<StatusCodeResult> PostAsync(long roleId, long[] menus)
        {
            ChangeRolePermissionDto dto = new ChangeRolePermissionDto();
            dto.RoleId = roleId;
            dto.Premission = new List<ChangePermissionModel>();
            dto.Premission = menus.Select(a => new ChangePermissionModel
            {
                MenuId = a
            }).ToList();
            await permissionService.ChangeRolePermissionAsync(dto);
            return Ok();
        }
    }
}
