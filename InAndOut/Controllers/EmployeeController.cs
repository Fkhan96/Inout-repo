using InAndOut.AppCode;
using InAndOut.Helper.Custom;
using InAndOut.Helper.General;
using InAndOut.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace InAndOut.Controllers
{
    public class EmployeeController : BaseController
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public string GetList()
        {
            return Common.Serialize(BLLModel.getlist_Employee());
        }

        public string Delete(int id)
        {
            BLLModel.delete_Employee(id);
            return Common.Serialize("success");
        }

        public string Add(Employee data)
        {
            BLLModel.add_employee(data);
            return Common.Serialize("success");
        }

        #region EmployeeDetails
        public ActionResult Details()
        {
            ViewBag.data = getDetails(Convert.ToInt32(Request["id"]));
            return View();
        }

        public string getDetails(int empId)
        {
            return Common.Serialize(BLLModel.getEmployeeDetails(empId));
        }

        public string Edit(Employee data)
        {
            BLLModel.update_EmployeeDetails(data);
            return Common.Serialize("success");
        }
        #endregion

    }
}