using AutoMapper;
using Core.Application.Dtos.Generic;
using Core.Application.Dtos.Inventory.Create;
using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities.Category;
using Core.Domain.Entities.Characteristics;
using Core.Domain.Entities.Product;
using Core.Domain.Entities.Source;
using Core.Domain.Entities.Taxes;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Features.Inventory.Product.Command
{
    public class AddProductCommand : IRequest<GenericApiResponse<bool>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public double SalePrice { get; set; }
        public double BuyPrice { get; set; }
        public int Stock { get; set; }

        public List<CreateCharacteristicsDto>? NewCharacteristic { get; set; }
        public List<int>? Characteristic { get; set; }

        public List<CreateCategoriesDto>? NewCategories { get; set; }
        public List<int>? Categories { get; set; }

        public CreateSourceDto? NewSource { get; set; }
        public int? Source { get; set; }

        public int? TrackingTypeId { get; set; }
        public List<int>? Taxes { get; set; }

        public class AddProductCommandHandler : IRequestHandler<AddProductCommand, GenericApiResponse<bool>>
        {
            private readonly IMapper _mapper;
            private readonly IProductRepository _productRepository;
            private readonly ICharacteristicsRepository _characteristicsRepository;
            private readonly ICharacteristics_ProductRepository _characteristics_ProductRepository;
            private readonly ICategoryRepository _categoryRepository;
            private readonly IProduct_CategoryRepository _product_CategoryRepository;
            private readonly ISourceRepository _sourceRepository;
            private readonly ISource_ProductRepository _source_ProductRepository;
            private readonly IProduct_TaxRepository _product_TaxRepository;


            public AddProductCommandHandler(IMapper mapper, IProductRepository productRepository
                , ICharacteristicsRepository characteristicsRepository, ICharacteristics_ProductRepository characteristics_ProductRepository
                , ICategoryRepository categoryRepository, IProduct_CategoryRepository product_CategoryRepository, ISourceRepository sourceRepository
                , ISource_ProductRepository source_ProductRepository, IProduct_TaxRepository product_TaxRepository)
            {
                _mapper = mapper;
                _productRepository = productRepository;

                _characteristicsRepository = characteristicsRepository;
                _characteristics_ProductRepository = characteristics_ProductRepository;

                _categoryRepository = categoryRepository;
                _product_CategoryRepository = product_CategoryRepository;

                _sourceRepository = sourceRepository;
                _source_ProductRepository = source_ProductRepository;
                _productRepository = productRepository;
            }

            public async Task<GenericApiResponse<bool>> Handle(AddProductCommand request, CancellationToken cancellationToken)
            {
                var response = new GenericApiResponse<bool>();
                var ListCharacteristic = request.Characteristic ?? new List<int>();
                var ListCategories = request.Categories ?? new List<int>();
                int source = request.Source ?? 0;

                try
                {
                    var productResponse = await _productRepository.AddAsync(_mapper.Map<Domain.Entities.Product.Product>(request));

                    #region Characteristics

                    if (request.NewCharacteristic != null)
                    {
                        var characteristics = _mapper.Map<List<Characteristic>>(request.NewCharacteristic);
                        foreach (var item in characteristics)
                        {
                            var charResponse = await _characteristicsRepository.AddAsync(item);
                            ListCharacteristic.Add(charResponse.ID);
                        }
                    }

                    foreach (var item in ListCharacteristic)
                    {
                        if(item != 0)
                        {
                            await _characteristics_ProductRepository.AddAsync(new Characteristic_Product { CharacteristicsId = item, ProductId = productResponse.ID });
                        }
                    }

                    #endregion Characteristics

                    #region Categories

                    if (request.NewCategories != null)
                    {
                        var categories = _mapper.Map<List<Category>>(request.NewCategories);
                        foreach (var item in categories)
                        {
                            var cateResponse = await _categoryRepository.AddAsync(item);
                            ListCategories.Add(cateResponse.ID);
                        }
                    }

                    foreach (var item in ListCategories)
                    {
                        if(item != 0)
                        {
                            await _product_CategoryRepository.AddAsync(new Product_Category { CategoryId = item, ProductId = productResponse.ID });
                        }
                    }

                    #endregion Categories

                    if (request.NewSource != null && request.TrackingTypeId != null && request.NewSource.TrackingTypeId != 0)
                    {
                        var SourceRequest = _mapper.Map<Source>(request.NewSource);
                        SourceRequest.TrackingTypeId = (int)request.TrackingTypeId;
                        var sourceResponse = await _sourceRepository.AddAsync(SourceRequest);
                        source = sourceResponse.ID;
                    }
                    if (source != 0)
                    {
                        await _source_ProductRepository.AddAsync(new Source_Product { ProductId = productResponse.ID, SourceId = source });
                    }

                    if(request.Taxes != null && request.Taxes.FirstOrDefault() != 0)
                    {
                        foreach (var item in request.Taxes)
                        {
                            await _product_TaxRepository.AddAsync(new Product_Tax { ProductId = productResponse.ID, TaxId = item });
                        }
                    }

                    response.Payload = true;
                    response.Message = "Product added successfully";
                    response.Success = true;
                    response.Statuscode = StatusCodes.Status200OK;

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
}
