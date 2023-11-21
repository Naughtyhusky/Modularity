using Infrastructure.Bus;
using Infrastructure.Common;
using Infrastructure.DataBase;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modules.Role.Commands;
using Modules.Role.Entities;
using Share.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Role.CommandHandlers
{
    public class RoleCommandHandler(IRepository<Entities.Role> roleRepository, IRepository<RoleMenus> roleMenusRepository, ILogger<RoleCommandHandler> logger, ICueerntUserProvider cueerntUserProvider, IMediatorHandler mediatorHandler) : ServerBase(cueerntUserProvider, mediatorHandler),
                       IRequestHandler<QueryRoleCmd, CommandResponse>,
                       IRequestHandler<CreateRoleCmd, CommandResponse>,
                       IRequestHandler<ModifyRoleInfoCmd, CommandResponse>,
                       IRequestHandler<DeleteRoleCmd, CommandResponse>,
                       IRequestHandler<RoleMenuBingingCmd, CommandResponse>,
                       IRequestHandler<QueryRoleMenusCmd,CommandResponse>
    {

        private readonly IRepository<Entities.Role> _roleRepository = roleRepository;

        private readonly IRepository<RoleMenus> _roleMenusRepository = roleMenusRepository;

        private readonly ILogger<RoleCommandHandler> _logger = logger;

        public async Task<CommandResponse> Handle(QueryRoleCmd request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.NoTrackingQuery().FirstOrDefaultAsync(t => t.Id == request.RoleId, cancellationToken: cancellationToken);

            if (role is null)
            {
                return CommandResponse.Fail("角色不存在", 201);
            }

            return CommandResponse.Success(role.Name);
        }

        public async Task<CommandResponse> Handle(CreateRoleCmd request, CancellationToken cancellationToken)
        {
            var exists = await CheckRoleNameExistsAsync(request.Name);

            if (exists)
            {
                return CommandResponse.Fail("角色名重复", 202);
            }

            var cmd = new CheckMenuExistsCmd(request.MenuIds);

            var res = await SendCommandAsync(cmd);

            if (res is null || !res.Status)
            {
                return CommandResponse.Fail("菜单数据异常", 203);
            }

            var role = new Entities.Role(request.Name, request.Description, request.Type, _user.Id, _user.Name!);

            await _roleRepository.AddAsync(role);

            return CommandResponse.Success();
        }

        public async Task<CommandResponse> Handle(ModifyRoleInfoCmd request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.Query().FirstOrDefaultAsync(t => t.Id == request.RoleId, cancellationToken: cancellationToken);

            if (role is null)
            {
                return CommandResponse.Fail("角色不存在", 201);
            }

            var exists = await CheckRoleNameExistsAsync(request.Name, role.Id);

            if (exists)
            {
                return CommandResponse.Fail("角色名重复", 202);
            }

            role.Modify(request.Name, request.Type, request.Description, _user.Id, _user.Name!);

            return CommandResponse.Success();
        }

        public async Task<CommandResponse> Handle(DeleteRoleCmd request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.Query().Include(x => x.RoleMenus).FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken: cancellationToken);

            if (role is null)
            {
                return CommandResponse.Fail("角色不存在", 201);
            }

            var cmd = new QueryRoleCanBeDeletedCmd(role.Id);

            var res = await SendCommandAsync(cmd);

            if (!res.Status)
            {
                return res;
            }

            role.RoleMenus.Clear();

            await _roleRepository.DeleteAsync(role);

            return CommandResponse.Success();
        }

        public async Task<CommandResponse> Handle(RoleMenuBingingCmd request, CancellationToken cancellationToken)
        {
            var adminRoleIds = await _roleRepository.NoTrackingQuery().Where(t => t.Type == Enums.RoleType.Admin).Select(t => t.Id).ToListAsync(cancellationToken: cancellationToken);

            if (adminRoleIds.Count > 0)
            {
                foreach (var roleId in adminRoleIds)
                {
                    _roleMenusRepository.Add(new RoleMenus(roleId, request.MenuId, _user.Id, _user.Name!));
                }
            }

            return CommandResponse.Success();
        }

        public async Task<CommandResponse> Handle(QueryRoleMenusCmd request, CancellationToken cancellationToken)
        {
            var exists = await _roleRepository.Query().AnyAsync(t => t.Id == request.RoleId, cancellationToken: cancellationToken);

            if (!exists)
            {
                return CommandResponse.Fail("角色不存在", 201);
            }

            var menuIds = await _roleMenusRepository.NoTrackingQuery().Select(t => t.MenuId).Distinct().ToListAsync(cancellationToken: cancellationToken);

            return CommandResponse.Success(data: menuIds);
        }


        /// <summary>
        /// 检查角色名是否存在
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="excludedRoleId">被排除，不参与检查的角色的id</param>
        /// <returns></returns>
        private async Task<bool> CheckRoleNameExistsAsync(string roleName, long excludedRoleId = 0)
        {
            return await _roleRepository.Query().AnyAsync(t => t.Name == roleName && t.Id != excludedRoleId);
        }
    }
}
