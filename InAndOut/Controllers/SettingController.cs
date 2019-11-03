using InAndOut.AppCode;
using InAndOut.DTO;
using InAndOut.Helper.General;
using InAndOut.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InAndOut.Controllers
{
    public class SettingController : BaseController
    {
        // GET: Setting
        public ActionResult Index()
        {
            var login = Session["login"];
            return View();
        }

        public object GetShift(int FK_CompanyID)
        {
            return SettingModel.getShiftSetting(FK_CompanyID);
        }

        public object GetSalaryDeduction(int FK_CompanyID)
        {
            return SettingModel.getSalaryDeductionSetting(FK_CompanyID);
        }

        #region Shift Settings
        public async Task<string> Add(ShiftDTO data)
        {
            await BLLModel.add_shiftAsync(data);
            return Common.Serialize("success");
        }

        public async Task<string> Edit(ShiftDTO data)
        {
            await BLLModel.edit_shiftAsync(data);
            return Common.Serialize("success");
        }
         #endregion

        #region Salary Deduction
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
        #endregion
    }
}