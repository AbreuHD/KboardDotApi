using AutoMapper;
using Core.Application.Dtos.Generic;
using Core.Application.Dtos.Inventory.Response;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities.Taxes;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Inventory.Product.Queries
{
    public class GetProductDetailsByIdQuery : IRequest<GenericApiResponse<DetailsProductResponseDto>>
    {
        public int ID { get; set; }
    }

    public class GetProductDetailsByIdQueryHandler : IRequestHandler<GetProductDetailsByIdQuery, GenericApiResponse<DetailsProductResponseDto>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICharacteristicsRepository _characteristicsRepository;
        private readonly ISourceRepository _sourceRepository;
        private readonly ITaxRepository _taxRepository;

        public GetProductDetailsByIdQueryHandler(IMapper mapper, IProductRepository productRepository, ICategoryRepository categoryRepository
            , ICharacteristicsRepository characteristicsRepository, ISourceRepository sourceRepository, ITaxRepository taxRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _characteristicsRepository = characteristicsRepository;
            _sourceRepository = sourceRepository;
            _taxRepository = taxRepository;
        }


        public async Task<GenericApiResponse<DetailsProductResponseDto>> Handle(GetProductDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new GenericApiResponse<DetailsProductResponseDto>();
            try
            {
                var productResponse = await _productRepository.GetAllWithIncludeAsync(
                               new List<string> { "Product_Categories", "Characteristic_Products", "Source_Product", "Product_Tax" });

                var filteredResponse = productResponse.Where(x => x.ID == request.ID).FirstOrDefault();

                response.Payload = _mapper.Map<DetailsProductResponseDto>(filteredResponse);
                response.Payload.Categories = new List<CategoryResponseDto>();
                response.Payload.Characteristics = new List<CharacteristicResponseDto>();
                response.Payload.Taxs = new List<TaxResponseDto>();


                foreach (var category in filteredResponse.Product_Categories)
                {
                    var categoryEntity = await _categoryRepository.GetByIdAsync(category.ID);
                    var categoryDto = _mapper.Map<CategoryResponseDto>(categoryEntity);
                    response.Payload.Categories.Add(categoryDto);
                }
                foreach (var characteristic in filteredResponse.Characteristic_Products)
                {
                    var characteristicEntity = await _characteristicsRepository.GetByIdAsync(characteristic.ID);
                    var characteristicDto = _mapper.Map<CharacteristicResponseDto>(characteristicEntity);
                    response.Payload.Characteristics.Add(characteristicDto);
                }
                foreach (var source in filteredResponse.Source_Product)
                {
                    var sourceEntity = await _sourceRepository.GetByIdAsync(source.ID);
                    var sourceDto = _mapper.Map<SourceResponseDto>(sourceEntity);
                    response.Payload.Source = sourceDto;
                }
                foreach (var tax in filteredResponse.Product_Tax)
                {
                    var taxEntity = await _taxRepository.GetByIdAsync(tax.ID);
                    var taxDto = _mapper.Map<TaxResponseDto>(taxEntity);
                    response.Payload.Taxs.Add(taxDto);
                }
                response.Statuscode = StatusCodes.Status200OK;
                response.Success = true;
                response.Message = "Product details retrieved successfully";

            }catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.Statuscode = StatusCodes.Status500InternalServerError;
            }



            return response;
        }
    }
}
