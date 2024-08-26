using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeInfo>>(employees);
        }

        public async Task<EmployeeInfo> GetEmployeeByCodeAsync(string employeeCode)
        {
            var employee = await _employeeRepository.GetByCodeAsync(employeeCode);
            return _mapper.Map<EmployeeInfo>(employee);
        }

        public async Task<bool> SaveEmployeeAsync(EmployeeInfo employee)
        {
            var employeeEntity = _mapper.Map<Employee>(employee);
            return await _employeeRepository.SaveEmployeeAsync(employeeEntity);
        }

    }
}
