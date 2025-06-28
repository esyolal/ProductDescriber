using AutoMapper;
using ProductDescriber.Data.Entities;
using ProductDescriber.Schema.Requests;
using ProductDescriber.Schema.Responses;

namespace ProductDescriber.Application.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        // Request -> Entity
        CreateMap<ProductCreateRequest, Product>()
            .ForMember(dest => dest.Description, opt => opt.Ignore()) // LLM üretecek
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());  // veritabanında otomatik

        // Entity -> Response
        CreateMap<Product, ProductResponse>();
    }
}
