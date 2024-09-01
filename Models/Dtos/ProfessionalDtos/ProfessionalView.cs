using System.ComponentModel.DataAnnotations;
using VVCRUD_IT_CDN_API_NET8.Models.Dtos.SkillsetDtos;

namespace VVCRUD_IT_CDN_API_NET8.Models.Dtos.ProfessionalDtos
{
    public class ProfessionalView
    {
        [StringLength(40)]
        public string? Id { get; set; }

        [StringLength(10)]
        public required string Username { get; set; }

        [StringLength(50)]
        public required string Mail { get; set; }

        [StringLength(15)]
        public required string PhoneNumber { get; set; }

        [StringLength(100)]
        public string? Hobby { get; set; }

        public DateTime CreateDateTime { get; set; }

        public ICollection<SkillsetView> Skillset { get; set; } = new List<SkillsetView>();
    }
}
