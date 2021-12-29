using UserService.DTOs.PersonalDTOs;

namespace UserService.Interfaces
{
    public interface IPersonalRepository
    {
        List<PersonalReadDto> Get();
        PersonalReadDto Get(int id);
        PersonalReadDto GetByEmail(string email);
        PersonalReadDto Create(PersonalCreateDto dto);
        PersonalReadDto Update(int id, PersonalCreateDto dto);
        void Delete(int id);
    }
}
