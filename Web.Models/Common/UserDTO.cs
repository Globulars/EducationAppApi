using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.Common
{
    public class UserDTO
    {
        public int UserId { get; set; }
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string UserName { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string FirstName { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string LastName { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string FullName { get; set; }
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string Password { get; set; }
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string Email { get; set; }
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string Phone { get; set; }
        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Address { get; set; }

        [Column(TypeName = "int")]
        public int CreatedBy { get; set; }

        public int ModifiedBy { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int RoleId { get; set; }
        public string Role { get; set; }

        public class UserPasswordDTO
        {
            public int UserId { get; set; }
            public string OldPassword { get; set; }
            public string NewPassword { get; set; }
        }



    }
}
