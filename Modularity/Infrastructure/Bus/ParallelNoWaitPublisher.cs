using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Bus
{
    public class ParallelNoWaitPublisher : INotificationPublisher
    {
        public Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification, CancellationToken cancellationToken)
        {
            foreach (var handler in handlerExecutors)
            {
                Task.Run(() => handler.HandlerCallback(notification, cancellationToken), cancellationToken);
            }

            return Task.CompletedTask;
        }
    }
}
