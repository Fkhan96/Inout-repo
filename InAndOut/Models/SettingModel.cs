using InAndOut.Helper.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InAndOut.Models
{
    public class SettingModel
    {

        public static object getShiftSetting(int FK_CompanyID)
        {
            var shiftSettings = new object();
            using (DBContext db = new DBContext())
            {
                try
                {
                    shiftSettings = db.CompanyShifts.Where(x => x.FK_CompanyID == FK_CompanyID).ToList();
                }
                catch (Exception ex) { }
            }
            return Common.Serialize(shiftSettings);
        }

        public static object getSalaryDeductionSetting(int FK_CompanyID)
        {
            var salaryDeductionSettings = new object();
            using (DBContext db = new DBContext())
            {
                try
                {
                    salaryDeductionSettings = db.SalaryDeductions.Where(x => x.FK_CompanyID == FK_CompanyID).FirstOrDefault();
                }
                catch (Exception ex) { }
                return Common.Serialize(salaryDeductionSettings);

            }
        }

        public static string getCurrencySetting(int CompanyId)
        {
            using (DBContext db = new DBContext())
            {
                var Currency = db.Companies.Where(y => y.CompanyID == CompanyId).Select(x => x.Currency).FirstOrDefault();
                return Common.Serialize(Currency);
            }
        }

        public static void editCurrency(int CompanyID, int? Currency)
        {
            using (DBContext db = new DBContext())
            {
                try
                {
                    var company = db.Companies.Where(x => x.CompanyID == CompanyID).FirstOrDefault();
                    company.Currency = Currency;
                    db.SaveChanges();
                    
                }
                catch (Exception ex) { }
            }
        }
    }
}