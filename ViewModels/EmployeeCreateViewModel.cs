/* ***********************************************
 * Handles file upload. Allows access to the     *
 * FileName() and CopyTo() methods of IFormFile  *
 * class. Done in this file to avoid storing all *
 * of the file info in the database as would be  *
 * the case if we used Employee.cs               *
*************************************************/
using FirstDemo.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstDemo.ViewModels
{
    public class EmployeeCreateViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }


        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Invalid Email Format")]
        [Display(Name = "Office Email")]
        public string Email { get; set; }


        [Required]
        public Dept? Department { get; set; } //? Enum is required by default. Using ? after makes it optional. [Required] is then added to allow the proper error to be displayed if it is blank

        public IFormFile Photo { get; set; } //? IFormFile allows access to the upload form through model binding

        // public List<IFormFile> Photos { get; set; } // Used for multiple file upload
    }
}
