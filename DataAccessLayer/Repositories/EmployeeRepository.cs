using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbWrapper<Employee> _employeeDbWrapper;

        public EmployeeRepository(IDbWrapper<Employee> employeeDbWrapper)
        {
            _employeeDbWrapper = employeeDbWrapper;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeDbWrapper.FindAllAsync();
        }

        public async Task<Employee> GetByCodeAsync(string employeeCode)
        {
            var employee = await _employeeDbWrapper.FindAsync(t => t.EmployeeCode.Equals(employeeCode));
            return employee?.FirstOrDefault();
        }

        public async Task<bool> SaveEmployeeAsync(Employee employee)
        {
            var existingEmployee = (await _employeeDbWrapper.FindAsync(e => e.EmployeeCode.Equals(employee.EmployeeCode)))?.FirstOrDefault();
            if (existingEmployee != null)
            {
                existingEmployee.EmployeeName = employee.EmployeeName;
                existingEmployee.Occupation = employee.Occupation;
                existingEmployee.EmployeeStatus = employee.EmployeeStatus;
                existingEmployee.EmailAddress = employee.EmailAddress;
                existingEmployee.Phone = employee.Phone;
                existingEmployee.LastModified = employee.LastModified;
                return _employeeDbWrapper.Update(existingEmployee);
            }
            return _employeeDbWrapper.Insert(employee);
        }

    }
}
