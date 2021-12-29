using UserService.DTOs.UserDTOs;

namespace UserService.Interfaces
{
    public interface IUserRepository
    {
        List<UserReadDto> Get();
        UserReadDto Get(int id);
        UserReadDto GetByEmail(string email);
        UserReadDto Create(UserCreateDto dto);
        UserReadDto Update(int id, UserCreateDto dto);
        void Delete(int id);
    }
}
