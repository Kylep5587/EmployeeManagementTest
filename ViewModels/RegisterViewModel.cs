/* ************************************************************
 *  Provides properties for AccountController
 *  **********************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstDemo.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]           //? Masks the password while typing it
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]    //? Modifies the what is displayed by the label
        [Compare("Password", ErrorMessage = "Passwords do not match.")] //? Compares the password and password confirmation and displays error if they don't match
        public string ConfirmPassword { get; set; }
    }
}
