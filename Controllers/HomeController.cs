// The HomeController is the default controller that ASP.NET Core looks for
using FirstDemo.Models;
using FirstDemo.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FirstDemo.Controllers
{
    public class HomeController : Controller //? Extending the Controller base class allows us to return a view or json data
    {
        private readonly IEmployeeRepository _employeeRepository; //? Good practice to make readonly
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger _logger;

        //? The constructor is used to inject the IEmployeeRepository service (aka constructor injection)
        //? The HomeController has a dependency on the IEmployeeRepository service
        public HomeController(IEmployeeRepository employeeRepository, 
                                IHostingEnvironment hostingEnvironment,
                                ILogger<HomeController> logger) //? Logging is happening from HomeController, so the generic parameter must be set as such.
        {
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment; // Needed for acquiring the file path of uploaded files in Create() action method
            _logger = logger;
        }


        // Index() indicates the default action - accessed at localhost/home
        public ViewResult Index()
        {
            var model = _employeeRepository.GetAllEmployees();
            return View(model);
        }


        // Accessed at localhost/home/details/{id}
        public ViewResult Details(int? id)
        {
            _logger.LogTrace("Trace Log");
            _logger.LogDebug("Debug Log");
            _logger.LogInformation("Information Log");
            _logger.LogWarning("Warning Log");
            _logger.LogError("Error Log");
            _logger.LogCritical("CriticalLog");
            Employee employee = _employeeRepository.GetEmployee(id.Value);

            // Handle invalid employee ID
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = employee,  
                PageTitle = "Employee Details"
            };
            return View(homeDetailsViewModel);
        }


        // Accessed at localhost/home/create
        // Points to Create.cshtml
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }


        [HttpPost] // Tells the browser that this method is used to respond to post request
        public IActionResult Create(EmployeeCreateViewModel model) //? IActionResult allows the method to return any action result type
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);

                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };

                _employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id }); // Sends the new employee information to the Details(id) action method
            }

            return View(); // Redisplay form if validation fails
        }



        /* Edit Employee -Get- Action Method
        **********************************/
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id); // Get employee information for employee with ID passed by the URL
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel // Create instance of viewModel based on employee data
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };

            return View(employeeEditViewModel);
        }


        /* Edit Employee -Post- Action Method
        **********************************/
        [HttpPost] 
        public IActionResult Edit(EmployeeEditViewModel model) //? IActionResult allows the method to return any action result type
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id); //? Sent from hidden input field in Edit view
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                
                // Upload new photo if user selects a new photo
                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null) // Check for existing photo
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath); // Combine the three strings into a URL to get complete physical path of existing photo
                        System.IO.File.Delete(filePath); 
                    }
                    employee.PhotoPath = ProcessUploadedFile(model);
                }

                _employeeRepository.Update(employee); // Entity framework Update() method generates SQL automatically
                return RedirectToAction("index"); // Returns user to the index action
            }

            return View(); // Redisplay form if validation fails
        }


        /* Creates a unique image file name and moves the file to a specified path
         * Used by Create and Edit action methods
        **********************************/
        private string ProcessUploadedFile(EmployeeCreateViewModel model) // Type is EmployeeCreateViewModel because EmployeeEditViewModel also inherits from this VM, allowing both action methods to use it
        {
            string uniqueFileName = null;
            if (model.Photo != null) // Copy file if one has been selected
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName; //? Global Unique Identifier. Generates unique character sequence. Used to prevent file name conflict
                string filePath = Path.Combine(uploadsFolder, uniqueFileName); // Combine the path with the filename

                // Using block ensures the FileStream is disposed of
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream); // Moves the file to the specified path
                }
            }

            return uniqueFileName;
        }
    }
}
