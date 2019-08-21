using InAndOut.Helper.Custom;
using InAndOut.Helper.General;
using InAndOut.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InAndOut.Controllers
{
    public class AttendanceController : Controller
    {
        // GET: Attendance
        public ActionResult Index()
        {
            return View();
        }

        #region Get Attendance of current day
        public string GetAttendance(AttendanceSearchViewModel model)
        {
            return Common.Serialize(AttendanceModel.GetAttendanceFilter(model));
        }
        #endregion

        #region Attendance Details by employee id
        public ActionResult AttDetails()
        {
            ViewBag.data = getAttenDetails(Convert.ToInt32(Request["id"]));
            return View();
        }

        public string getAttenDetails(int empId)
        {
            return Common.Serialize(BLLModel.getAttendenceDetails(empId));
        }
        #endregion

        

    }
}