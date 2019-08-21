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
                ViewBag.roleacess = RoleModel.getRoleRights(login.roleid, login);
                ViewBag.HaveAcessToPages = login.MenuRightList.Where(x => x.hasaccess.Value == true).Select(x => new String2 { val1 = x.menuitem_url, val2 = x.menuitem_name }).Distinct().ToList();
                List<UserAuthenticate> MenuRightList = login.MenuRightList;
                string path = "";
                if (MenuRightList != null)
                {
                    foreach (var item in MenuRightList)
                    {
                        if (Convert.ToBoolean(item.hasaccess) && controllerName.ToLower() == item.menuitem_url.ToLower() && actionName.ToLower() == item.menuitem_name.ToLower())
                        {
                            bit = true;
                        }
                    }
                    bit = true;
                    base.Initialize(requestContext);
                    if (bit == false && controllerName.ToLower() != "dashboard")
                    {
                        requestContext.HttpContext.Response.Clear();
                        requestContext.HttpContext.Response.Redirect(Url.Action("Index", "Account"));
                        requestContext.HttpContext.Response.End();
                        return;
                    }
                    else
                    {
                        //ViewBag.pages
                        //var flatlist = MenuRightList.Where(x=>x.menuitem_url!="Payment").Select(x => x.menuitem_url).Distinct().Select(x => new tblPageDetail { page = x, name = Common.getPageName(x), icon = Common.getTextIcon(x), order=Common.getPageOrder(x),parenttitle=Common.ParentTitle(x)}).OrderBy(x=>x.order).ToList();
                    }
                }
            }
        }
        public ActionResult RedirectToLogin()
        {
            return RedirectToAction("Logout", "Account");
        }
    }
}