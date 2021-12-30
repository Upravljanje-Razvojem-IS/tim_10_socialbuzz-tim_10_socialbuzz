using AutoMapper;
using UserService.DTOs.CorporateDTOs;
using UserService.Models;

namespace UserService.MapperProfiles
{
    public class CorporateMapper : Profile
    {
        public CorporateMapper()
        {
            CreateMap<Corporate, CorporateReadDto>();
            CreateMap<CorporateCreateDto, Corporate>();
        }
    }
}
