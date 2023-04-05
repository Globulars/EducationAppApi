using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.DTO.Common
{
    public class ComponentAccessDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ComponentAccessId { get; set; }

        [ForeignKey("ComponentIdFK")]
        public int ComponentIdFK { get; set; }
        [ForeignKey("RoleIdFK")]
        public int RoleIdFK { get; set; }
        [Column(TypeName = "int")]
        public int CreatedBy { get; set; }
        [Column(TypeName = "DateTime")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "int")]
        public int ModifiedBy { get; set; }
        [Column(TypeName = "DateTime")]
        public DateTime? ModifiedDate { get; set; }
        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }
    }
}
