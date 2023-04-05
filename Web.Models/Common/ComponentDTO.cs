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
    public class ComponentDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ComponentId { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string ComModuleName { get; set; }
        public int ParentComponentId { get; set; }
        [Column(TypeName = "int")]
        public int CreatedBy { get; set; }
       
        [Column(TypeName = "int")]
        public int ModifiedBy { get; set; }
        
        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string PageUrl { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string PageName { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string PageTitle { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string PageDescription { get; set; }

        public int SortOrder { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string ModuleImage { get; set; }
    }
}
