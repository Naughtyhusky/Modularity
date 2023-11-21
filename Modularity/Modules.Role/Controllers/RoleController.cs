using Infrastructure.Bus;
using Infrastructure.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Modules.Role.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Role.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RoleController(ILogger<RoleController> logger, IMediatorHandler mediatorHandler) : ControllerBase
    {
        private readonly ILogger<RoleController> _logger = logger;
      
        private readonly IMediatorHandler _mediatorHandler = mediatorHandler;

        [Route("create")]
        [HttpPost]
        public async Task<ApiResponseBase> CreateAsync(CreateRoleCmd cmd)
        {
            var res = await _mediatorHandler.SendCommandAsync(cmd);

            return res.Status ? ApiResponseBase.Success()
                             : ApiResponseBase.Fail(res.Code, res.Message);
        }

        [Route("modify")]   
        [HttpPost]
        public async Task<ApiResponseBase> ModifyAsync(ModifyRoleInfoCmd cmd)
        {
            var res = await _mediatorHandler.SendCommandAsync(cmd);

            return res.Status ? ApiResponseBase.Success()
                             : ApiResponseBase.Fail(res.Code, res.Message);
        }

        [Route("delete")]
        [HttpPost]
        public async Task<ApiResponseBase> DeleteAsync(DeleteRoleCmd cmd)
        {
            var res = await _mediatorHandler.SendCommandAsync(cmd);

            return res.Status ? ApiResponseBase.Success()
                             : ApiResponseBase.Fail(res.Code, res.Message);
        }
    }
}
