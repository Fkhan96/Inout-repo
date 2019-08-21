using InAndOut.Helper.General;
using InAndOut.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InAndOut.Controllers
{
    public class SettingController : Controller
    {
        // GET: Setting
        public ActionResult Index()
        {
            return View();
        }

        public string GetList(int FK_CompanyID)
        {
            return Common.Serialize(BLLModel.getlist_Setting(FK_CompanyID));
        }

        public string Add(Shift data)
        {
            BLLModel.add_shift(data);
            return Common.Serialize("success");
        }

        public string Edit(Shift data)
        {
            BLLModel.edit_shift(data);
            return Common.Serialize("success");
        }

        public string AddSalary(SalaryDeduction data)
        {
            BLLModel.add_Salary(data);
            return Common.Serialize("success");
        }

        public string EditSalary(SalaryDeduction data)
        {
            BLLModel.edit_Salary(data);
            return Common.Serialize("success");
        }
    }
}