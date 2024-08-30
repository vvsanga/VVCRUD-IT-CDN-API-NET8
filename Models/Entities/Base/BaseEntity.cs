using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VVCRUD_IT_CDN_API_NET8.Models.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreateDateTime { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreateDateTime = DateTime.Now;
        }
    }
}
