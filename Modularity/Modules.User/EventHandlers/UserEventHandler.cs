using Infrastructure.DataBase;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Modules.User.CommandHandlers;
using Modules.User.Events;
using Share.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.User.EventHandlers
{
    public class UserEventHandler(IServiceProvider provider, ILogger<UserEventHandler> logger) : INotificationHandler<RoleNameChangedEvent>,
                                   INotificationHandler<UserBannedEvent>

    {
        private readonly IServiceProvider _serviceProvider = provider;

        private readonly ILogger<UserEventHandler> _logger = logger;

        public async Task Handle(RoleNameChangedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Excute event:{event}", notification.SerializeEvent());

            var scope = _serviceProvider.CreateScope();

            var _userRepository = scope.ServiceProvider.GetService<IRepository<Entities.User>>();

            if (_userRepository is null)
            {
                _logger.LogError("excute event abort! event:{event}", notification.SerializeEvent());
                return;
            }

            await _userRepository.Query().Where(t => t.RoleId == notification.RoleId).ExecuteUpdateAsync(s => s.SetProperty(b => b.RoleName, b => notification.RoleName), cancellationToken: cancellationToken);

            await _userRepository.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task Handle(UserBannedEvent notification, CancellationToken cancellationToken)
        {
            /*用户账号被封禁事件，这里可以进行其他逻辑处理，比如同步封禁用户的登录token等*/

            throw new NotImplementedException();
        }
    }
}
