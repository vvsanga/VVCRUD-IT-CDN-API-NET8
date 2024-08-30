using System.ComponentModel.DataAnnotations;

namespace VVCRUD_IT_CDN_API_NET8.Models.Dtos.ProfessionalDtos
{
    public class ProfessionalUpdateDto
    {
        [StringLength(50)]
        public required string Mail { get; set; }

        [StringLength(15)]
        public required string PhoneNumber { get; set; }

        [StringLength(500)]
        public required string Skillset { get; set; }

        [StringLength(100)]
        public string? Hobby { get; set; }
    }
}
