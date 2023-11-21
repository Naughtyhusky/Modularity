using Infrastructure.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Bus
{
    public class MediatorHandler(IMediator mediator) : IMediatorHandler
    {
        private readonly IMediator _mediator = mediator;

        public async Task PublishEventAsync<T>(T @event) where T : EventBase
        {
            await _mediator.Publish(@event);
        }

     
        public async Task<CommandResponse> SendCommandAsync<T>(T command) where T : CommandBase
        {
            return await _mediator.Send(command);
        }
    }
}
