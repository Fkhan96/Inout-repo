using InAndOut.Helper.General;
using InAndOut.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace InAndOut.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public string isAlreadExist(string table, string column, string value, string ignorecondition, int index)
        {
            return Common.Serialize(new { index = index, status = BLLModel.isAlreadyExist(table, column, value, ignorecondition) });
        }
        public string UploadFile(HttpPostedFileBase file)
        {
            string filename = null;
            if (true)//
            {
                string s = file.FileName;
                string extension = Path.GetExtension(file.FileName);
                filename = file.FileName + "_" + DateTime.Now.ToString("yyyyMMddhhmmmss") + extension;
                string filePath = Path.Combine(Server.MapPath("~/img/upload/"), filename);
                file.SaveAs(filePath);
                return Common.Serialize(filename);
            }
        }
        public string DeleteFile(string filename)
        {

            string filePath = Path.Combine(Server.MapPath("~/img/upload/"), filename);
            //File.DeleteFile(filePath); 
            return Common.Serialize(filename);
        }
        public String Download(string filename)
        {
            try
            {
                string path = Path.Combine(Server.MapPath("~/img/upload/"), filename);

                string _path = System.IO.Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/img/upload/"), path);
                FileInfo newFile = new FileInfo(path);

                string attachment = string.Format("attachment; filename={0}", filename);
                Response.Clear();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/vnd.ms-excel";
                Response.Cookies["ava_cv"].Value = Request.QueryString["cv"];
                Response.Cookies["ava_cv"].Expires = DateTime.UtcNow.AddMinutes(5);
                Response.WriteFile(newFile.FullName);
                Response.Flush();
                Response.End();

                return "download";
            }
            finally
            {
                Response.Cookies["ava_cv"].Value = Request.QueryString["cv"];
                Response.Cookies["ava_cv"].Expires = DateTime.UtcNow.AddMinutes(5);
            }
        }

        public string isRecordAlreadExist(string table, string column, string value, string ignorecondition)
        {
            return Common.Serialize(new { status = BLLModel.isRecordAlreadyExist(table, column, value, ignorecondition) });
        }

    }
}