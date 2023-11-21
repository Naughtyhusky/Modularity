using FluentValidation;
using Infrastructure.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Bus
{
    /// <summary>
    /// 管道验证中间件（目前publish模式暂时不支持这个）
    /// 进行Command的参数校验
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class ValidatorBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidatorBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : CommandResponse
    {
        private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> _logger = logger;
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Count != 0)
            {
                var failure = failures.First();

                _ = int.TryParse(failure.ErrorCode, out int errCode);

                return (TResponse)CommandResponse.Fail(failure.ErrorMessage, errCode);
            }

            return await next();
        }     
    }
}
