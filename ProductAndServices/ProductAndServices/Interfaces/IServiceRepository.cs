using ProductAndServices.DTOs.Service;

namespace ProductAndServices.Interfaces
{
    public interface IServiceRepository
    {
        List<ServiceReadDto> Get();
        ServiceReadDto Get(int id);
        ServiceReadDto GetByUser(int userId);
        ServiceReadDto Create(ServiceCreateDto DTO);
        ServiceReadDto Update(int id, ServiceCreateDto DTO);
        void Delete(int id);
    }
}
