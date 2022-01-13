using AutoMapper;
using ProductAndServices.CustomException;
using ProductAndServices.DTOs.ProductDTOs;
using ProductAndServices.Interfaces;
using ProductAndServices.MockLogger;
using ProductAndServices.Models;

namespace ProductAndServices.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IFakeLogger _logger;
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(IMapper mapper, DatabaseContext context, IFakeLogger logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public ProductReadDto Create(ProductCreateDto dto)
        {
            var user = UserData.Users.FirstOrDefault(e => e.Id == dto.UserId);

            if (user == null)
                throw new BusinessException("User does not exist", 400);

            var product = _mapper.Map<Product>(dto);

            _context.Products.Add(product);

            _context.SaveChanges();

            _logger.Log("Product created!");

            return _mapper.Map<ProductReadDto>(product);
        }

        public void Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(e => e.Id == id);

            if (product == null)
                throw new BusinessException("Products does not exist");


            _context.Products.Remove(product);

            _context.SaveChanges();
        }

        public List<ProductReadDto> Get()
        {
            var products = _context.Products.ToList();

            return _mapper.Map<List<ProductReadDto>>(products);
        }

        public ProductReadDto Get(int id)
        {
            var product = _context.Products.FirstOrDefault(e => e.Id == id);

            return _mapper.Map<ProductReadDto>(product);
        }

        public ProductReadDto GetByUser(int userId)
        {
            var product = _context.Products.FirstOrDefault(e => e.UserId == userId);

            return _mapper.Map<ProductReadDto>(product);
        }

        public ProductReadDto Update(int id, ProductCreateDto dto)
        {
            var user = UserData.Users.FirstOrDefault(e => e.Id == dto.UserId);

            if (user == null)
                throw new BusinessException("User does not exist", 400);

            var product = _context.Products.FirstOrDefault(e => e.Id == id);

            if (product == null)
                throw new BusinessException("Product does not exist");

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Quantity = dto.Quantity;
            product.UserId = dto.UserId;

            _context.SaveChanges();

            return _mapper.Map<ProductReadDto>(product);
        }
    }
}
