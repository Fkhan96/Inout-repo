using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using InAndOut.Helper.Custom;
using System.Text;

namespace InAndOut.Models
{
    public class PdfGeneration
    {
        public byte[] GenerateReport(PaymentSearchViewModel model)
        {
            DBContext db = new DBContext();
            var emp = db.Employees.First(each=> each.EmpID == model.EmpID);
            var comp = db.Companies.First(each => each.CompanyID == emp.FK_CompanyID);
            Document document = new Document(PageSize.A4, 88f, 88f,35f, 15f);
            Font NormalFont = FontFactory.GetFont("Arial", 14, Font.NORMAL, Color.BLACK);
            using (MemoryStream memoryStream = new MemoryStream())
            {

                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable table = null;
                Color color = null;

                document.Open();

                //Header Table
                table = new PdfPTable(2);
                table.TotalWidth = 400f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.9f, 0.5f });
                table.SpacingBefore = 15f;
                //Company Name and Address
                phrase = new Phrase();
                phrase.Add(new Chunk("Company Name : ", FontFactory.GetFont("Arial", 16, Font.BOLD, Color.BLACK)));
                phrase.Add(new Chunk(comp.Name + "\n\n", FontFactory.GetFont("Arial", 16, Font.BOLD, Color.RED)));
                //phrase.Add(new Chunk("107, Park site,\n", FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK)));
                //phrase.Add(new Chunk("Salt Lake Road,\n", FontFactory.GetFont("Arial", 12, Font.NORMAL, Color.BLACK)));
                //phrase.Add(new Chunk("Seattle, USA", FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                table.AddCell(new PdfPCell(phrase) { VerticalAlignment = PdfPCell.ALIGN_TOP,FixedHeight = 10f, PaddingLeft = 4f});
                //Company Logo
                
                cell = ImageCell(emp.UserPictureUrl, 10f, PdfPCell.ALIGN_JUSTIFIED);
                cell.BorderWidth = 1f;
                cell.BorderColor = Color.DARK_GRAY;
                cell.PaddingLeft = 8f;
                table.AddCell(cell);


                //Separater Line
                color = new Color(System.Drawing.ColorTranslator.FromHtml("#A9A9A9"));
                //DrawLine(writer, 25f, document.Top - 90f, document.PageSize.Width - 25f, document.Top - 90f, color);
                //DrawLine(writer, 25f, document.Top - 93f, document.PageSize.Width - 25f, document.Top - 93f, color);
                document.Add(table);

                table = new PdfPTable(2);
                table.HorizontalAlignment = Element.ALIGN_LEFT;
                table.SetWidths(new float[] { 1f, 1f });
                table.SpacingBefore = 20f;

