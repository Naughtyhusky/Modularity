using Infrastructure.Common;
using Infrastructure.DataBase;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Bus
{
    public class TransactionBehavior<TRequest, TResponse>(DiamondhuskyDbContext context, IMediatorHandler handler, ILogger<TransactionBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : CommandResponse
    {

        private readonly DiamondhuskyDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        private readonly IMediatorHandler _mediatorHandler = handler ?? throw new ArgumentNullException(nameof(handler));

        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = default(TResponse);

            try
            {
                if (_context.HasActiveTransaction)
                {
                    return await next();
                }

                var strategy = _context.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    using var transaction = await _context.BeginTransactionAsync();
                    response = await next();

                    if (response.Status)
                    {
                        var events = GetAllDomianEvents();

                        await _context.CommitTransactionAsync(transaction);

                        await PublishDomianEventsAsync(events);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(exception: ex, "sql 执行异常!");

                response = (TResponse)CommandResponse.Fail("操作失败", 999);

                ClearDomianEvents();

            }

            return response!;
        }


        /// <summary>
        /// 获取所有的领域事件
        /// </summary>
        /// <returns></returns>
        private List<EventBase> GetAllDomianEvents()
        {
            var domainEntities = _context.ChangeTracker
              .Entries<EntityBase>()
              .Where(x => x.Entity.GetDomainEvents() != null && x.Entity.GetDomainEvents()!.Count != 0);

            var domainEvents = domainEntities.SelectMany(x => x.Entity.GetDomainEvents()!).ToList();

            domainEntities?.ToList().ForEach(entity => entity.Entity.ClearDomainEvents());

            return domainEvents;
        }

        /// <summary>
        /// 清空领域事件
        /// </summary>
        private void ClearDomianEvents()
        {
            var domainEntities = _context.ChangeTracker
             .Entries<EntityBase>()
             .Where(x => x.Entity.GetDomainEvents() != null && x.Entity.GetDomainEvents()!.Count != 0);

            domainEntities?.ToList().ForEach(entity => entity.Entity.ClearDomainEvents());
        }


        /// <summary>
        /// 发布领域事件
        /// </summary>
        /// <param name="events"></param>
        /// <returns></returns>
        public async Task PublishDomianEventsAsync(IEnumerable<EventBase> events)
        {
            if (events != null && events.Any())
            {
                foreach (var @event in events)
                {
                    await _mediatorHandler.PublishEventAsync(@event);
                }
            }
        }
    }
}
