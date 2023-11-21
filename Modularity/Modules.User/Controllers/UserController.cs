using Infrastructure.Bus;
using Infrastructure.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Modules.User.Commands;
using Modules.User.Dto;
using Modules.User.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.User.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController(ILogger<UserController> logger, IUserService userService, IMediatorHandler mediatorHandler) : ControllerBase
    {
        private readonly ILogger<UserController> _logger = logger;

        private readonly IUserService _userService = userService;

        private readonly IMediatorHandler _mediatorHandler = mediatorHandler;


        [Route("create")]
        [HttpPost]
        public async Task<ApiResponseBase> CreateUserAsync(CreateUserCmd cmd)
        {
            var res = await _mediatorHandler.SendCommandAsync(cmd);

            return res.Status ? ApiResponseBase.Success()
                             : ApiResponseBase.Fail(res.Code, res.Message);
        }

        [Route("modifyPwd")]
        [HttpPost]
        public async Task<ApiResponseBase> ModifyPasswordAsync(ModifyPasswordCmd cmd)
        {
            var res = await _mediatorHandler.SendCommandAsync(cmd);

            return res.Status ? ApiResponseBase.Success()
                             : ApiResponseBase.Fail(res.Code, res.Message);
        }

        [Route("modify")]
        [HttpPost]
        public async Task<ApiResponseBase> ModifyUserAsync(ModifyUserCmd cmd)
        {
            var res = await _mediatorHandler.SendCommandAsync(cmd);

            return res.Status ? ApiResponseBase.Success()
                             : ApiResponseBase.Fail(res.Code, res.Message);
        }

        [Route("modifyState")]
        [HttpPost]
        public async Task<ApiResponseBase> ModifyStateAsync(ModifyStateCmd cmd)
        {
            var res = await _mediatorHandler.SendCommandAsync(cmd);

            return res.Status ? ApiResponseBase.Success()
                             : ApiResponseBase.Fail(res.Code, res.Message);
        }


        [Route("GetUsers")]
        [HttpGet]
        public async Task<ApiResponseBase<List<UserDto>?>> GetUsersAsync([FromQuery] string? keyword)
        {
            var res = await _userService.GetUsersAsync(keyword);

            return ApiResponseBase<List<UserDto>?>.Success(data: res.Data);
        }

        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResponseBase<TokenDto>> LoginAsync(LoginReq req)
        {
            var res = await _userService.LoginAsync(req.Account, req.Password);

            return res.Status ? ApiResponseBase<TokenDto>.Success(data: res.Data!) : ApiResponseBase<TokenDto>.Fail(res.Code, res.Message);
        }

        [Route("auth")]
        [HttpPost]
        public async Task<ApiResponseBase<AuthInfo>> GetAuthAsync()
        { 
            var res= await _userService.GetAuthAsync();

            return res.Status ? ApiResponseBase<AuthInfo>.Success(data: res.Data!) : ApiResponseBase<AuthInfo>.Fail(res.Code, res.Message);
        }
    }
}
