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
                return Common.Serialize(shiftSettings);

            }
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
    }
}