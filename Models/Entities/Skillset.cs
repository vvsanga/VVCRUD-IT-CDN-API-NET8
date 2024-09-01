using System.ComponentModel.DataAnnotations.Schema;
using VVCRUD_IT_CDN_API_NET8.Models.Entities.Base;

namespace VVCRUD_IT_CDN_API_NET8.Models.Entities
{
    [Table("Skillset")]
    public class Skillset : BaseEntity
    {
        [Column(TypeName = "nvarchar(30)")]
        public required string Name { get; set; }

        // Foreign key property
        public Guid ProfessionalId { get; set; }

        // Navigation property
        [ForeignKey("ProfessionalId")]
        public virtual Professional? Professional { get; set; }
    }
}
