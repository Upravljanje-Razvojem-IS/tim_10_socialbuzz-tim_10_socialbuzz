using AutoMapper;
using UserService.CustomException;
using UserService.DTOs.AdministratorDTOs;
using UserService.Interfaces;
using UserService.MockLogger;
using UserService.Models;

namespace UserService.Repositories
{
    public class AdministratorRepository : IAdministratorRepository
    {
        private readonly IFakeLogger _logger;
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public AdministratorRepository(IMapper mapper, UserDbContext context, IFakeLogger logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public AdministratorReadDto Create(AdministratorCreateDto dto)
        {
            var admin = _mapper.Map<Administrator>(dto);

            _context.Administrators.Add(admin);

            _context.SaveChanges();

            _logger.Log("Admin created!");

            return _mapper.Map<AdministratorReadDto>(admin);
        }

        public void Delete(int id)
        {
            var admin = _context.Administrators.FirstOrDefault(e => e.Id == id);

            if (admin == null)
                throw new BusinessException("Administrator does not exist");


            _context.Administrators.Remove(admin);

            _context.SaveChanges();
        }

        public List<AdministratorReadDto> Get()
        {
            var administrators = _context.Administrators.ToList();

            return _mapper.Map<List<AdministratorReadDto>>(administrators);
        }

        public AdministratorReadDto Get(int id)
        {
            var admin = _context.Administrators.FirstOrDefault(e => e.Id == id);

            return _mapper.Map<AdministratorReadDto>(admin);
        }

        public AdministratorReadDto GetByEmail(string email)
        {
            var admin = _context.Administrators.FirstOrDefault(e => e.Email == email);

            return _mapper.Map<AdministratorReadDto>(admin);
        }

        public AdministratorReadDto Update(int id, AdministratorCreateDto dto)
        {
            var admin = _context.Administrators.FirstOrDefault(e => e.Id == id);

            if (admin == null)
                throw new BusinessException("Admin does not exist");

            admin.Name = dto.Name;
            admin.Username = dto.Username;
            admin.Surname = dto.Surname;
            admin.Password = dto.Password;
            admin.Email = dto.Email;

            _context.SaveChanges();

            return _mapper.Map<AdministratorReadDto>(admin);
        }
    }
}
