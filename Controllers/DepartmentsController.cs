using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FirstDemo.Controllers
{
    public class DepartmentsController : Controller
    {
        // Accessed at localhost/departments/departments
        public string List()
        {
            return "List() of DepartmentsController";
        }


        // Accessed at localhost/departments/details
        public string Details()
        {
            return "Details() of DepartmentsController";
        }
    }
}