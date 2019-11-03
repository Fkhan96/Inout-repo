using InAndOut.Helper.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using InAndOut.Models;

namespace InAndOut.AppCode
{
    public class BaseController : Controller
    {
        // GET: Base
        public tblLogin login { get; set; }
        string SESSION_USERKEY = "login";
        Dictionary<string, List<string>> PackageType = new Dictionary<string, List<string>>() {
            { "Silver", new List<string>() { "Employee", "Attendance", "PaymentGeneration" } },
            {"Bronze", new List<string>() { "Employee","Attendance" } },
            { "Gold", new List<string>() { "Employee", "Attendance", "PaymentGeneration","Setting","Performance" } }
        } ;
        
        protected override void Initialize(RequestContext requestContext)
        {
            string RequestPath = requestContext.HttpContext.Request.FilePath;
            string actionName = requestContext.RouteData.Values["action"].ToString();
            string controllerName = requestContext.RouteData.Values["controller"].ToString();
            login = requestContext.HttpContext.Session[SESSION_USERKEY] as tblLogin;
            bool bit = false;
            if (login == null)
            {
                base.Initialize(requestContext);
                string _actionName = requestContext.RouteData.Values["action"].ToString();
                if (!requestContext.HttpContext.Request.IsAjaxRequest())
                {
                    requestContext.HttpContext.Response.Clear();
                    requestContext.HttpContext.Response.Redirect(Url.Action("Logout", "Account"));
                    requestContext.HttpContext.Response.End();
                }
                else
                {
                    //For Ajax Call's
                    requestContext.HttpContext.Response.Clear();
                    requestContext.HttpContext.Response.Redirect(Url.Action("GetSessionExpiredCode", "Account"));
                    requestContext.HttpContext.Response.End();
                }
                return;
            }
            else
            {
                if (requestContext.HttpContext.Request.IsAjaxRequest())
                {
                    //return  ajax request without validating for performance
                    base.Initialize(requestContext);
                    return;
                }

                base.Initialize(requestContext);
                ViewBag.username = login.name;
                ViewBag.role = login.role;
                ViewBag.roleid = login.roleid;
                ViewBag.userid = login.id;
                ViewBag.type = login.type;
                var package = login.PackageType;
                if (!PackageType[package].Contains(controllerName)) {
                    requestContext.HttpContext.Response.Redirect(Url.Action("NotAuthorize", "Account"));
                }
            }
        }
        public ActionResult RedirectToLogin()
        {
            return RedirectToAction("Logout", "Account");
        }
    }
}