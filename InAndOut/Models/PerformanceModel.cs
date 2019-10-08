using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InAndOut.Models
{
    public class PerformanceModel
    {
        public static Object getEmpList()
        {
            var empList = new object();
            using (DBContext db = new DBContext())
            {
                try
                {
                    empList = db.Employees
                        .Where(x => x.IsActive == true)
                        .Select(i => new
                        {
                            i.EmpID,
                            i.Name,
                            i.SelfId,
                            i.FK_CompanyID
                        }).ToList();
                }
                catch (Exception ex) { }
                return empList;
            }
        }
        public static Object getPerformanceByEmployeeId(int id)
        {
            var empList = new object();
            using (DBContext db = new DBContext())
            {
                try
                {
                    empList = db.PerformanceReports
                        .Where(x => x.IsDeleted == false && x.FK_EmpID == id)
                        .Select(i => new
                        {
                           i.FK_ReportTypeID,
                           i.FK_QuarterID,
                           i.FK_ExpectationID,
                           i.FK_EmpID,
                           i.FK_DocumentUploadID,
                           i.notes,
                           i.year,
                           i.PerformanceReportID
                        }).OrderBy(each => each.year).ThenBy(each => each.FK_QuarterID).ToList();
                }
                catch (Exception ex) { }
                return empList;
            }
        }
    }
}