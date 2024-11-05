using AutoMapper;
using Core.Application.Dtos.Generic;
using Core.Application.Dtos.Inventory.Response;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities.Category;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Inventory.Product.Queries
{
    public class GetAllProductsQuery : IRequest<GenericApiResponse<List<PreviewProductResponseDto>>>
    {
        public int Quantity { get; set; }
        public int Page { get; set; }
        public int? Category { get; set; }
        public string? Search { get; set; }
    }
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, GenericApiResponse<List<PreviewProductResponseDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<GenericApiResponse<List<PreviewProductResponseDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var response = new GenericApiResponse<List<PreviewProductResponseDto>>();
            try
            {
                var productsResponse = await _productRepository.SearchProduct(request.Search, request.Category, request.Page, request.Quantity);
                response.Payload = _mapper.Map<List<PreviewProductResponseDto>>(productsResponse);
                response.Statuscode = StatusCodes.Status200OK;
                response.Message = "Products retrieved successfully!";
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
