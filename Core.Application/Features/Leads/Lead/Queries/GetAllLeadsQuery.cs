using AutoMapper;
using Core.Application.Dtos.Generic;
using Core.Application.Dtos.Inventory.Response;
using Core.Application.Dtos.Leads.Response;
using Core.Application.Features.Inventory.Product.Queries;
using Core.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Leads.Lead.Queries
{
    public class GetAllLeadsQuery : IRequest<GenericApiResponse<List<PreviewLeadResponseDto>>>
    {
        public int Quantity { get; set; }
        public int Page { get; set; }
        public int? Category { get; set; }
        public string? Search { get; set; }
    }
    public class GetAllLeadsQueryHandler : IRequestHandler<GetAllLeadsQuery, GenericApiResponse<List<PreviewLeadResponseDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ILeadRepository _leadRepository;

        public GetAllLeadsQueryHandler(IMapper mapper, ILeadRepository leadRepository)
        {
            _mapper = mapper;
            _leadRepository = leadRepository;
        }

        public async Task<GenericApiResponse<List<PreviewLeadResponseDto>>> Handle(GetAllLeadsQuery request, CancellationToken cancellationToken)
        {
            var response = new GenericApiResponse<List<PreviewLeadResponseDto>>();
            try
            {
                var leadResponse = await _leadRepository.SearchLead (request.Search,  request.Page, request.Quantity);
                response.Payload = _mapper.Map<List<PreviewLeadResponseDto>>(leadResponse);
                response.Statuscode = StatusCodes.Status200OK;
                response.Message = "Leads retrieved successfully!";
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Statuscode = StatusCodes.Status500InternalServerError;
            }


            return response;
        }
    }
}


