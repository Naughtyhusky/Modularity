using Infrastructure.Bus;
using Infrastructure.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Modules.Org.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Org.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CompanyController(ILogger<CompanyController> logger, IMediatorHandler mediatorHandler) : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger = logger;

        private readonly IMediatorHandler _mediatorHandler = mediatorHandler;


        [Route("create")]
        [HttpPost]
        public async Task<ApiResponseBase> CreateAsync(CreateCompanyCmd cmd)
        {
            var res = await _mediatorHandler.SendCommandAsync(cmd);

            return res.Status ? ApiResponseBase.Success()
                             : ApiResponseBase.Fail(res.Code, res.Message);
        }
    }
}
