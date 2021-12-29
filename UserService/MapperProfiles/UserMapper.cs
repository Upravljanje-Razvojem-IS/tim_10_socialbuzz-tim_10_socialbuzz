using AutoMapper;
using UserService.DTOs.UserDTOs;
using UserService.Models;

namespace UserService.MapperProfiles
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
        }
    }
}
