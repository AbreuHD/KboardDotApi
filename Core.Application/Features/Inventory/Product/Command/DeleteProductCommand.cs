using Core.Application.Dtos.Generic;
using Core.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Inventory.Product.Command
{
    public class DeleteProductCommand : IRequest<GenericApiResponse<bool>>
    {
        public int ID { get; set; }
    }
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, GenericApiResponse<bool>>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<GenericApiResponse<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var response = new GenericApiResponse<bool>();
            try
            {
                var product = await _productRepository.GetByIdAsync(request.ID);

                if (product == null)
                {
                    response.Success = false;
                    response.Message = "Product not found";
                    response.Statuscode = StatusCodes.Status404NotFound;
                    response.Payload = false;
                    return response;
                }

                await _productRepository.DeleteAsync(product);
                response.Success = true;
                response.Message = "Product deleted successfully";
                response.Statuscode = StatusCodes.Status200OK;
                response.Payload = true;

            }catch (Exception ex)
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
