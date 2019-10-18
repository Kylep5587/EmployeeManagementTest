using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstDemo.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context; // Dependency injection
        private readonly ILogger<SQLEmployeeRepository> _logger;

        /* Constructor
        **********************************/
        public SQLEmployeeRepository(AppDbContext context, ILogger<SQLEmployeeRepository> logger)
        {
            this.context = context;
            _logger = logger;
        }


        /* Adds an Employee
        **********************************/
        public Employee Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }


        /* Deletes an Employee
        **********************************/
        public Employee Delete(int id)
        {
            Employee employee = context.Employees.Find(id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges(); // Executes change on the database
            }
            return employee;
        }


        public IEnumerable<Employee> GetAllEmployees()
        {
            return context.Employees;
        }


        /* Returns Employee data
        **********************************/
        public Employee GetEmployee(int Id)
        {
            _logger.LogTrace("Trace Log");
            _logger.LogDebug("Debug Log");
            _logger.LogInformation("Information Log");
            _logger.LogWarning("Warning Log");
            _logger.LogError("Error Log");
            _logger.LogCritical("CriticalLog");
            return context.Employees.Find(Id);
        }


        /* Updates Employee data
        **********************************/
        public Employee Update(Employee employeeChanges)
        {
            var employee = context.Employees.Attach(employeeChanges);               // Attach the employee object that has the changes
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;    // Tells entity framework that the entity attached has been modified
            context.SaveChanges();                                                  // Entity framework issues the required update statement
            return employeeChanges;
        }
    }
}
