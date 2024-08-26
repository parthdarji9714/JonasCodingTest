using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using Microsoft.AspNetCore.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }
        // GET api/<controller>
        public async Task<IEnumerable<CompanyDto>> GetAllAsync()
        {
            try
            {
                var items = await _employeeService.GetAllEmployeesAsync();
                return _mapper.Map<IEnumerable<CompanyDto>>(items);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/<controller>/5
        public async Task<CompanyDto> GetAsync(string companyCode)
        {
            try
            {
                var item = await _employeeService.GetEmployeeByCodeAsync(companyCode);
                return _mapper.Map<CompanyDto>(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}