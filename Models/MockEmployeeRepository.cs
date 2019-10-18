/*******************************************
 * Provides implementation for             *
 *  IEmployeeRepository.cs                 *
********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstDemo.Models
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() { Id = 1, Name = "Kyle", Department = Dept.IT, Email = "kylep5587@gmail.com" },
                new Employee() { Id = 2, Name = "Tom", Department = Dept.HR, Email = "tom@gmail.com" },
                new Employee() { Id = 3, Name = "Jill", Department = Dept.Payroll, Email = "jill@gmail.com" },
            };
        }


        /* Adds an employee
        **********************************/
        public Employee Add(Employee employee)
        {
            employee.Id = _employeeList.Max(e => e.Id) + 1; // Finds the max ID and increments by 1
            _employeeList.Add(employee);
            return employee;
        }


        /* Deletes an employee
        **********************************/
        public Employee Delete(int id)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }


        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }


        /* Returns employee information
        **********************************/
        public Employee GetEmployee(int Id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == Id); // Returns an employee with the matching Id
        }


        /* Updates employee data 
        **********************************/
        public Employee Update(Employee employeeChanges)
        {
            Employee employee = _employeeList.FirstOrDefault(e => e.Id == employeeChanges.Id);
            if (employee != null)
            {
                employee.Name = employeeChanges.Name;
                employee.Email = employeeChanges.Email;
                employee.Department = employeeChanges.Department;
            }
            return employee;
        }
    }
}
