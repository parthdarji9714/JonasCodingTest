using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WebApi.Models
{
    public class EmployeeDto
    {
        public string SiteId { get; set; }
        public string CompanyCode { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Occupation { get; set; }
        public string EmployeeStatus { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public DateTime LastModified { get; set; }
    }
}