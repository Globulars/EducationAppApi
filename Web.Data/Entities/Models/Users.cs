using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Data.Entities.Models
{
    public class Users
    {
        

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
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
        [Column(TypeName = "DateTime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "int")]
        public int ModifiedBy { get; set; }
        [StringLength(100)]
        [Column(TypeName = "DateTime")]
        public DateTime? ModifiedDate { get; set; }
        [Column(TypeName = "bit")]
        public bool? IsActive { get; set; }

       

        

        

        





    }
}