                //Employee Details
                cell = PhraseCell(new Phrase("Employee Pay Slip", FontFactory.GetFont("Arial", 12, Font.UNDERLINE, Color.BLACK)), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                table.AddCell(cell);
                //cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                //cell.Colspan = 2;
                //cell.PaddingBottom = 30f;
                //table.AddCell(cell);

                //Photo
                cell = ImageCell(emp.UserPictureUrl, 8f, PdfPCell.ALIGN_RIGHT);
                table.AddCell(cell);
                document.Add(table);

                //DrawLine(writer, 182f, 30f, 182f, 700f, Color.BLACK);
                //DrawLine(writer, 100f, document.Top - 402f, document.PageSize.Width - 350f, document.Top - 402, Color.BLACK);
                //DrawLine(writer, 100f, document.Top - 382f, document.PageSize.Width - 350f, document.Top - 382f, Color.BLACK);

                table = new PdfPTable(2);
                table.SetWidths(new float[] { 0.5f, 2f });
                table.TotalWidth = 400f;
                table.LockedWidth = true;
                table.SpacingBefore = 10f;
                table.HorizontalAlignment = Element.ALIGN_RIGHT;

                //Date of From 
                table.AddCell(PhraseCell(new Phrase("From : ", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(model.startDate.ToString("dd-MM-yyyy"), FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                //Date of To 
                table.AddCell(PhraseCell(new Phrase("To  : ", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(model.endDate.ToString("dd-MM-yyyy"), FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);


                //Employee Name
                table.AddCell(PhraseCell(new Phrase("Employee Name : ", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(emp.Name, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                //Employee Designation
                table.AddCell(PhraseCell(new Phrase("Employee Designation : ", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(emp.Designation, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                //Employee Id
                table.AddCell(PhraseCell(new Phrase("Employee code:", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase("0" + emp.EmpID, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                //Address
                table.AddCell(PhraseCell(new Phrase("Address:", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                phrase = new Phrase(emp.Address, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK));
                table.AddCell(PhraseCell(phrase, PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                //Date of JoingDate
                table.AddCell(PhraseCell(new Phrase("Joining Date: ", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(emp.JoiningDate.Value.ToString("dd-MM-yyyy")), FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                //Phone
                table.AddCell(PhraseCell(new Phrase("Phone No : ", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(emp.ContactNumber, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                //Emergency Phone
                table.AddCell(PhraseCell(new Phrase("Emergency No : ", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(emp.EmergencyContact , FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                //Addtional Information
                //table.AddCell(PhraseCell(new Phrase("Addtional Information:", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));

                // Employee Basic Salary
                //table.SetTotalWidth(new float[] { 1f, 1f });
                table.AddCell(PhraseCell(new Phrase("Employee Basic Salary : ", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                table.AddCell(PhraseCell(new Phrase(Convert.ToString(emp.Salary.ToString()), FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT));
                cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                cell.Colspan = 2;
                cell.PaddingBottom = 10f;
                table.AddCell(cell);

                var returnDictionary = CalculateSalary(emp.Salary, model, comp.CompanyID, emp.FK_ShiftID);
                foreach (var returndiction in returnDictionary)
                {
                    if (returndiction.Key == "Net Salary")
                    {
                       
                        cell = PhraseCell(new Phrase(returndiction.Key, FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT);
                        cell.BorderWidth = 1f;
                        cell.BorderColor = Color.DARK_GRAY;
                        cell.PaddingBottom = 8f;
                        table.AddCell(cell);
                        cell = PhraseCell(new Phrase(Convert.ToString(returndiction.Value), FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT);
                        cell.BorderWidth = 1f;
                        cell.BorderColor = Color.DARK_GRAY;
                        cell.PaddingBottom = 8f;
                        table.AddCell(cell);
                    }
                    else {
                        cell = PhraseCell(new Phrase(returndiction.Key, FontFactory.GetFont("Arial", 8, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_LEFT);
                        table.AddCell(cell);
                        cell = PhraseCell(new Phrase(Convert.ToString(returndiction.Value), FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)), PdfPCell.ALIGN_LEFT);
                        table.AddCell(cell);
                    }
                   
                    cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
                   
                    cell.Colspan = 2;
                    cell.PaddingBottom = 5f;
                    table.AddCell(cell);

                }
                document.Add(table);
                
                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                return bytes;
            }
        }

        private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, Color color)
        {
            PdfContentByte contentByte = writer.DirectContent;
            contentByte.SetColorStroke(color);
            contentByte.MoveTo(x1, y1);
            contentByte.LineTo(x2, y2);
            contentByte.Stroke();
        }
        private static PdfPCell PhraseCell(Phrase phrase, int align)
        {
            PdfPCell cell = new PdfPCell(phrase);
            cell.BorderColor = Color.WHITE;
            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 2f;
            cell.PaddingTop = 1f;
            return cell;
        }
        private static PdfPCell ImageCell(byte[] path, float scale, int align)
        {
            //iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path));
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(path);
            image.ScalePercent(scale);
            PdfPCell cell = new PdfPCell(image);
            cell.BorderColor = Color.WHITE;
            cell.VerticalAlignment = PdfCell.ALIGN_TOP;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 3f;
            cell.PaddingTop = 3f;
            return cell;
        }


        private Dictionary<string, decimal?> CalculateSalary(decimal? basicSalary, PaymentSearchViewModel model, int companyId, int shiftId) {
            var noOfAbsentDay = 0;
            var noOfHalfDay = 0;
            var noOfLateDay = 0;
            DBContext db = new DBContext();
            var companyShift = db.CompanyShifts.First(each => each.FK_CompanyID == companyId && each.FK_ShiftID == shiftId);
            var salaryDed = db.SalaryDeductions.First(each => each.FK_CompanyID == companyId);
            var attDetails = db.AttDetails.Where(each => each.CheckinTime >= model.startDate && each.CheckinTime <= model.endDate).OrderBy(each => each.CheckinTime).Select(each => each).ToList();
            var NoOfValidDays = 0;
            var differenceDays = (model.endDate.Date - model.startDate.Date).Days;
            var spanDays = 0;
            while (model.endDate.Date > model.startDate.Date.AddDays(spanDays))
            {
                spanDays++;
                if (model.startDate.Date.AddDays(spanDays).DayOfWeek == DayOfWeek.Saturday || model.startDate.Date.AddDays(spanDays).DayOfWeek == DayOfWeek.Sunday)
                {
                    continue;
                }
                else {
                    NoOfValidDays++;
                }
            };
            noOfAbsentDay = NoOfValidDays - attDetails.Count;
           
            foreach (var attdetail in attDetails)
            {
                if ((attdetail.CheckoutTime.Value.Hour - attdetail.CheckinTime.Value.Hour) > 5) continue;
                noOfHalfDay++;
            }
            foreach (var attdetail in attDetails)
            {
                if (attdetail.CheckinTime.Value.Hour > companyShift.StartTime.Value.Hours) continue;
                noOfLateDay++;
            }
            var calculatedSalaryPerday = basicSalary / 30;
            if (noOfHalfDay > salaryDed.NoOfHalfDays)
            {
                spanDays -= (int)(noOfHalfDay / salaryDed.NoOfHalfDays);
            }
            if (noOfLateDay > salaryDed.NoOfLateDays)
            {
                spanDays -= (int)(noOfLateDay / salaryDed.NoOfLateDays);
            }
            spanDays -= noOfAbsentDay;
            var calculatedSalary = calculatedSalaryPerday * spanDays;
            
            var returnDictionary = new Dictionary<string, decimal?>();
            returnDictionary.Add("No of Day Salary", differenceDays);
            returnDictionary.Add("No of Absent Days", noOfAbsentDay);
            returnDictionary.Add("No of Late Days",noOfLateDay);
            returnDictionary.Add("No of Half Days",noOfHalfDay);
            returnDictionary.Add("Net Salary", calculatedSalary);

            return returnDictionary;
        }
    }
}