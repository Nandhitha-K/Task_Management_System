using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLibrary.Model
{
        public class Users
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int UserId { get; set; }

            [StringLength(30)]
            [Required(ErrorMessage = "Name is required")]
            public string UserName { get; set; }

            [StringLength(10)]
            [Required(ErrorMessage = "Password is required")]
            public string Password { get; set; }

        
            [Required(ErrorMessage = "Email is required")]
            [EmailAddress(ErrorMessage = "Invalid email format")]
            public string? Email { get; set; }

        }
}
