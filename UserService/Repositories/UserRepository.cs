using AutoMapper;
using UserService.CustomException;
using UserService.DTOs.UserDTOs;
using UserService.Interfaces;
using UserService.MockLogger;
using UserService.Models;

namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IFakeLogger _logger;
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;


        public UserRepository(IFakeLogger logger, UserDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }


        public UserReadDto Create(UserCreateDto dto)
        {
            var user = _mapper.Map<User>(dto);

            _context.Users.Add(user);

            _context.SaveChanges();

            _logger.Log("User created!");

            return _mapper.Map<UserReadDto>(user);
        }

        public void Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(e => e.Id == id);

            if (user == null)
                throw new BusinessException("User does not exist");


            _context.Users.Remove(user);

            _context.SaveChanges();
        }

        public List<UserReadDto> Get()
        {
            var users = _context.Users.ToList();

            return _mapper.Map<List<UserReadDto>>(users);
        }

        public UserReadDto Get(int id)
        {
            var user = _context.Users.FirstOrDefault(e => e.Id == id);

            return _mapper.Map<UserReadDto>(user);
        }

        public UserReadDto GetByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(e => e.Email == email);

            return _mapper.Map<UserReadDto>(user);
        }

        public UserReadDto Update(int id, UserCreateDto dto)
        {
            var user = _context.Users.FirstOrDefault(e => e.Id == id);

            if (user == null)
                throw new BusinessException("User does not exist");

            user.Name = dto.Name;
            user.Surname = dto.Surname;
            user.IsPersonal = dto.IsPersonal;
            user.Mobile = dto.Mobile;
            user.Email = dto.Email;
            user.Username = dto.Username;
            user.Password = dto.Password;
            user.City = dto.City;
            user.Address = dto.Address;

            _context.SaveChanges();

            return _mapper.Map<UserReadDto>(user);
        }
    }
}
