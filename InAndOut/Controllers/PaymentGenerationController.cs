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
    public class PaymentGenerationController : Controller
    {
        // GET: PaymentGeneration
        public ActionResult Index()
        {
            return View();
        }

        #region Get Employee for PaymentGeneration
        public string GetList()
        {
            return Common.Serialize(BLLModel.getEmployees());
        }
        #endregion

        #region Get Attendance Details against Employee
        public ActionResult Details()
        {
            return View();
        }

        #region Get Payment Details for current Month By Date filter
        public string GetPaymentDetails(PaymentSearchViewModel model)
        {
            return Common.Serialize(PaymentModel.GetPaymentDateFilter(model));
        }
        #endregion
        #endregion

    }
}