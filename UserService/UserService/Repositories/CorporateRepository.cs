using AutoMapper;
using UserService.CustomException;
using UserService.DTOs.CorporateDTOs;
using UserService.Interfaces;
using UserService.MockLogger;
using UserService.Models;

namespace UserService.Repositories
{
    public class CorporateRepository : ICorporateRepository
    {
        private readonly IFakeLogger _logger;
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;


        public CorporateRepository(IMapper mapper, UserDbContext context, IFakeLogger logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }


        public CorporateReadDto Create(CorporateCreateDto dto)
        {
            var corporate = _mapper.Map<Corporate>(dto);

            var owner = _context.Users.FirstOrDefault(e => e.Id == dto.OwnerId);

            if (owner == null)
                throw new BusinessException("Owner does not exist", 400);

            _context.Corporators.Add(corporate);

            _context.SaveChanges();

            _logger.Log("Corporate created!");

            return _mapper.Map<CorporateReadDto>(corporate);
        }

        public void Delete(int id)
        {
            var corporate = _context.Corporators.FirstOrDefault(e => e.Id == id);

            if (corporate == null)
                throw new BusinessException("Corporate does not exist");


            _context.Corporators.Remove(corporate);

            _context.SaveChanges();
        }

        public List<CorporateReadDto> Get()
        {
            var corporates = _context.Corporators.ToList();

            return _mapper.Map<List<CorporateReadDto>>(corporates);
        }

        public CorporateReadDto Get(int id)
        {
            var corporate = _context.Corporators.FirstOrDefault(e => e.Id == id);

            return _mapper.Map<CorporateReadDto>(corporate);
        }

        public CorporateReadDto Update(int id, CorporateCreateDto dto)
        {
            var corporate = _context.Corporators.FirstOrDefault(e => e.Id == id);

            if (corporate == null)
                throw new BusinessException("Admin does not exist");

            corporate.CreatedAt = dto.CreatedAt;
            corporate.Pib = dto.Pib;
            corporate.CompanyName = dto.CompanyName;
            corporate.CompanyCity = dto.CompanyCity;
            corporate.CompanyAddress = dto.CompanyAddress;
            corporate.CompanyEmail = dto.CompanyEmail;
            corporate.CompanyMobile = dto.CompanyMobile;
            corporate.OwnerId = dto.OwnerId;

            _context.SaveChanges();

            return _mapper.Map<CorporateReadDto>(corporate);
        }
    }
}
