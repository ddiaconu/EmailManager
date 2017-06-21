using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Models
{
    public class ApplicationUser : BaseEntity
    {
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
