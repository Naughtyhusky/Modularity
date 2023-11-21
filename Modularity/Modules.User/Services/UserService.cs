using AutoMapper;
using FreeRedis;
using Infrastructure.Bus;
using Infrastructure.Common;
using Infrastructure.Data;
using Infrastructure.DataBase;
using Infrastructure.Extensions;
using Infrastructure.Interfaces;
using Infrastructure.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modules.User.CommandHandlers;
using Modules.User.Dto;
using Newtonsoft.Json;
using Share.Commands;
using Share.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Modules.User.Services
{
    public class UserService(IRepository<Entities.User> userRepository, RedisClient redisClient, ILogger<UserService> logger, IMapper mapper, ICueerntUserProvider cueerntUserProvider, IMediatorHandler mediatorHandler) : ServerBase(cueerntUserProvider, mediatorHandler), IUserService
    {
        private readonly IRepository<Entities.User> _userRepository = userRepository;

        private readonly ILogger<UserService> _logger = logger;

        private readonly IMapper _mapper = mapper;

        private readonly RedisClient _redisClient = redisClient;



        public async Task<QueryResponse<List<UserDto>>> GetUsersAsync(string? keyword)
        {
            var users = await _userRepository.NoTrackingQuery().WhereIf(!string.IsNullOrWhiteSpace(keyword), t => t.Name.Contains(keyword!)).ToListAsync();

            return QueryResponse<List<UserDto>>.Instance(data: _mapper.Map<List<UserDto>>(users));
        }

        public async Task<QueryResponse<TokenDto>> LoginAsync(string account, string password)
        {
            var user = await _userRepository.NoTrackingQuery()
                       .FirstOrDefaultAsync(t => t.Account == account);

            if (user is null)
            {
                return QueryResponse<TokenDto>.Instance(120, "用户不存在");
            }

            if (!user.VerifyPassword(password))
            {
                return QueryResponse<TokenDto>.Instance(121, "密码错误");
            }

            if (user.State == Enums.State.Disable)
            {
                return QueryResponse<TokenDto>.Instance(122, "用户账号已被禁用");
            }

            var token = JwtTokenProvider.GenerateToken(user.Id);

            CacheUserModel userModel = new() { Name = user.Name, Id = user.Id };

            _redisClient.Set($"user:{user.Id}", JsonConvert.SerializeObject(userModel));

            return QueryResponse<TokenDto>.Instance(data: new TokenDto { Authorization = $"Bearer {token}" });
        }

        public async Task<QueryResponse<AuthInfo>> GetAuthAsync()
        {
            var user = await _userRepository.NoTrackingQuery()
                     .FirstOrDefaultAsync(t => t.Id == _user.Id);

            if (user is null)
            {
                return QueryResponse<AuthInfo>.Instance(120, "用户不存在");
            }

            var queryMenuIdsCmd = new QueryRoleMenusCmd(user.RoleId);

            var res = await SendCommandAsync(queryMenuIdsCmd);

            if (!res.Status)
            {
                throw new Exception($"数据异常，未查询到id为{user.RoleId}的角色信息");
            }

            if (res.Data is not IEnumerable<long> menuIds || !menuIds.Any())
            {
                throw new Exception($"数据异常，id为{user.RoleId}的角色没有任何菜单权限");
            }

            var queryMenusCmd = new QueryMenuInfoCmd(menuIds!);

            var result= await SendCommandAsync(queryMenusCmd);

            if (result.Data is not IEnumerable<MenuItem> menuItems || !menuItems.Any())
            {
                throw new Exception($"数据异常，id为{user.RoleId}的角色没有查询到菜单数据");
            }

            var menuTree = menuItems.GenerateTree(x => x.Flag, x => x.Parent, x => x.Order);


            var userDto = _mapper.Map<UserDto>(user);

            return QueryResponse<AuthInfo>.Instance(data: new AuthInfo(userDto, menuTree));
        }

    }
}
