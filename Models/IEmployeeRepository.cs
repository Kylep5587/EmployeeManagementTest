/****************************************************************
 * Works in conjunction with Employee.cs                        *
 * Provides logic to manage employee details. This file is used *                                  *
 * Interface only contains what operations the repository should*
 *   support - no implementation details.                       *
****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstDemo.Models
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int Id);
        IEnumerable<Employee> GetAllEmployees();
        Employee Add(Employee employee); // implementation defined in MockEmployeeRepository.cs
        Employee Update(Employee employeeChanges);
        Employee Delete(int id);
    }
}
