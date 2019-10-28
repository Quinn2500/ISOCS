using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISOCS.Models.AdminViewModels
{
    public class ChangeUserViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Voornaam")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Achternaam")]
        public string Lastname { get; set; }

        [Required]
        [Display(Name = "Tussenvoegsel")]
        public string Preposition { get; set; }
    }
}
