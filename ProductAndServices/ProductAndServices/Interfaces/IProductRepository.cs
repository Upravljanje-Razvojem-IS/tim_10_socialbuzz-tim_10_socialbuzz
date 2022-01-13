using ProductAndServices.DTOs.ProductDTOs;

namespace ProductAndServices.Interfaces
{
    public interface IProductRepository
    {
        List<ProductReadDto> Get();
        ProductReadDto Get(int id);
        ProductReadDto GetByUser(int userId);
        ProductReadDto Create(ProductCreateDto dto);
        ProductReadDto Update(int id, ProductCreateDto dto);
        void Delete(int id);
    }
}
