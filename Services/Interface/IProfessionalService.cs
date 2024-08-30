using VVCRUD_IT_CDN_API_NET8.Models.Dtos.ProfessionalDtos;

namespace VVCRUD_IT_CDN_API_NET8.Services.Interface
{
    public interface IProfessionalService
    {
        Task<IEnumerable<ProfessionalViewDto>> GetAllAsync(int pageNo, int pageSize);
        Task<ProfessionalViewDto> GetByIdAsync(Guid id);
        Task<ProfessionalViewDto> CreateAsync(ProfessionalCreateDto professionalCreateDto);
        Task<ProfessionalViewDto> UpdateAsync(Guid id, ProfessionalUpdateDto professionalUpdateDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
