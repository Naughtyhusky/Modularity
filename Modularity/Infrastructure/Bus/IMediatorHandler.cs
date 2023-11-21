using Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Bus
{
    public interface IMediatorHandler
    {
      
        Task<CommandResponse> SendCommandAsync<TCommand>(TCommand command) where TCommand : CommandBase;
      
        Task PublishEventAsync<TEvent>(TEvent @event) where TEvent : EventBase;
    }
}
