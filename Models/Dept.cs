/***********************************************
 * Provides enumeration of company departments *
 *  for use in HomeController.cs -> Create()   *
***********************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstDemo.Models
{
    public enum Dept
    {
        None,
        HR,
        IT,
        Payroll
    }
}
