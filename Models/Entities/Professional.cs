using System.ComponentModel.DataAnnotations.Schema;
using VVCRUD_IT_CDN_API_NET8.Models.Entities.Base;

namespace VVCRUD_IT_CDN_API_NET8.Models.Entities
{
    [Table("Professional")]
    public class Professional : BaseEntity
    {
        [Column(TypeName = "nvarchar(10)")]
        public required string Username { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public required string Mail { get; set; }

        [Column(TypeName = "nvarchar(15)")]
        public required string PhoneNumber { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public required string Skillset { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? Hobby { get; set; }
    }
}
