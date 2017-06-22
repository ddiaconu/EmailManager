using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.ViewModels
{
    public class EnEventsViewModel
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public DateTime EventDate { get; set; }
    }
}
