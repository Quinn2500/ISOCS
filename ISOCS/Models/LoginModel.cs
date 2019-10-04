using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISOCS.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Voer een email in.")]
        [Display(Name = "Email:")]
        public string email { get; set; }

        [Required(ErrorMessage = "Voer een wachtwoord in.")]
        [Display(Name = "Wachtwoord:")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
