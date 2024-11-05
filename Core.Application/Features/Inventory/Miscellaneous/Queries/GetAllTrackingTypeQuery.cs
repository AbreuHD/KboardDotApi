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
    public class GetAllTrackingTypeQuery : IRequest<GenericApiResponse<List<TrackingTypeResponseDto>>>
    {
    }

    public class GetAllTrackingTypeQueryHandler : IRequestHandler<GetAllTrackingTypeQuery, GenericApiResponse<List<TrackingTypeResponseDto>>>
    {
        private readonly ITrackingTypeRepository _trackingTypeRepository;
        private readonly IMapper _mapper;

        public GetAllTrackingTypeQueryHandler(ITrackingTypeRepository trackingTypeRepository, IMapper mapper)
        {
            _trackingTypeRepository = trackingTypeRepository;
            _mapper = mapper;
        }

        public async Task<GenericApiResponse<List<TrackingTypeResponseDto>>> Handle(GetAllTrackingTypeQuery request, CancellationToken cancellationToken)
        {
            var response = new GenericApiResponse<List<TrackingTypeResponseDto>>();
            try
            {
                var trackingTypes = await _trackingTypeRepository.GetAllAsync();
                response.Payload = _mapper.Map<List<TrackingTypeResponseDto>>(trackingTypes);
                response.Statuscode = StatusCodes.Status200OK;
                response.Message = "Success";
                response.Success = true;
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
