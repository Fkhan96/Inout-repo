using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace InAndOut.Helper.General
{
    public struct Result<T> where T : class
    {
        public ResultStatus Status { get; set; }
        public string Message { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
    public struct Result
    {
        public ResultStatus Status { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }
    public class Common
    {
        public Common()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    
        public static string GetMD5Hash(string plaintext)
        {
            MD5CryptoServiceProvider MD5provider = new MD5CryptoServiceProvider();
            byte[] hasedvalue = MD5provider.ComputeHash(Encoding.Default.GetBytes(plaintext));
            StringBuilder str = new StringBuilder();
            for (int counter = 0; counter < hasedvalue.Length; counter++)
            {
                str.Append(hasedvalue[counter].ToString("x2"));
            }
            return str.ToString();
        }

        public static string GetCommaSeprateStringFromList(List<string> mylist)
        {
            string result = "";
            foreach (string item in mylist)
            {
                result += item + ",";

            }
            result = result.Remove(result.Length - 1);

            return result;
        }

        public static List<int> GetListOfInterger(string text)
        {
            List<int> mylist = new List<int>();

            if (text != null)
            {

                string textst = text.Replace("[", " ");
                text = textst.Replace("]", " ");
                text = text.Replace("\"", " ");
                text = text.Replace("\\", " ");
                if (text == null)
                {
                    return mylist;
                }

                string[] Ids = text.Split(',');
                if ((text != "") && (text != null) && (text != "null"))
                {
                    foreach (string word in Ids)
                    {

                        if (word.Trim() != "")
                            mylist.Add(Convert.ToInt32(word.Trim()));

                    }
                }
            }
            return mylist;

        }
        public static string RemoveCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }

        public static string RemoveSpecialCharacter(string input_text, Dictionary<string, string> characters)
        {

            foreach (string key in characters.Keys)
            {

                bool status = false;
                while (status == false)
                {
                    if (input_text.Contains(key))
                    {
                        input_text = input_text.Replace(key, characters[key]);
                    }

                    else
                    {
                        status = true;
                    }
                }
            }

            return input_text;
        }

        public static DateTime? ConvertDate(object Date)
        {
            string[] d = Date.ToString().Split(' ');
            if (d.Length > 1)
            {
                string[] f = d[0].Split('/');
                Date = f[1] + "/" + f[0] + "/" + f[2] + " " + d[1];
                return Convert.ToDateTime(Date);
            }
            else
            {
                string[] f = Date.ToString().Split('/');
                Date = f[1] + "/" + f[0] + "/" + f[2];
                return Convert.ToDateTime(Date);
            }
        }

        public static DateTime ConvertDateTime(object Datetime)
        {
            string[] d = Datetime.ToString().Split(' ');
            if (d.Length > 1)
            {
                DateTime dt;
                bool res = DateTime.TryParse(d[4] + " " + d[5], out dt);
                Datetime = d[0] + "-" + d[1] + "-" + d[2] + " " + dt.ToString("HH:mm");
            }
            return Convert.ToDateTime(Datetime);
        }


        private static string replace(string c)
        {
            string cc = c.ToString();

            switch (cc)
            {
                case "%7E":
                    return "~";
                case "%21":
                    return "!";
                case "%40":
                    return "@";
                case "%23":
                    return "#";
                case "%24":
                    return "$";
                case "%25":
                    return "%";
                case "%5E":
                    return "^";
                case "%26":
                    return "&";
                case "%2A":
                    return "*";
                case "%28":
                    return "(";
                case "%29":
                    return ")";
                case "%7B":
                    return "{";
                case "%7D":
                    return "}";
                case "%5B":
                    return "[";
                case "%5D":
                    return "]";
                case "%3D":
                    return "=";
                case "%3A":
                    return ":";
                case "/":
                    return "/";
                case "%2C":
                    return ",";
                case "%3B":
                    return ";";
                case "%3F":
                    return "?";
                case "%2B":
                    return "+";
                case "%27":
                    return "\''";
                case "%5C":
                    return "\\";
                default:
                    return cc.ToString();
            }

        }

        public static string Serialize(object obj)
        {
            string result = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, PreserveReferencesHandling = PreserveReferencesHandling.None });
            return result;
        }

        public static object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }
        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static T Deserialize<T>(string json, IsoDateTimeConverter dateformat)
        {
            return JsonConvert.DeserializeObject<T>(json, dateformat);
        }
        public static string GetHash(string value)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(value, "md5");
        }

        public static void BindDropDown(DropDownList ddlGeneral, Object dataSource, string dataTextField, string dataValueField, bool hasSelectItem, bool hasOtherItem)
        {
            ddlGeneral.DataSource = dataSource;
            ddlGeneral.DataTextField = dataTextField;
            ddlGeneral.DataValueField = dataValueField;
            ddlGeneral.DataBind();

            if (hasSelectItem == true)
            {
                ddlGeneral.Items.Insert(0, new ListItem("-Select-", "0"));
            }

            if (hasOtherItem == true)
            {
                ddlGeneral.Items.Add(new ListItem("-OTHER-", "-100"));
            }
        }

        public static string ConvertJSONDate(string Date)
        {
            //2013-10-11T09:29:07.000Z
            //2013-09-26 16:33:46
            // Base baseClass = new Base();
            Date = Date.Replace("T", " ").Replace("Z", "").Remove(Date.Length - 5);
            //String newDate = Convert.ToDateTime(Date).AddMinutes(-1 * baseClass.TimeZoneOffset).ToString("MM/dd/yyyy hh:mm:ss tt");
            //return newDate;
            return Date;
        }

        public static string MapPathReverse(string fullServerPath)
        {
            return @"\" + fullServerPath.Replace(HttpContext.Current.Request.PhysicalApplicationPath, String.Empty);
        }

        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ' ')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static string[] ParseReferer(string referer)
        {
            if (string.IsNullOrEmpty(referer) == false)
            {
                Uri uri = new Uri(referer);
                string domain = uri.Scheme + Uri.SchemeDelimiter + uri.Host;
                string keyword = string.Empty;

                if (string.IsNullOrEmpty(uri.Query) == false)
                {
                    string qsValue = uri.Query.Replace("?", "").Split('&').FirstOrDefault(f => f.StartsWith("q="));
                    if (qsValue != null)
                        keyword = qsValue.Substring(2);
                }
                return new string[] { domain, keyword };
            }
            else
                return null;
        }

        public static string[] Split(string input, string p)
        {
            bool pair = false;
            StringBuilder inputSB = new StringBuilder(input);
            for (int i = 0; i < inputSB.Length; i++)
            {
                char c = inputSB[i];
                if (c == '\"')
                {
                    pair = (pair == false) ? true : false;
                }
                if (c == ',' && pair == true)
                    inputSB[i] = ' ';

            }

            input = inputSB.ToString();
            return input.Split(',');
        }

        public static string GetDateInDesireFormats(string format, DateTime StartDate, DateTime EndDate)
        {
            DateTime today = DateTime.Now;

            switch (format)
            {
                case "1":
                    return today.ToString("MMM", new System.Globalization.CultureInfo("en-GB")) + " " + today.Year;
                case "2":
                    return
                        new DateTime(DateTime.Now.Year, 1, 1).ToString("MMM", new System.Globalization.CultureInfo("en-GB")) + " - " +
                        today.ToString("MMM", new System.Globalization.CultureInfo("en-GB")) + ", " + DateTime.Now.Year + " (PY)";
                case "3":
                    return today.ToString("MMMM", new System.Globalization.CultureInfo("en-GB")) + " " + DateTime.Now.Year;
                case "4":
                    return StartDate.ToString("MMM", new System.Globalization.CultureInfo("en-GB")) + "-" + StartDate.Year + " - " +
                           EndDate.ToString("MMM", new System.Globalization.CultureInfo("en-GB")) + "-" + EndDate.Year;
                default:
                    return null;
            }
        }

        public static string GetGMTPlusOneFormattedDate(DateTime? date, bool withTime)
        {
            DateTime MyTime = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, date.Value.Hour, date.Value.Minute, date.Value.Second, DateTimeKind.Utc);

            DateTime MyTimeInWesternEurope = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(MyTime, "W. Europe Standard Time");

            string format = withTime == true ? "yyyy-MM-dd hh:mm" : "yyyy-MM-dd";
            if (date != null)
            {
                return Convert.ToDateTime(MyTimeInWesternEurope).ToString(format);
            }

            return "";
        }

        public static string GetFormattedDate(DateTime? date, bool withTime, int offset = 0)
        {
            string format = withTime == true ? "dd-MM-yyyy hh:mm tt" : "dd-MM-yyyy";
            if (date != null)
                return Convert.ToDateTime(date.Value.AddHours(offset)).ToString(format);
            else
                return "";

            //yyyy-MM-dd
        }

        public static string GetFormatTime(DateTime? date)
        {
            DateTime dt = DateTime.Parse(date.Value.ToShortTimeString());
            return dt.ToString("HH:mm");
        }

        public enum EmailTemplates
        {
            ForgotPassword = 1,
            VerifyEmail = 2,
            Newvisitor = 3,
            NewSingupRequest = 4,
            SignupLink = 5,
            Activation = 6,
            TicketReply = 7,
            PromotionCode = 8,
            ResetPassword = 9,
            TaskReminder = 104,
        }

        public enum Roles
        {
            Administrator = 1,
            Partner = 2,
            Customer = 3
        }

        public static string GetRoleName(int roleCode)
        {
            if (roleCode == (int)Roles.Administrator)
                return "Administrator";
            else if (roleCode == (int)Roles.Partner)
                return "Partner";
            else
                return "Customer";
        }

        public static string GetTemplateString(int templateCode)
        {
            StreamReader objStreamReader;
            string path = "";
            if (templateCode == (int)Common.EmailTemplates.ForgotPassword)
            {
                path = AppDomain.CurrentDomain.BaseDirectory + @"\Templates\forgotpassword.html";
            }
            //else if (templateCode == (int)Common.EmailTemplates.SendCampaign)
            //{
            //    path = AppDomain.CurrentDomain.BaseDirectory + @"\Templates\SendCampaign.html";
            //}
            //else if (templateCode == (int)Common.EmailTemplates.AdminEmailOffer)
            //{
            //    path = AppDomain.CurrentDomain.BaseDirectory + @"\Templates\AdminEmailOffer.html";
            //}
            //else if (templateCode == (int)Common.EmailTemplates.PartnerAddEmail)
            //{
            //    path = AppDomain.CurrentDomain.BaseDirectory + @"\Templates\PartnerAddEmail.html";
            //}
            if (!string.IsNullOrEmpty(path))
            {
                objStreamReader = File.OpenText(path);
                string emailText = objStreamReader.ReadToEnd();
                objStreamReader.Close();
                objStreamReader = null;
                objStreamReader = null;
                return emailText;

            }
            else
            {
                objStreamReader = null;
                return string.Empty;
            }


        }

        public static Stream ResizeImage(System.Drawing.Image imgToResize, Size size)
        {
            //int sourceWidth = imgToResize.Width;
            //int sourceHeight = imgToResize.Height;

            //float nPercent = 0;
            //float nPercentW = 0;
            //float nPercentH = 0;

            //nPercentW = ((float)size.Width / (float)sourceWidth);
            //nPercentH = ((float)size.Height / (float)sourceHeight);

            //if (nPercentH < nPercentW)
            //    nPercent = nPercentH;
            //else
            //    nPercent = nPercentW;

            //int destWidth = (int)(sourceWidth * nPercent);
            //int destHeight = (int)(sourceHeight * nPercent);

            int destWidth = size.Width;
            int destHeight = size.Height;

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return ImageToStream((System.Drawing.Image)b);
        }

        public static Stream ImageToStream(System.Drawing.Image image)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, ImageFormat.Bmp);
            stream.Position = 0;
            return stream;
        }

        public static System.Drawing.Bitmap Resize(System.Drawing.Bitmap value, int newWidth, int newHeight)
        {
            System.Drawing.Bitmap resizedImage = new System.Drawing.Bitmap(newWidth, newHeight);
            System.Drawing.Graphics.FromImage((System.Drawing.Image)resizedImage).DrawImage(value, 0, 0, newWidth, newHeight);
            return (resizedImage);
        }

        public static double GetDistanceFromLatLonInKm(decimal slat1,
                                 decimal slon1,
                                 decimal slat2,
                                 decimal slon2)
        {
            try
            {
                double lat1 = Convert.ToDouble(slat1);
                double lon1 = Convert.ToDouble(slon1);
                double lat2 = Convert.ToDouble(slat2);
                double lon2 = Convert.ToDouble(slon2);
                var R = 6371d; // Radius of the earth in km
                var dLat = Deg2Rad(lat2 - lat1);  // deg2rad below
                var dLon = Deg2Rad(lon2 - lon1);
                var a =
                  Math.Sin(dLat / 2d) * Math.Sin(dLat / 2d) +
                  Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) *
                  Math.Sin(dLon / 2d) * Math.Sin(dLon / 2d);
                var c = 2d * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1d - a));
                var d = R * c; // Distance in km
                return d;
            }
            catch (Exception ex)
            {
                //TODO LOGHERE
                return 1000;
            }
        }

        static double Deg2Rad(double deg)
        {
            return deg * (Math.PI / 180d);
        }

        public static object GetUserInfo()
        {
            return HttpContext.Current.Session["CurrentUser"];
        }
        public static List<DateTime> getDatesOfDayInMonth(DateTime date, DayOfWeek DayOfWeek)
        {
            List<DateTime> list = new List<DateTime>();
            int intMonth = date.Month;
            int intYear = date.Year;
            int intDaysThisMonth = DateTime.DaysInMonth(intYear, intMonth);
            DateTime oBeginnngOfThisMonth = new DateTime(intYear, intMonth, 1);
            for (int i = 1; i < intDaysThisMonth + 1; i++)
            {
                if (oBeginnngOfThisMonth.DayOfWeek == DayOfWeek)
                {
                    list.Add(new DateTime(intYear, intMonth, i));

                }
                oBeginnngOfThisMonth = oBeginnngOfThisMonth.AddDays(1);
            }
            return list;
        }
        public static int CountDays(DayOfWeek day, DateTime start, DateTime end)
        {
            TimeSpan ts = end - start;                       // Total duration
            int count = (int)Math.Floor(ts.TotalDays / 7);   // Number of whole weeks
            int remainder = (int)(ts.TotalDays % 7);         // Number of remaining days
            int sinceLastDay = (int)(end.DayOfWeek - day);   // Number of days since last [day]
            if (sinceLastDay < 0) sinceLastDay += 7;         // Adjust for negative days since last [day]

            // If the days in excess of an even week are greater than or equal to the number days since the last [day], then count this one, too.
            if (remainder >= sinceLastDay) count++;

            return count;
        }
        public static List<DateTime> DateRange(DateTime fromDate, DateTime toDate)
        {
            return Enumerable.Range(0, toDate.Subtract(fromDate).Days + 1)
                             .Select(d => fromDate.AddDays(d)).ToList();
        }

        #region excel
        private static string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;

            if (cell.CellValue == null)
            {
                return null;
            }
            string value = getCellValue(cell);// cell.CellValue.InnerXml;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                return value;
            }
        }
        private static string getCellValue(Cell cell)
        {
            string value;
            if (cell.CellFormula != null && cell.CellFormula.InnerXml.Contains("/100"))
            {
                value = (Convert.ToDecimal(cell.CellValue.InnerXml) * (decimal)100).ToString();
            }
            else
            {
                value = cell.CellValue.InnerXml;
            }
            return value;
        }
        public static DataTable ReadExcelAsDataTable(string fileName)
        {
            DataTable dataTable = new DataTable();
            try
            {

                using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(fileName, false))
                {
                    WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                    IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                    string relationshipId = sheets.First().Id.Value;
                    WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
                    Worksheet workSheet = worksheetPart.Worksheet;
                    SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                    IEnumerable<Row> rows = sheetData.Descendants<Row>();

                    foreach (Cell cell in rows.ElementAt(0))
                    {
                        dataTable.Columns.Add(GetCellValue(spreadSheetDocument, cell));
                    }

                    foreach (Row row in rows)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
                        {
                            try
                            {
                                dataRow[i] = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(i));
                            }
                            catch (Exception)
                            {

                            }
                        }

                        dataTable.Rows.Add(dataRow);
                    }

                }
                dataTable.Rows.RemoveAt(0);
                return dataTable;

            }
            catch (System.Exception ex)
            {

                return null;
            }


        }
        public static DataTable ReadExcelAsDataTable(string fileName, string startcondition)
        {
            DataTable dataTable = new DataTable();
            try
            {

                using (SpreadsheetDocument spreadSheetDocument = SpreadsheetDocument.Open(fileName, false))
                {
                    WorkbookPart workbookPart = spreadSheetDocument.WorkbookPart;
                    IEnumerable<Sheet> sheets = spreadSheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                    string relationshipId = sheets.First().Id.Value;
                    WorksheetPart worksheetPart = (WorksheetPart)spreadSheetDocument.WorkbookPart.GetPartById(relationshipId);
                    Worksheet workSheet = worksheetPart.Worksheet;
                    SheetData sheetData = workSheet.GetFirstChild<SheetData>();
                    List<Row> rows = sheetData.Descendants<Row>().ToList();
                    var recordStartRow = 0;
                    for (var j = 0; j < rows.Count(); j++)
                    {
                        Row row = rows[j];
                        DataRow dataRow = dataTable.NewRow();
                        for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
                        {
                            string text = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(i));
                            if (!string.IsNullOrEmpty(text) && text.Replace(" ", "").Replace(".", "").ToLower().Contains(startcondition))
                            {
                                recordStartRow = j;
                                break;
                            }
                        }
                    }
                    foreach (Cell cell in rows.ElementAt(recordStartRow))
                    {
                        dataTable.Columns.Add(GetCellValue(spreadSheetDocument, cell));
                    }


                    for (var j = recordStartRow; j < rows.Count(); j++)
                    {
                        Row row = rows[j];
                        DataRow dataRow = dataTable.NewRow();
                        for (int i = 0; i < row.Descendants<Cell>().Count(); i++)
                        {
                            dataRow[i] = GetCellValue(spreadSheetDocument, row.Descendants<Cell>().ElementAt(i));
                        }

                        dataTable.Rows.Add(dataRow);
                    }
                }
                dataTable.Rows.RemoveAt(0);
                return dataTable;

            }
            catch (System.Exception ex)
            {

                return null;
            }


        }
        #endregion

        #region GetEnumName
        //public static string GetEnumName(int enumValue) 
        //{
        //    return enumValue;
        
        //}
         

        #endregion

    }
}