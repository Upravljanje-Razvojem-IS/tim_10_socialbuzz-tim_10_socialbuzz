using AutoMapper;
using UserService.DTOs.AdministratorDTOs;
using UserService.Models;

namespace UserService.MapperProfiles
{
    public class AdministratorMapper : Profile
    {
        public AdministratorMapper()
        {
            CreateMap<Administrator, AdministratorReadDto>();
            CreateMap<AdministratorCreateDto, Administrator>();
        }
    }
}
