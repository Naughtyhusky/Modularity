using Infrastructure.Bus;
using Infrastructure.Common;
using Infrastructure.DataBase;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modules.User.Commands;
using Share.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.User.CommandHandlers
{
    public class UserCommandHandler(IRepository<Entities.User> userRepository, ILogger<UserCommandHandler> logger, ICueerntUserProvider cueerntUserProvider, IMediatorHandler mediatorHandler) : ServerBase(cueerntUserProvider, mediatorHandler),
                                    IRequestHandler<CreateUserCmd, CommandResponse>,
                                    IRequestHandler<ModifyUserCmd, CommandResponse>,
                                    IRequestHandler<ModifyPasswordCmd, CommandResponse>,
                                    IRequestHandler<ModifyStateCmd, CommandResponse>,
                                    IRequestHandler<QueryRoleCanBeDeletedCmd, CommandResponse>

    {
        private readonly IRepository<Entities.User> _userRepository = userRepository;

        private readonly ILogger<UserCommandHandler> _logger = logger;

        /// <summary>
        /// 创建账号
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CommandResponse> Handle(CreateUserCmd request, CancellationToken cancellationToken)
        {
            var exists = await _userRepository.NoTrackingQuery().AnyAsync(t => t.Account == request.Account, cancellationToken: cancellationToken);

            if (exists)
            {
                return CommandResponse.Fail("该账号已经被注册", 101);
            }

            var cmd = new QueryRoleCmd(request.RoleId);

            var res = await SendCommandAsync(cmd);

            if (!res.Status)
            {
                return res;
            }

            var roleName = res.Data!.ToString();

            var user = new Entities.User(request.Account, request.Password, request.Name, request.Gender, request.RoleId, roleName!, _user.Id, _user.Name!);

            await _userRepository.AddAsync(user);

            return CommandResponse.Success();
        }

        /// <summary>
        /// 修改账号信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CommandResponse> Handle(ModifyUserCmd request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Query().FirstOrDefaultAsync(t => t.Id == request.UserId, cancellationToken: cancellationToken);

            if (user is null)
            {
                return CommandResponse.Fail("用户不存在", 102);
            }

            user.ModifyUserInfo(request.Name, request.Gender);

            return CommandResponse.Success();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CommandResponse> Handle(ModifyPasswordCmd request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Query().FirstOrDefaultAsync(t => t.Id == _user.Id, cancellationToken: cancellationToken);

            if (user is null)
            {
                return CommandResponse.Fail("用户不存在", 102);
            }

            if (!user.IsAvailable())
            {
                return CommandResponse.Fail("账号已被禁用", 103);
            }

            if (!user.VerifyPassword(request.Password))
            {
                return CommandResponse.Fail("密码错误", 104);
            }

            user.ModifyPassword(request.NewPassword);

            return CommandResponse.Success();
        }

        /// <summary>
        /// 修改账号状态
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CommandResponse> Handle(ModifyStateCmd request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Query().FirstOrDefaultAsync(t => t.Id == _user.Id, cancellationToken: cancellationToken);

            if (user is null)
            {
                return CommandResponse.Fail("用户不存在", 102);
            }

            user.ChangeState(request.State, _user.Id, _user.Name!);

            return CommandResponse.Success();
        }

        public async Task<CommandResponse> Handle(QueryRoleCanBeDeletedCmd request, CancellationToken cancellationToken)
        {
            var exists = await _userRepository.NoTrackingQuery().AnyAsync(t => t.RoleId == request.RoleId, cancellationToken: cancellationToken);

            return exists ? CommandResponse.Fail("当前角色正在被使用", 104) : CommandResponse.Success();
        }
    }
}
