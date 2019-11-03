using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InAndOut.Helper.Custom
{
    public class login
    {
        public long id { get; set; }        
        public string name { get; set; }
        public string role { get; set; }
        public int roleid { get; set; }
        public string image { get; set; }
        public string status { get; set; }     
        public int type { get; set; }
        public int? companyId { get; set; }
        public string CompanyName { get; set; }
        public string PackageType { get; set; }
        public bool isreaddetail { get; set; }
    }
    public class tblLogin : login
    {
        //public List<UserAuthenticate> MenuRightList { get; set; }
    }
}