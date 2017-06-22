using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Models
{
    public class User : IdentityUser
    {
        [Required, StringLength(100)]
        public string Name { get; set; }
    }
}
