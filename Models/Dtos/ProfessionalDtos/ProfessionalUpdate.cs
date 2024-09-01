using System.ComponentModel.DataAnnotations;
using VVCRUD_IT_CDN_API_NET8.Models.Dtos.SkillsetDtos;

namespace VVCRUD_IT_CDN_API_NET8.Models.Dtos.ProfessionalDtos
{
    public class ProfessionalUpdate
    {
        [StringLength(50)]
        public required string Mail { get; set; }

        [StringLength(15)]
        public required string PhoneNumber { get; set; }

        [StringLength(100)]
        public string? Hobby { get; set; }

        public List<SkillsetCreate> Skillset { get; set; } = new List<SkillsetCreate>();
    }
}
