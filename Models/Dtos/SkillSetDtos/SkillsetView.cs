using System.ComponentModel.DataAnnotations;

namespace VVCRUD_IT_CDN_API_NET8.Models.Dtos.SkillsetDtos
{
    public class SkillsetView
    {
        public Guid Id { get; set; }

        [StringLength(30)]
        public required string Name { get; set; }

        public Guid ProfessionalId { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
