using AutoMapper;
using Infrastructure.Bus;
using Infrastructure.Common;
using Infrastructure.DataBase;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modules.Resource.Commands;
using Modules.Resource.Entities;
using Share.Commands;
using Share.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Resource.CommandHandlers
{
    public class ResourceCommandHandler(IRepository<Menu> menuRepository, ILogger<ResourceCommandHandler> logger, IMapper mapper, ICueerntUserProvider cueerntUserProvider, IMediatorHandler mediatorHandler) : ServerBase(cueerntUserProvider, mediatorHandler),
                         IRequestHandler<CheckMenuExistsCmd, CommandResponse>,
                         IRequestHandler<CreateMenuCmd, CommandResponse>,
                         IRequestHandler<QueryMenuInfoCmd, CommandResponse>
    {
        private readonly IRepository<Menu> _menuRepository = menuRepository;

        private readonly ILogger<ResourceCommandHandler> _logger = logger;

        private readonly IMapper _mapper = mapper;

        public async Task<CommandResponse> Handle(CheckMenuExistsCmd request, CancellationToken cancellationToken)
        {
            var allMenuIds = await _menuRepository.NoTrackingQuery().Select(x => x.Id).ToListAsync(cancellationToken: cancellationToken);

            var result = request.MenuIds != null && request.MenuIds.Any() && allMenuIds.All(request.MenuIds.Contains);

            return result ? CommandResponse.Success() : CommandResponse.Fail("数据异常");
        }

        public async Task<CommandResponse> Handle(CreateMenuCmd request, CancellationToken cancellationToken)
        {
            var routeExists = await _menuRepository.Query().AnyAsync(t => t.Route == request.Route, cancellationToken: cancellationToken);

            if (routeExists)
            {
                return CommandResponse.Fail("菜单路由已存在", 301);
            }

            if (request.Parent != 0)
            {
                var parentExists = await _menuRepository.Query().AnyAsync(t => t.Flag == request.Parent, cancellationToken: cancellationToken);

                if (!parentExists)
                {
                    return CommandResponse.Fail("上级菜单不存在", 301);
                }
            }

            var menu = new Menu(request.Name, request.Flag, request.Parent, request.Order, request.Route, request.Description, _user.Id, _user.Name!);

            _menuRepository.Add(menu);

            await _menuRepository.SaveChangesAsync();

            var cmd = new RoleMenuBingingCmd(menu.Id); //新增菜单之后，自动加入到管理员角色的权限里

            return await SendCommandAsync(cmd);
        }

        public async Task<CommandResponse> Handle(QueryMenuInfoCmd request, CancellationToken cancellationToken)
        {
            var menus = await _menuRepository.NoTrackingQuery().Where(t => request.MenuIds.Contains(t.Id)).ToListAsync(cancellationToken: cancellationToken);

            return CommandResponse.Success(data: _mapper.Map<IEnumerable<MenuItem>>(menus));
        }
    }
}
