using AutoMapper;
using ProductAndServices.DTOs.ProductDTOs;
using ProductAndServices.Models;

namespace ProductAndServices.MapperProfiles
{
    public class ServiceMapper : Profile
    {
        public ServiceMapper()
        {
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductCreateDto, Product>();
        }
    }
}
