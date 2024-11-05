using AutoMapper;
using Core.Application.Dtos.Generic;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities.Source;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Inventory.Miscellaneous.Command
{
    public class TrackingTypeCommand : IRequest<GenericApiResponse<bool>>
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        public string Key { get; set; }
    }

    public class TrackingTypeCommandHandler : IRequestHandler<TrackingTypeCommand, GenericApiResponse<bool>>
    {
        private readonly IMapper _mapper;
        private readonly ITrackingTypeRepository _trackingTypeRepository;

        public TrackingTypeCommandHandler(IMapper mapper, ITrackingTypeRepository trackingTypeRepository)
        {
            _mapper = mapper;
            _trackingTypeRepository = trackingTypeRepository;
        }

        public async Task<GenericApiResponse<bool>> Handle(TrackingTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new GenericApiResponse<bool>();

            try
            {
                var trackingType = _mapper.Map<TrackingType>(request);
                await _trackingTypeRepository.AddAsync(trackingType);
                response.Success = true;
                response.Message = "Tracking Type created successfully";
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
