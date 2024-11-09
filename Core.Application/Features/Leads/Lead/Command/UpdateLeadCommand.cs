using AutoMapper;
using Core.Application.Dtos.Generic;
using Core.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Core.Application.Features.Leads.Lead.Command
{
    public class UpdateLeadCommand : IRequest<GenericApiResponse<bool>>
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int PhoneNumber { get; set; }
        public string Address { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
    }

    public class UpdateLeadCommandHandler : IRequestHandler<UpdateLeadCommand, GenericApiResponse<bool>>
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IMapper _mapper;

        public UpdateLeadCommandHandler(ILeadRepository leadRepository, IMapper mapper)
        {
            _leadRepository = leadRepository;
            _mapper = mapper;
        }

        public async Task<GenericApiResponse<bool>> Handle(UpdateLeadCommand request, CancellationToken cancellationToken)
        {
            var response = new GenericApiResponse<bool>();

            try
            {
                var lead = await _leadRepository.GetByIdAsync(request.ID);

                if (lead == null)
                {
                    response.Success = false;
                    response.Message = "Lead not found";
                    response.Statuscode = StatusCodes.Status404NotFound;
                    response.Payload = false;
                    return response;
                }

                // Mapea los datos de la solicitud en el objeto existente
                _mapper.Map(request, lead);

                // Llama a UpdateAsync con el objeto y el ID
                await _leadRepository.UpdateAsync(lead, request.ID);

                response.Success = true;
                response.Message = "Lead updated successfully";
                response.Statuscode = StatusCodes.Status200OK;
                response.Payload = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Statuscode = StatusCodes.Status500InternalServerError;
                response.Payload = false;
            }

            return response;
        }
    }
}

