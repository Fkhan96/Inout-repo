using InAndOut.DTO;
using InAndOut.Helper.General;
using InAndOut.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InAndOut.Controllers
{
    public class PerformanceController : Controller
    {
        DBContext db;

        // GET: Performance
        public ActionResult Index()
        {
            return View();
        }
        // GET: PerformanceDetails
        public ActionResult Details()
        {
            return View();
        }


        public string GetList()
        {
            return Common.Serialize(PerformanceModel.getEmpList());
        }

        public string GetPerformanceByEmployee(int id)
        {
            return Common.Serialize(PerformanceModel.getPerformanceByEmployeeId(id));
        }

        [HttpPost]
        public async Task<JsonResult> UploadPerformanceDocument()
        {
            var document = new DocumentUpload();

            try
            {
                db = new DBContext();
               
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        var stream = fileContent.InputStream;
                        document.DocumentName = fileContent.FileName;
                        document.ContentType = fileContent.ContentType;
                        using (var reader = new BinaryReader(stream))
                        {
                            document.DocumentEncode = reader.ReadBytes((int)stream.Length);
                        }
                        db.DocumentUploads.Add(document);
                        await db.SaveChangesAsync();
                      
                        db.SaveChanges();

                    }
                }
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Upload failed");
            }

            return Json(document.DocumentUploadID,"File uploaded successfully");
        }
        [HttpPost]
        public async Task<JsonResult> PostPerformanceReport(PerformanceReportDTO performanceReportDTO)
        {
            var performance = new PerformanceReport();


            try
            {
                db = new DBContext();

                performance.FK_EmpID = performanceReportDTO.FK_EmpID;
                performance.FK_QuarterID = performanceReportDTO.FK_QuarterID;
                performance.FK_ReportTypeID = performanceReportDTO.FK_ReportTypeID;
                performance.notes = performanceReportDTO.notes;
                performance.FK_DocumentUploadID = (performanceReportDTO.FK_DocumentUploadID != 0  ? performanceReportDTO.FK_DocumentUploadID : 0);
                performance.year = performanceReportDTO.year;
                performance.FK_ExpectationID = performanceReportDTO.FK_ExpectationID;
                performance.IsDeleted = false;
                performance.CreatedOn = DateTime.Now;
                db.PerformanceReports.Add(performance);
                db.SaveChanges();
            }
            catch (Exception)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return Json(performance.PerformanceReportID, "Performance Report Added");
        }
        
        public HttpResponseBase GetPerformanceReportDocument(int id)
        {
            db = new DBContext();
            var document = db.DocumentUploads.FirstOrDefault(each => each.DocumentUploadID == id);
            byte[] stream = document.DocumentEncode;
            Response.ContentType = document.ContentType;
            Response.AddHeader("Content-Disposition", "attachment; filename=" + document.DocumentName);
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(stream);
            Response.End();
            Response.Close();
            return Response;
        }


        public string DeletePerformanceReport(int id)
        {
            db = new DBContext();
            var PerformanceReports = db.PerformanceReports.FirstOrDefault(each => each.PerformanceReportID == id);
            if (!ReferenceEquals(PerformanceReports,null)) {
                PerformanceReports.IsDeleted = true;
                db.SaveChanges();
                return "Record Deleted";
            }
            return "No Record Found";
        }
      

    }
}