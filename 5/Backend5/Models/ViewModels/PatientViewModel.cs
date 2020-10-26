using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend5.Models.ViewModels
{
    public class PatientViewModel
    {
        [Required]
        [MaxLength(200)]
        public String Name { get; set; }

        [MaxLength(300)]
        public String Address { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [MaxLength(6)]
        public String Gender { get; set; }
    }
}
