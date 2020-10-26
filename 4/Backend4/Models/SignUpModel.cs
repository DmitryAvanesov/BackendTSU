using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend4.Models
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "You should enter your first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You should enter your last name")]
        public string LastName { get; set; }

        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }

        [Required(ErrorMessage = "You should define your gender (yes, we have only 2, I'm sorry...)")]
        public string Gender { get; set; }

        public bool Existed { get; set; }
    }
}