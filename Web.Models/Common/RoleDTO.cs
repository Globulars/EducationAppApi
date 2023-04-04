﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.DTO.Common
{
    public class RoleDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int RoleId { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Role { get; set; }
        [Column(TypeName = "int")]
        public int CreatedBy { get; set; }
       
        [Column(TypeName = "int")]
        public int ModifiedBy { get; set; }
       
        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }
    }
}
