using FreeRedis;
using Infrastructure.Bus;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public class ServerBase
    {
        private readonly ICueerntUserProvider _cueerntUserProvider;

        private readonly IMediatorHandler _mediatorHandler;

        public readonly CurrentUser _user;

        public ServerBase(ICueerntUserProvider cueerntUserProvider, IMediatorHandler mediatorHandler)
        {
            _cueerntUserProvider = cueerntUserProvider;
            _mediatorHandler = mediatorHandler;

            _user = _cueerntUserProvider.GetCurrentUser();
        }

       
        public async Task PublishEventAsync<T>(T @event) where T : EventBase
        {
            await _mediatorHandler.PublishEventAsync(@event);
        }

        public async Task<CommandResponse> SendCommandAsync<T>(T command) where T : CommandBase
        {
            return await _mediatorHandler.SendCommandAsync(command);
        }
    }
}
