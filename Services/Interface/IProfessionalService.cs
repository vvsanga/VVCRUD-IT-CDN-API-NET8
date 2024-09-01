using VVCRUD_IT_CDN_API_NET8.Models.Dtos.ProfessionalDtos;

namespace VVCRUD_IT_CDN_API_NET8.Services.Interface
{
    public interface IProfessionalService
    {
        Task<IEnumerable<ProfessionalView>> GetAllAsync(int pageNo, int pageSize);
        Task<ProfessionalView> GetByIdAsync(Guid id);
        Task<ProfessionalView> CreateAsync(ProfessionalCreate professionalCreateDto);
        Task<ProfessionalView> UpdateAsync(Guid id, ProfessionalUpdate professionalUpdateDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
