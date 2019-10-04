using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISOCS.Models
{
    public class RegisterModel
    {

        [EmailAddress(ErrorMessage = "The Email Address is not valid")]
        [Required(ErrorMessage = "Voer een email adres in.")]
        [Display(Name = "Email Address:")]
        public string email { get; set; }


        [Required(ErrorMessage = "Voer een wachtwoord in.")]
        [Display(Name = "Wachtwoord:")]
        [MaxLength(32, ErrorMessage = "Wachtwoord mag maximaal 32 tekens lang zijn")]
        [MinLength(4, ErrorMessage = "Wachtwoord moet minimaal 4 tekens lang zijn")]
        [RegularExpression(@"^.*(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\@#$%^&+=]).*$", ErrorMessage = "Je wachtwoord moet mininaal 1 nummer, 1 hoofdletter, 1 kleine letter en een speciaal teken bevatten")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "De wachtwoorden komen niet overeen.")]
        [Display(Name = "Herhaal je Wachtwoord:")]
        public string password2 { get; set; }

        [Display(Name = "Voornaam:")]
        public string firstname { get; set; }

        [Display(Name = "Tussenvoegsel:")]
        public string preposition { get; set; }

        [Display(Name = "Achternaam:")]
        public string lastname { get; set; }
    }
}
