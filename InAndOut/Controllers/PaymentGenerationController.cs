using DocumentFormat.OpenXml.Wordprocessing;
using InAndOut.AppCode;
using InAndOut.Helper.Custom;
using InAndOut.Helper.General;
using InAndOut.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace InAndOut.Controllers
{
    public class PaymentGenerationController : BaseController
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

        public HttpResponseBase GeneratePaySlip(PaymentSearchViewModel model) {
            
            PdfGeneration pdfGeneration = new PdfGeneration();
            var bytes = pdfGeneration.GenerateReport(model);
            Response.Clear();
            Response.ContentType = "application/pdf";
            //Response.AddHeader("Content-Disposition", "attachment; filename=PaySlip.pdf");
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
            Response.Close();
            return Response;
        }

    }
}