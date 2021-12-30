using AutoMapper;
using UserService.DTOs.PersonalDTOs;
using UserService.Models;

namespace UserService.MapperProfiles
{
    public class PersonalMapper : Profile
    {
        public PersonalMapper()
        {
            CreateMap<Personal, PersonalReadDto>();
            CreateMap<PersonalCreateDto, Personal>();
        }
    }
}
