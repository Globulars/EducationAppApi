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
    public class CourseDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CourseId { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string CourseName { get; set; }
        [Column(TypeName = "int")]
        public int CreatedBy { get; set; }
       
        [Column(TypeName = "int")]
        public int ModifiedBy { get; set; }
        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }
    }
}
