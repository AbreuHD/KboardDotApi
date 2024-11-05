using AutoMapper;
using Core.Application.Dtos.Generic;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities.Taxes;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Inventory.Miscellaneous.Command
{
    public class CreateTaxCommand : IRequest<GenericApiResponse<bool>>
    {
        public string Name { get; set; }
        public double Rate { get; set; }
    }

    public class CreateTaxCommandHandler : IRequestHandler<CreateTaxCommand, GenericApiResponse<bool>>
    {
        private readonly IMapper _mapper;
        private readonly ITaxRepository _taxRepository;
        
        public CreateTaxCommandHandler(IMapper mapper, ITaxRepository taxRepository) 
        {
            _mapper = mapper;
            _taxRepository = taxRepository;
        }

        public async Task<GenericApiResponse<bool>> Handle(CreateTaxCommand request, CancellationToken cancellationToken)
        {
            var response = new GenericApiResponse<bool>();
            try
            {
                var tax = _mapper.Map<Tax>(request);
                await _taxRepository.AddAsync(tax);

                response.Success = true;
                response.Message = "Tax created successfully";
                response.Payload = true;
                response.Statuscode = StatusCodes.Status201Created;

            }catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Payload = false;
                response.Statuscode = StatusCodes.Status500InternalServerError;
            }

            return response;
        }
    }
}
