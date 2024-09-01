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
        public async Task<IEnumerable<ProfessionalView>> GetAllAsync(int pageNo, int pageSize)
        {
            var queryable = _dbContext.Professionals
                .Include(p => p.Skillset) // Include Skillset
                .AsQueryable();

            var professionalViewDtos = await queryable
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Select(professional => _mapper.Map<ProfessionalView>(professional))
                .ToListAsync();

            return professionalViewDtos;
        }
        public async Task<ProfessionalView> GetByIdAsync(Guid id)
        {
            var professional = await _dbContext.Professionals
                                                .Include(p => p.Skillset)
                                                .FirstOrDefaultAsync(p => p.Id == id);
            var professionalViewDto = _mapper.Map<ProfessionalView>(professional);
            return professionalViewDto;
        }
        public async Task<ProfessionalView> CreateAsync(ProfessionalCreate professionalCreateDto)
        {
            var professional = new Professional()
            {
                Username = professionalCreateDto.Username,
                Mail = professionalCreateDto.Mail,
                PhoneNumber = professionalCreateDto.PhoneNumber,
                Hobby = professionalCreateDto.Hobby,
                Skillset = professionalCreateDto.Skillset
                                                .Select(skill => new Skillset
                                                {
                                                    Name = skill.Name
                                                }).ToList()
            };

            _dbContext.Professionals.Add(professional);
            await _dbContext.SaveChangesAsync();

            var professionalViewDto = _mapper.Map<ProfessionalView>(professional);

            return professionalViewDto;
        }
        public async Task<ProfessionalView> UpdateAsync(Guid id, ProfessionalUpdate updateProfessionalDto)
        {
            // Load the Professional entity including the Skillset collection
            var professional = await _dbContext.Professionals
                .Include(p => p.Skillset)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (professional == null)
            {
                return null;
            }

            // Update the Professional entity properties
            professional.Mail = updateProfessionalDto.Mail;
            professional.PhoneNumber = updateProfessionalDto.PhoneNumber;
            professional.Hobby = updateProfessionalDto.Hobby;

            // Extract current and new skill names
            var currentSkills = professional.Skillset.Select(s => s.Name).ToList();
            var updatedSkills = updateProfessionalDto.Skillset.Select(s => s.Name).ToList();

            // Identify skills to remove
            var skillsToRemove = currentSkills.Except(updatedSkills).ToList();
            // Remove skills from database
            var skillsToRemoveEntities = professional.Skillset
                .Where(s => skillsToRemove.Contains(s.Name))
                .ToList();
            // Set Db
            if (skillsToRemoveEntities.Count > 0)
            {
                _dbContext.Skillsets.RemoveRange(skillsToRemoveEntities);
            }
            await _dbContext.SaveChangesAsync();

            // Identify skills to add
            var skillsToAdd = updatedSkills.Except(currentSkills).ToList();
            // Add new skills to the database
            if (skillsToAdd.Count > 0)
            {
                foreach (var skillName in skillsToAdd)
                {
                    var newSkill = new Skillset
                    {
                        Name = skillName,
                        ProfessionalId = professional.Id
                    };
                    _dbContext.Skillsets.Add(newSkill);
                }
                // Save changes to the database
                await _dbContext.SaveChangesAsync();
            }

            // Map the updated Professional entity to a ProfessionalView DTO
            var professionalViewDto = _mapper.Map<ProfessionalView>(professional);

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
