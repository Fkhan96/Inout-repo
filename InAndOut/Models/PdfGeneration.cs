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
                var navyBlue = new Color(12 , 120, 245);
                var navyLightBlue = new Color(158, 203, 241);
                document.Open();

                //Header Table
                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.SetWidths(new float[] { 0.9f, 0.5f });
                table.SpacingBefore = 10f;
                //Company Name and Address
                phrase = new Phrase();
                phrase.Add(new Chunk(comp.Name + "\n", FontFactory.GetFont("Arial", 16, Font.BOLD, navyBlue)));
                phrase.Add(new Chunk("Email : " + comp.ContactEmail +"\n", FontFactory.GetFont("Arial", 12, Font.NORMAL, navyBlue)));
                phrase.Add(new Chunk("Address : "+comp.Country + "\n", FontFactory.GetFont("Arial", 12, Font.NORMAL, navyBlue)));
                phrase.Add(new Chunk("Website : " +comp.WebsiteURL + "\n", FontFactory.GetFont("Arial", 10, Font.NORMAL, navyBlue)));
                table.AddCell(new PdfPCell(phrase) { VerticalAlignment = PdfPCell.ALIGN_TOP,FixedHeight = 50f, PaddingLeft = 4f,BorderWidth = 0f });
                //Company Logo

                cell = new PdfPCell(new Phrase((new Chunk("PAYSLIP", FontFactory.GetFont("Arial", 20, Font.BOLD, navyBlue))))); //ImageCell(emp.UserPictureUrl, 10f, PdfPCell.ALIGN_JUSTIFIED);
                cell.BorderWidth = 0f;
                cell.PaddingLeft = 55f;
                cell.PaddingTop = 15f;
                cell.PaddingBottom = 65f;
                table.AddCell(cell);


                //Separater Line
                color = new Color(System.Drawing.ColorTranslator.FromHtml("#f5f5f5"));
                //DrawLine(writer, 25f, document.Top - 90f, document.PageSize.Width - 25f, document.Top - 90f, color);
                //DrawLine(writer, 25f, document.Top - 93f, document.PageSize.Width - 25f, document.Top - 93f, color);
                document.Add(table);
               
                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = 0;
                table.SetWidths(new float[] { 0.8f, 1.2f });
                var table1 = new PdfPTable(1);
                table1.TotalWidth = 180f;
                table1.LockedWidth = true;
                table1.HorizontalAlignment = Element.ALIGN_LEFT;

                phrase = new Phrase((new Chunk("Employee Information \n", FontFactory.GetFont("Arial", 12, Font.BOLD, Color.WHITE)))) ;
                cell = new PdfPCell(phrase) { HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0, BackgroundColor = navyLightBlue, PaddingLeft = 4f, FixedHeight = 25f, Colspan = 1 };
                table1.AddCell(cell);

                phrase = new Phrase();
                phrase.Add(new Chunk(emp.Name + "\n", FontFactory.GetFont("Arial", 10, Font.NORMAL, Color.BLACK)));
                phrase.Add(new Chunk(emp.Address + "\n", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK)));
                cell = new PdfPCell(phrase) { HorizontalAlignment = Element.ALIGN_LEFT, BorderWidth = 0, BackgroundColor = color, PaddingLeft = 4f, FixedHeight = 45f, Colspan = 1 };
                table1.AddCell(cell);
                table.AddCell(table1);

                table1 = new PdfPTable(3);
                table1.TotalWidth = 250f;
                table1.LockedWidth = true;
                table1.HorizontalAlignment = Element.ALIGN_RIGHT;
                var cell1 = new PdfPCell(new Phrase((new Chunk("Payment Type", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.WHITE))))) { HorizontalAlignment = Element.ALIGN_MIDDLE, BorderWidth = 0, BackgroundColor = navyLightBlue, PaddingLeft = 3f, FixedHeight = 25f };
                table1.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase((new Chunk("Date", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.WHITE))))) { HorizontalAlignment = Element.ALIGN_MIDDLE, BorderWidth = 0, BackgroundColor = navyLightBlue, PaddingLeft = 3f, FixedHeight = 25f };
                table1.AddCell(cell1);  
                cell1 = new PdfPCell(new Phrase((new Chunk("From - To", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.WHITE))))) { HorizontalAlignment = Element.ALIGN_MIDDLE, BorderWidth = 0, BackgroundColor = navyLightBlue, PaddingLeft = 3f, FixedHeight = 25f };
                table1.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase((new Chunk("Monthly", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK))))) { HorizontalAlignment = Element.ALIGN_MIDDLE, BorderWidth = 0, BackgroundColor = color, PaddingLeft = 3f, FixedHeight = 25f };
                table1.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase((new Chunk(DateTime.Now.ToString("dd/MM/yyyy"), FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK))))) { HorizontalAlignment = Element.ALIGN_MIDDLE, BorderWidth = 0, BackgroundColor = color, PaddingLeft = 3f, FixedHeight = 25f };
                table1.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase((new Chunk(model.startDate.ToString("dd/MM/yyyy") + " - " + model.endDate.ToString("dd/MM/yyyy"), FontFactory.GetFont("Arial", 7, Font.NORMAL, Color.BLACK))))) { HorizontalAlignment = Element.ALIGN_MIDDLE, BorderWidth = 0, BackgroundColor = color, PaddingLeft = 3f, FixedHeight = 25f };
                table1.AddCell(cell1);
                    
                table.AddCell(table1);
              
                document.Add(table);

                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                table.DefaultCell.Border = 0;
                table.SetWidths(new float[] { 1.6f, 0.4f });
                
                table1 = new PdfPTable(3);
                table1.TotalWidth = 200f;
                table1.LockedWidth = true;
                table1.HorizontalAlignment = Element.ALIGN_LEFT;
                cell1 = new PdfPCell(new Phrase((new Chunk("Designation", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.WHITE))))) { HorizontalAlignment = Element.ALIGN_MIDDLE, BorderWidth = 0, BackgroundColor = navyLightBlue, PaddingLeft = 3f, FixedHeight = 20f };
                table1.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase((new Chunk("Emp Code", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.WHITE))))) { HorizontalAlignment = Element.ALIGN_MIDDLE, BorderWidth = 0, BackgroundColor = navyLightBlue, PaddingLeft = 3f, FixedHeight = 20f };
                table1.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase((new Chunk("Join Date", FontFactory.GetFont("Arial", 8, Font.BOLD, Color.WHITE))))) { HorizontalAlignment = Element.ALIGN_MIDDLE, BorderWidth = 0, BackgroundColor = navyLightBlue, PaddingLeft = 3f, FixedHeight = 20f };
                table1.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase((new Chunk(emp.Designation, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK))))) { HorizontalAlignment = Element.ALIGN_MIDDLE, BorderWidth = 0, BackgroundColor = color, PaddingLeft = 3f, FixedHeight = 20f };
                table1.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase((new Chunk(emp.EmpID.ToString(), FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK))))) { HorizontalAlignment = Element.ALIGN_MIDDLE, BorderWidth = 0, BackgroundColor = color, PaddingLeft = 3f, FixedHeight = 20f };
                table1.AddCell(cell1);
                cell1 = new PdfPCell(new Phrase((new Chunk(emp.JoiningDate.Value.ToString("dd-MM-yyyy"), FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK))))) { HorizontalAlignment = Element.ALIGN_MIDDLE, BorderWidth = 0, BackgroundColor = color, PaddingLeft = 3f, FixedHeight = 20f };
                table1.AddCell(cell1);
                table.AddCell(table1);
                cell = new PdfPCell() { FixedHeight = 30f, BorderWidth = 0 };
                table.AddCell(cell);


                cell = new PdfPCell() { FixedHeight = 25f, BorderWidth = 0 };
                table.AddCell(cell);
                cell = new PdfPCell() { FixedHeight = 25f, BorderWidth = 0 };
                table.AddCell(cell);


                cell = new PdfPCell(new Phrase((new Chunk("Description", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.WHITE))))) { HorizontalAlignment = Element.ALIGN_MIDDLE, BorderWidth = 0, BackgroundColor = navyLightBlue,PaddingTop=10f, PaddingLeft = 15f, FixedHeight = 30f };
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase((new Chunk("Amount", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.WHITE))))) { HorizontalAlignment = Element.ALIGN_MIDDLE, BorderWidth = 0, BackgroundColor = navyLightBlue, PaddingLeft = 15f, FixedHeight = 30f, PaddingTop = 10f, };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase((new Chunk("Basic Salary ", FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK))))) { HorizontalAlignment = Element.ALIGN_MIDDLE, BorderWidth = 0, BackgroundColor = color, PaddingLeft = 15f, FixedHeight = 25, PaddingTop = 5f };
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase((new Chunk(Convert.ToString(emp.Salary.ToString()), FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK))))) { HorizontalAlignment = Element.ALIGN_MIDDLE, BorderWidth = 0, BackgroundColor = color, PaddingLeft = 15f, FixedHeight = 25f, PaddingTop = 5f, };
                table.AddCell(cell);
                

                var returnValues = CalculateSalary(emp.Salary, model, comp.CompanyID, emp.FK_ShiftID);

                foreach (var returndiction in returnValues)
                {
                    if (returndiction.Key == "Net Pay")
                    {

                        cell = new PdfPCell(new Phrase((new Chunk(returndiction.Key, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK))))) { HorizontalAlignment = Element.ALIGN_MIDDLE, BorderWidth = 0, BackgroundColor = color, PaddingLeft = 15f, FixedHeight = 25, PaddingTop = 5f };
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase((new Chunk(Convert.ToString(returndiction.Value), FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK))))) { HorizontalAlignment = Element.ALIGN_MIDDLE, BorderWidth = 0, BackgroundColor = color, PaddingLeft = 15f, FixedHeight = 25f, PaddingTop = 5f, };
                        table.AddCell(cell);
                            
                    }
                    else
                    {
                        cell = new PdfPCell(new Phrase((new Chunk(returndiction.Key, FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK))))) { HorizontalAlignment = Element.ALIGN_MIDDLE, BorderWidth = 0, BackgroundColor = color, PaddingLeft = 15f, FixedHeight = 25f, PaddingTop = 5f };
                        table.AddCell(cell);
                        cell = new PdfPCell(new Phrase((new Chunk(Convert.ToString(returndiction.Value), FontFactory.GetFont("Arial", 8, Font.NORMAL, Color.BLACK))))) { HorizontalAlignment = Element.ALIGN_MIDDLE, BorderWidth = 0, BackgroundColor = color, PaddingLeft = 15f, FixedHeight = 25f, PaddingTop = 5f };
                        table.AddCell(cell);

                    }
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
            returnDictionary.Add("Net Pay", calculatedSalary);

            return returnDictionary;
        }
    }
}