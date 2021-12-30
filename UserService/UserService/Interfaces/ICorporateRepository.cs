using UserService.DTOs.CorporateDTOs;

namespace UserService.Interfaces
{
    public interface ICorporateRepository
    {
        List<CorporateReadDto> Get();
        CorporateReadDto Get(int id);
        CorporateReadDto Create(CorporateCreateDto dto);
        CorporateReadDto Update(int id, CorporateCreateDto dto);
        void Delete(int id);
    }
}
