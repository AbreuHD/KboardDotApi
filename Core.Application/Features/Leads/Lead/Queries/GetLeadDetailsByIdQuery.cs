using AutoMapper;
using Core.Application.Dtos.Generic;
using Core.Application.Dtos.Leads.Response;
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
    public class GetLeadDetailsByIdQuery : IRequest<GenericApiResponse<DetailsLeadsResponseDto>>
    {
        public int ID { get; set; }
    }

    public class GetLeadDetailsByIdQueryHandler : IRequestHandler<GetLeadDetailsByIdQuery, GenericApiResponse<DetailsLeadsResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeadRepository _leadRepository;

        public GetLeadDetailsByIdQueryHandler(IMapper mapper, ILeadRepository leadRepository)
        {
            _mapper = mapper;
            _leadRepository = leadRepository;
        }

        public async Task<GenericApiResponse<DetailsLeadsResponseDto>> Handle(GetLeadDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new GenericApiResponse<DetailsLeadsResponseDto>();

            try
            {
                // Obtener el Lead directamente por ID
                var lead = await _leadRepository.GetByIdAsync(request.ID);

                if (lead == null)
                {
                    response.Success = false;
                    response.Message = "Lead not found";
                    response.Statuscode = StatusCodes.Status404NotFound;
                    return response;
                }

                // Mapear la entidad Lead al DTO de respuesta
                response.Payload = _mapper.Map<DetailsLeadsResponseDto>(lead);
                response.Success = true;
                response.Message = "Lead details retrieved successfully";
                response.Statuscode = StatusCodes.Status200OK;
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

