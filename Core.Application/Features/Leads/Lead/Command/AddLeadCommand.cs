using AutoMapper;
using Core.Application.Dtos.Generic;
using Core.Application.Features.Inventory.Product.Command;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities.Category;
using Core.Domain.Entities.Characteristics;
using Core.Domain.Entities.Source;
using Core.Domain.Entities.Taxes;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Leads.Lead.Command
{
    public class AddLeadCommand : IRequest<GenericApiResponse<bool>>
    {

        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int PhoneNumber { get; set; }
        public string Address { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;

        public class AddLeadCommandHandler : IRequestHandler<AddLeadCommand, GenericApiResponse<bool>>
        {
            private readonly IMapper _mapper;
            private readonly ILeadRepository _leadRepository;


            public AddLeadCommandHandler(IMapper mapper, ILeadRepository leadRepository)
            {
                _mapper = mapper;
                _leadRepository = leadRepository;
            }

            public async Task<GenericApiResponse<bool>> Handle(AddLeadCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericApiResponse<bool>();

                try
                {
                    // Mapeo y creación del Lead
                    var leadEntity = _mapper.Map<Domain.Entities.Leads.Leads>(request);
                    await _leadRepository.AddAsync(leadEntity);

                    response.Payload = true;
                    response.Message = "Lead added successfully";
                    response.Success = true;
                    response.Statuscode = StatusCodes.Status200OK;
                }
                catch (Exception ex)
                {
                    response.Statuscode = StatusCodes.Status500InternalServerError;
                    response.Message = ex.Message;
                    response.Success = false;
                }

                return response;
            }
        }
    }
}