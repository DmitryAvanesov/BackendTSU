using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend4.Models
{
    public class SignUpCredentialsModel : SignUpModel
    {
        [Required(ErrorMessage = "You should enter your e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You should enter your password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "You should confirm your password")]
        [Compare("Password", ErrorMessage = "Passwords must match!")]
        public string PasswordConfirm { get; set; }

        public bool Remembered { get; set; }
    }
}
