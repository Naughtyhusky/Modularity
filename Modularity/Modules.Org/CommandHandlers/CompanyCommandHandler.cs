using Infrastructure.Bus;
using Infrastructure.Common;
using Infrastructure.DataBase;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Modules.Org.Commands;
using Modules.Org.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Org.CommandHandlers
{
    public class CompanyCommandHandler(IRepository<Company> companyRepository, ILogger<CompanyCommandHandler> logger, ICueerntUserProvider cueerntUserProvider, IMediatorHandler mediatorHandler) : ServerBase(cueerntUserProvider, mediatorHandler),
                                          IRequestHandler<CreateCompanyCmd,CommandResponse>
    {
        private readonly IRepository<Company> _companyRepository = companyRepository;

        private readonly ILogger<CompanyCommandHandler> _logger = logger;

        public async Task<CommandResponse> Handle(CreateCompanyCmd request, CancellationToken cancellationToken)
        {
            var company = new Company(request.Name, request.Description, _user.Id, _user.Name!);

            await _companyRepository.AddAsync(company);

            return CommandResponse.Success();
        }
    }
}
