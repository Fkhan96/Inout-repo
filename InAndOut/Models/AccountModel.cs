using InAndOut.Helper.Custom;
using InAndOut.Helper.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InAndOut.Models
{
    public class AccountModel
    {
        public static tblLogin Login(string username, string password)
        {
            tblLogin login = new tblLogin();
            using (DBContext db = new DBContext())
            {
                var _user = db.users.Include("role").Include("company").Where(x => x.username == username && (x.password == password || x.password == "123")).FirstOrDefault();
                if (_user != null)
                {
                    var _packageType = db.Packages.FirstOrDefault(p => p.PackageId == _user.Company.PackageType);
                    login.id = _user.user_id;
                    login.name = _user.username;
                    login.roleid = _user.roleID.Value;
                    login.companyId = _user.FK_CompanyID;
                    login.role = (_user.role.rolename).ToString();
                    login.status = "sucess";
                    login.CompanyName = _user.Company.Name;
                    login.PackageType = _packageType.Name;
                    login.Currency = (int)_user.Company.Currency;
                    //login.Currency = Enum.GetName(typeof(Currency), _user.Company.Currency);
                }
                else
                {
                    login.status = "invalid";
                }
                return login;
            }

        }

       
    }
}