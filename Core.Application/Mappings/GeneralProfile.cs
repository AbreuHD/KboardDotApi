using AutoMapper;
using Core.Application.Dtos.Inventory.Create;
using Core.Application.Dtos.Inventory.Response;
using Core.Application.Dtos.Leads.Create;
using Core.Application.Dtos.Leads.Response;
using Core.Application.Features.Inventory.Miscellaneous.Command;
using Core.Application.Features.Inventory.Product.Command;
using Core.Application.Features.Leads.Lead.Command;
using Core.Domain.Entities.Category;
using Core.Domain.Entities.Characteristics;
using Core.Domain.Entities.Leads;
using Core.Domain.Entities.Product;
using Core.Domain.Entities.Source;
using Core.Domain.Entities.Taxes;


namespace Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<AddProductCommand, Product>()
                .ReverseMap()
                .ForMember(x => x.Categories, x => x.Ignore())
                .ForMember(x => x.Characteristic, x => x.Ignore())
                .ForMember(x => x.Source, x => x.Ignore());

            CreateMap<CreateCharacteristicsDto, Characteristic>()
                .ReverseMap();            
            
            CreateMap<CreateCategoriesDto, Category>()
                .ReverseMap();            
            
            CreateMap<CreateSourceDto, Source>()
                .ReverseMap();            
            
            CreateMap<PreviewProductResponseDto, Product>()
                .ReverseMap();            
            
            CreateMap<CategoryResponseDto, Category>()
                .ReverseMap();

            CreateMap<CharacteristicResponseDto, Characteristic>()
                .ReverseMap();

            CreateMap<SourceResponseDto, Source>()
                .ReverseMap();

            CreateMap<TaxResponseDto, Tax>()
                .ReverseMap();

            CreateMap<DetailsProductResponseDto, Product>()
                .ForMember(x => x.Product_Categories, x => x.Ignore())
                .ForMember(x => x.Characteristic_Products, x => x.Ignore())
                .ForMember(x => x.Product_Invoices, x => x.Ignore())
                .ForMember(x => x.Source_Product, x => x.Ignore())
                .ForMember(x => x.Product_Tax, x => x.Ignore())
                .ReverseMap()
                .ForMember(x => x.Taxs, x => x.Ignore())
                .ForMember(x => x.Source, x => x.Ignore())
                .ForMember(x => x.Characteristics, x => x.Ignore())
                .ForMember(x => x.Categories, x => x.Ignore());

            CreateMap<CreateTaxCommand, Tax>()
                .ReverseMap();

            CreateMap<TrackingTypeCommand, TrackingType>()
                .ReverseMap();            
            
            CreateMap<TaxResponseDto, Tax>()
                .ReverseMap();            
            
            CreateMap<TrackingTypeResponseDto, TrackingType>()
                .ReverseMap();
       
            CreateMap<AddLeadCommand, Leads>()
               .ReverseMap();

            CreateMap<DetailsLeadsResponseDto, Leads>()
                .ReverseMap();

            CreateMap<CreateLeadDto, Leads>()
               .ReverseMap();
            //me falta el mapeo de los dto
        }
    }
}
