/* Contains the data and logic to *
 *  manage employee data          *
**********************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstDemo.Models
{
    public class Employee
    {
        public int Id { get; set; }

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

        public string PhotoPath { get; set; }
    }
}
