using Infrastructure.Bus;
using Infrastructure.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Modules.Resource.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Resource.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MenuController(ILogger<MenuController> logger, IMediatorHandler mediatorHandler) : ControllerBase
    {
        private readonly ILogger<MenuController> _logger = logger;

        private readonly IMediatorHandler _mediatorHandler = mediatorHandler;


        [Route("create")]
        [HttpPost]
        public async Task<ApiResponseBase> CreateAsync(CreateMenuCmd cmd)
        {
            var res = await _mediatorHandler.SendCommandAsync(cmd);

            return res.Status ? ApiResponseBase.Success()
                             : ApiResponseBase.Fail(res.Code, res.Message);
        }
    }
}
