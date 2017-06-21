using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EmailManager.Models
{
    public class BaseEntity
    {
        [Required]
        public int Id { get; set; }

    }
}
