using AutoMapper;
using ProductAndServices.CustomException;
using ProductAndServices.DTOs.Service;
using ProductAndServices.Interfaces;
using ProductAndServices.MockLogger;
using ProductAndServices.Models;

namespace ServiceAndServices.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly IFakeLogger _logger;
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public ServiceRepository(IMapper mapper, DatabaseContext context, IFakeLogger logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public ServiceReadDto Create(ServiceCreateDto dto)
        {
            var user = UserData.Users.FirstOrDefault(e => e.Id == dto.UserId);

            if (user == null)
                throw new BusinessException("User does not exist", 400);

            var service = _mapper.Map<Service>(dto);

            _context.Services.Add(service);

            _context.SaveChanges();

            _logger.Log("Service created!");

            return _mapper.Map<ServiceReadDto>(service);
        }

        public void Delete(int id)
        {
            var service = _context.Services.FirstOrDefault(e => e.Id == id);

            if (service == null)
                throw new BusinessException("Services does not exist");


            _context.Services.Remove(service);

            _context.SaveChanges();
        }

        public List<ServiceReadDto> Get()
        {
            var services = _context.Services.ToList();

            return _mapper.Map<List<ServiceReadDto>>(services);
        }

        public ServiceReadDto Get(int id)
        {
            var service = _context.Services.FirstOrDefault(e => e.Id == id);

            return _mapper.Map<ServiceReadDto>(service);
        }

        public ServiceReadDto GetByUser(int userId)
        {
            var service = _context.Services.FirstOrDefault(e => e.UserId == userId);

            return _mapper.Map<ServiceReadDto>(service);
        }

        public ServiceReadDto Update(int id, ServiceCreateDto dto)
        {
            var user = UserData.Users.FirstOrDefault(e => e.Id == dto.UserId);

            if (user == null)
                throw new BusinessException("User does not exist", 400);

            var service = _context.Services.FirstOrDefault(e => e.Id == id);

            if (service == null)
                throw new BusinessException("Service does not exist");

            service.Name = dto.Name;
            service.Description = dto.Description;
            service.Price = dto.Price;
            service.UserId = dto.UserId;

            _context.SaveChanges();

            return _mapper.Map<ServiceReadDto>(service);
        }
    }
}
