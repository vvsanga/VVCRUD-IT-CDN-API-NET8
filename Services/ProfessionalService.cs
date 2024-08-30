using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VVCRUD_IT_CDN_API_NET8.Data;
using VVCRUD_IT_CDN_API_NET8.Models.Dtos.ProfessionalDtos;
using VVCRUD_IT_CDN_API_NET8.Models.Entities;
using VVCRUD_IT_CDN_API_NET8.Services.Interface;

namespace VVCRUD_IT_CDN_API_NET8.Services
{
    public class ProfessionalService : IProfessionalService
    {

        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProfessionalService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProfessionalViewDto>> GetAllAsync(int pageNo, int pageSize)
        {
            var queryable = _dbContext.Professionals.AsQueryable();
            var professionalViewDtos = await queryable
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Select(professional => _mapper.Map<ProfessionalViewDto>(professional))
                .ToListAsync();

            return professionalViewDtos;
        }
        public async Task<ProfessionalViewDto> GetByIdAsync(Guid id)
        {
            var professional = await _dbContext.Professionals.FindAsync(id);
            var professionalViewDto = _mapper.Map<ProfessionalViewDto>(professional);
            return professionalViewDto;
        }

        public async Task<ProfessionalViewDto> CreateAsync(ProfessionalCreateDto professionalCreateDto)
        {
            var professional = new Professional()
            {
                Username = professionalCreateDto.Username,
                Mail = professionalCreateDto.Mail,
                PhoneNumber = professionalCreateDto.PhoneNumber,
                Hobby = professionalCreateDto.Hobby,
                Skillset = professionalCreateDto.Skillset
            };

            _dbContext.Professionals.Add(professional);
            await _dbContext.SaveChangesAsync();

            var professionalViewDto = _mapper.Map<ProfessionalViewDto>(professional);

            return professionalViewDto;
        }
        public async Task<ProfessionalViewDto> UpdateAsync(Guid id, ProfessionalUpdateDto updateProfessionalDto)
        {
            var professional = await _dbContext.Professionals.FindAsync(id);

            if (professional is null)
            {
                return null;
            }

            professional.Mail = updateProfessionalDto.Mail;
            professional.PhoneNumber = updateProfessionalDto.PhoneNumber;
            professional.Hobby = updateProfessionalDto.Hobby;
            professional.Skillset = updateProfessionalDto.Skillset;

            await _dbContext.SaveChangesAsync();

            var professionalViewDto = _mapper.Map<ProfessionalViewDto>(professional);

            return professionalViewDto;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var professional = await _dbContext.Professionals.FindAsync(id);
            if (professional == null)
            {
                return false; // Or throw a NotFoundException
            }

            _dbContext.Professionals.Remove(professional);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
