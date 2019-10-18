

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InAndOut.DTO
{
    public class RegisterDTO
    {
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public Nullable<int> SelfId { get; set; }
        public byte[] PictureUrl { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ContactEmail { get; set; }
        public string Country { get; set; }
        public string OperatingSince { get; set; }
        public Nullable<int> NoOfEmployees { get; set; }
        public Nullable<int> NoOfBranches { get; set; }
        public string Description { get; set; }
        public string CreatedEmployersName { get; set; }
        public string CreatedEmployersDesignation { get; set; }
        public string WebsiteURL { get; set; }
        public string ServiceProviding { get; set; }
        public string IDCardNumber { get; set; }
        public string TypeOfIndustry { get; set; }
        public string ContactPhoneNumber { get; set; }
    }
}