using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace InAndOut.Models
{
    public class EmailHelper
    {
        public static readonly EmailHelper instance = new EmailHelper();
        public EmailHelper GetInstance()
        {
            return instance;
        }
        private EmailHelper() { }
        public bool sendEmail(string from, string to, string body, string subject)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.IsBodyHtml = true;
                mail.From = new MailAddress(from);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("inout.companys@gmail.com", "Character12");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception e) { return false; }
        }

        public bool sendWelcomeEmail(string from, string to, string company) {
            string message = "<html><head></head><body><div><div><title>Welcome To InOut System</title><div><span></span><table border=\"0\" cellpadding=\"0\" width=\"100 % \" cellspacing=\"0\" align=\"center\"><tbody><tr><td align=\"center\"><table border=\"0\" cellpadding=\"0\" width=\"100 % \" cellspacing=\"0\" style=\"max - width:900px; \" align=\"center\"><tbody><tr><td style=\"padding: 0px 80px; \" align=\"center\"><table border=\"0\" bgcolor=\"#f9fafe\" cellpadding=\"0\" width=\"100%\" cellspacing=\"0\" style=\"max-width:900px;background-color:#f9fafe;\" align=\"center\"><tbody><tr><td align=\"center\"><table border=\"0\" cellpadding=\"0\" width=\"585\" cellspacing=\"0\" align=\"center\"><tbody><tr><td style=\"font-family:'Open Sans', Arial, sans-serif;font-size:15px;line-height:21px;text-align:center;padding:40px 20px 45px;color:#434A54;\" align=\"center\">Welcome To InOut System  <br>your company " +company + " has been registered <br> thanks for joining our application<br><br></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></div></div></div></body></html>";
            string subject = "Register Successfull InOut";
            return sendEmail(from, to, message, subject);
        }
        public bool sendRegisterEmail(string from, string to,string company)
        {
            string message = "<html><head></head><body><div><div><title>Welcome To InOut System</title><div><span></span><table border=\"0\" cellpadding=\"0\" width=\"100 % \" cellspacing=\"0\" align=\"center\"><tbody><tr><td align=\"center\"><table border=\"0\" cellpadding=\"0\" width=\"100 % \" cellspacing=\"0\" style=\"max - width:900px; \" align=\"center\"><tbody><tr><td style=\"padding: 0px 80px; \" align=\"center\"><table border=\"0\" bgcolor=\"#f9fafe\" cellpadding=\"0\" width=\"100%\" cellspacing=\"0\" style=\"max-width:900px;background-color:#f9fafe;\" align=\"center\"><tbody><tr><td align=\"center\"><table border=\"0\" cellpadding=\"0\" width=\"585\" cellspacing=\"0\" align=\"center\"><tbody><tr><td style=\"font-family:'Open Sans', Arial, sans-serif;font-size:15px;line-height:21px;text-align:center;padding:40px 20px 45px;color:#434A54;\" align=\"center\"> " + company + " has signed up in your InOut System<br><br></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></td></tr></tbody></table></div></div></div></body></html>";
            string subject = "Register Successfull InOut";
            return sendEmail(from, to, message, subject);
        }
    }
}