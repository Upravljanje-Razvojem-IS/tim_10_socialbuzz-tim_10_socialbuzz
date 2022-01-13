using AutoMapper;
using ProductAndServices.DTOs.ProductDTOs;
using ProductAndServices.Models;

namespace ProductAndServices.MapperProfiles
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductCreateDto, Product>();
        }
    }
}
