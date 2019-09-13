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

        #region Get Attendance Details against Employee
        public ActionResult AttDetails()
        {
            return View();
        }

        #region Get Attendance Details for current Month By Date filter
        public string GetAttendanceDetails(PaymentSearchViewModel model)
        {
            return Common.Serialize(PaymentModel.GetPaymentDateFilter(model));
        }
        #endregion
        #endregion

    }
}