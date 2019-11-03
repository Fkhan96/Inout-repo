using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using InAndOut.Models;
using InAndOut.Helper.Custom;
using System.Web.Security;
using InAndOut.DTO;
using InAndOut.Helper.General;
using InAndOut.AppCode;

namespace InAndOut.Controllers
{
    public class AccountController : Controller
    {
        public static tblLogin login;
        public ActionResult Index()
        {
            string Username = Request["Usernametxt"];
            string Password = Request["Passwordtxt"];
            if (Username != null && Password != null)
            {
                login = AccountModel.Login(Username, Password);
                if (login.status == "sucess")
                {
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, Username, DateTime.Now, DateTime.Now.AddMinutes(2880), false, login.role, FormsAuthentication.FormsCookiePath);
                    string hash = FormsAuthentication.Encrypt(ticket);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                    Session["login"] = login;
                    if (ticket.IsPersistent)
                    {
                        cookie.Expires = ticket.Expiration;
                    }
                    Response.Cookies.Add(cookie);
                    Response.Redirect("Employee/Index");
                }
                else
                {
                    ViewBag.Validation = "Invalid Credational!";
                }
            }
            return View();
        }
        public ActionResult GetSessionExpiredCode()
        {
            Response.StatusCode = 403;

            return null;
        }
        public void Logout()
        {
            try
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                Response.Redirect("~/");
            }
            catch (Exception ex)
            {

            }
        }

        #region Account commented code
        
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult NotAuthorize()
        {
            return View();
        }
        ////
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<string> Register(RegisterDTO registerDTO)
        {
            DBContext db = new DBContext();
            var user = new user()
            {
                name = registerDTO.CreatedEmployersName,
                password = "admin",
                username = registerDTO.CreatedEmployersName,
                roleID = 1,
                //FK_CompanyID = company.CompanyID

            };
            db.users.Add(user);
            db.SaveChanges();
            var company = new Company()
            {
                City = registerDTO.City,
                Country = registerDTO.Country,
                WebsiteURL = registerDTO.WebsiteURL,
                ServiceProviding = registerDTO.ServiceProviding,
                State = registerDTO.State,
                Name = registerDTO.Name,
                NoOfBranches = registerDTO.NoOfBranches,
                IDCardNumber = registerDTO.IDCardNumber,
                OperatingSince = registerDTO.OperatingSince,
                ContactEmail = registerDTO.ContactEmail,
                ContactPhoneNumber = registerDTO.ContactPhoneNumber,
                CreatedById = user.user_id,
                Description = registerDTO.Description,
                CreatedEmployersDesignation = registerDTO.CreatedEmployersDesignation,
                TypeOfIndustry = registerDTO.TypeOfIndustry,
                NoOfEmployees = registerDTO.NoOfEmployees,
                PackageType = registerDTO.PackageType

            };
            db.Companies.Add(company);
            db.SaveChanges();
            user.FK_CompanyID = company.CompanyID;
            db.SaveChanges();
            var helper = EmailHelper.instance;
            helper.sendWelcomeEmail("InOutCompanys@gmail.com", registerDTO.ContactEmail, registerDTO.Name);
            helper.sendRegisterEmail("InOutCompanys@gmail.com", "InOutCompanys@gmail.com", registerDTO.Name);
            return Common.Serialize(new { index = user.user_id});        }
        
            #endregion
        }
}