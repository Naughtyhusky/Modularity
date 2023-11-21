using Infrastructure.Common;
using Infrastructure.Interfaces;
using Modules.User.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.User.Services
{
    public interface IUserService : ITransientDependency
    {
        Task<QueryResponse<List<UserDto>>> GetUsersAsync(string? keyword);

        Task<QueryResponse<TokenDto>> LoginAsync(string account, string password);

        Task<QueryResponse<AuthInfo>> GetAuthAsync();
    }
}
