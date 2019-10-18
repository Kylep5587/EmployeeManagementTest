/* *************************************************
 * Provides data necessary for editing an employee.*
 * Used by Edit.cshtml view                        *
 * Inherits from EmployeeCreateViewModel.cs to     *
 *  provide properties: Name, email, department,   *
 *  and photo.                                     *
***************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstDemo.ViewModels
{
    public class EmployeeEditViewModel: EmployeeCreateViewModel
    {
        public int Id { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}
