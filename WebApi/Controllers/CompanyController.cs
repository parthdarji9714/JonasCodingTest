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
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }
        // GET api/<controller>
        public async Task<IEnumerable<CompanyDto>> GetAllAsync()
        {
            try
            {
                var items = await _companyService.GetAllCompaniesAsync();
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
                var item = await _companyService.GetCompanyByCodeAsync(companyCode);
                return _mapper.Map<CompanyDto>(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST api/<controller>
        public async void Post([FromBody]string value)
        {
            try
            {
                var companyInfo = _mapper.Map<CompanyInfo>(value);
                var result = await _companyService.SaveCompanyAsync(companyInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // PUT api/<controller>/5
        public async void Put(string id, [FromBody]string value)
        {
            try
            {
                var companyInfo = _mapper.Map<CompanyInfo>(value);
                if(companyInfo.SiteId != id)
                {
                    BadRequest("SiteId is not the same");
                }
                else
                {
                    await _companyService.SaveCompanyAsync(companyInfo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE api/<controller>/5
        public async void Delete(string id)
        {
            try
            {
                await _companyService.DeleteCompanyAsync(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}