using AutoMapper;
using Core.Application.Dtos.Generic;
using Core.Application.Features.Inventory.Product.Command;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities.Product;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Leads.Lead.Command
{
    public class DeleteLeadCommand : IRequest<GenericApiResponse<bool>>
    {
        public int ID { get; set; }

    }

    public class DeleteLeadCommandHandler : IRequestHandler<DeleteLeadCommand, GenericApiResponse<bool>>
    {
        private readonly ILeadRepository _leadRepository;


        public DeleteLeadCommandHandler( ILeadRepository leadRepository)
        {
            
            _leadRepository = leadRepository;
        }

        public async Task<GenericApiResponse<bool>> Handle(DeleteLeadCommand request, CancellationToken cancellationToken)
        {
            var response = new GenericApiResponse<bool>();

            try
            {
                var lead = await _leadRepository.GetByIdAsync(request.ID);

                if (lead == null)
                {
                    response.Success = false;
                    response.Message = "lead not found";
                    response.Statuscode = StatusCodes.Status404NotFound;
                    response.Payload = false;
                    return response;
                }

                await _leadRepository.DeleteAsync(lead);
                response.Success = true;
                response.Message = "lead deleted successfully";
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


