using FreeRedis;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CueerntUserProvider(IHttpContextAccessor httpContextAccessor, RedisClient redisClient, ILogger<CueerntUserProvider> logger) : ICueerntUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        private readonly RedisClient _redisClient = redisClient;

        private ILogger<CueerntUserProvider> _logger = logger;

        public CurrentUser GetCurrentUser()
        {
            var id = GetUserId();
            return new()
            {
                Id = id,
                Name = GetUserName(id),
                TraceIdentifier = _httpContextAccessor.HttpContext?.TraceIdentifier
            };
        }

        private ClaimsPrincipal GetCurClaimsPrincipal()
        {
            return _httpContextAccessor.HttpContext!.User;
        }

        /// <summary>
        /// 获取用户ID
        /// </summary>
        /// <returns></returns>
        private long GetUserId()
        {
            var tmp = GetCurClaimsPrincipal().FindFirst(item => item.Type == "id")?.Value;

            return string.IsNullOrWhiteSpace(tmp) ? 0 : long.Parse(tmp);
        }

        private string GetUserName(long userId)
        {
            try
            {
                if (userId <= 0)
                {
                    return string.Empty;
                }

                var key = $"user:{userId}";

                if (_redisClient.Exists(key))
                {
                    var value = _redisClient.Get(key);

                    var user = JsonConvert.DeserializeObject<CacheUserModel>(value);

                    return user?.Name!;
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "an exception was throwed during GetUserName");

                return string.Empty;
            }

        }
    }
}
