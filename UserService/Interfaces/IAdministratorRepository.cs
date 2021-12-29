using UserService.DTOs.AdministratorDTOs;

namespace UserService.Interfaces
{
    public interface IAdministratorRepository
    {
        List<AdministratorReadDto> Get();
        AdministratorReadDto Get(int id);
        AdministratorReadDto GetByEmail(string email);
        AdministratorReadDto Create(AdministratorCreateDto dto);
        AdministratorReadDto Update(int id, AdministratorCreateDto dto);
        void Delete(int id);
    }
}
