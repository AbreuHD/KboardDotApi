using AutoMapper;
using Core.Application.Dtos.Generic;
using Core.Application.Dtos.Inventory.Response;
using Core.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Inventory.Miscellaneous.Queries
{
    public class GetAllTaxesQuery : IRequest<GenericApiResponse<List<TaxResponseDto>>>
    {
    }
    public class GetAllTaxesQueryHandler : IRequestHandler<GetAllTaxesQuery, GenericApiResponse<List<TaxResponseDto>>>
    {
        private readonly ITaxRepository _taxRepository;
        private readonly IMapper _mapper;

        public GetAllTaxesQueryHandler(ITaxRepository taxRepository, IMapper mapper)
        {
            _taxRepository = taxRepository;
            _mapper = mapper;
        }

        public async Task<GenericApiResponse<List<TaxResponseDto>>> Handle(GetAllTaxesQuery request, CancellationToken cancellationToken)
        {
            var response =  new GenericApiResponse<List<TaxResponseDto>>();

            try
            {
                var taxes = await _taxRepository.GetAllAsync();
                response.Payload = _mapper.Map<List<TaxResponseDto>>(taxes);
                response.Statuscode = StatusCodes.Status200OK;
                response.Message = "Success";
                response.Success = true;

            }catch (Exception ex)
            {
                response.Statuscode = StatusCodes.Status500InternalServerError;
                response.Message = ex.Message;
                response.Success = false;
            }


            return response;
        }
    }
}
