using AutoMapper;
using UserService.CustomException;
using UserService.DTOs.PersonalDTOs;
using UserService.Interfaces;
using UserService.MockLogger;
using UserService.Models;

namespace UserService.Repositories
{
    public class PersonalRepository : IPersonalRepository
    {
        private readonly IFakeLogger _logger;
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public PersonalRepository(IMapper mapper, UserDbContext context, IFakeLogger logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public PersonalReadDto Create(PersonalCreateDto dto)
        {
            var personal = _mapper.Map<Personal>(dto);

            _context.Personal.Add(personal);

            _context.SaveChanges();

            _logger.Log("Personal created!");

            return _mapper.Map<PersonalReadDto>(personal);
        }

        public void Delete(int id)
        {
            var personal = _context.Personal.FirstOrDefault(e => e.Id == id);

            if (personal == null)
                throw new BusinessException("Personal does not exist");


            _context.Personal.Remove(personal);

            _context.SaveChanges();
        }

        public List<PersonalReadDto> Get()
        {
            var personal = _context.Personal.ToList();

            return _mapper.Map<List<PersonalReadDto>>(personal);
        }

        public PersonalReadDto Get(int id)
        {
            var personal = _context.Personal.FirstOrDefault(e => e.Id == id);

            return _mapper.Map<PersonalReadDto>(personal);
        }

        public PersonalReadDto GetByEmail(string email)
        {
            var personal = _context.Personal.FirstOrDefault(e => e.Email == email);

            return _mapper.Map<PersonalReadDto>(personal);
        }

        public PersonalReadDto Update(int id, PersonalCreateDto dto)
        {
            var personal = _context.Personal.FirstOrDefault(e => e.Id == id);

            if (personal == null)
                throw new BusinessException("Admin does not exist");

            personal.CreatedAt = dto.CreatedAt;
            personal.Balance = dto.Balance;
            personal.Username = dto.Username;
            personal.Name = dto.Name;
            personal.Surname = dto.Surname;
            personal.Email = dto.Email;
            personal.Address = dto.Address;
            personal.City = dto.City;
            personal.Mobile = dto.Mobile;
            personal.Password = dto.Password;
            personal.IsPersonal = dto.IsPersonal;

            _context.SaveChanges();

            return _mapper.Map<PersonalReadDto>(personal);
        }
    }
}
