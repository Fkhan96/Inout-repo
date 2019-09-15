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
                            i.SelfId
                        }).ToList();
                }
                catch (Exception ex) { }
                return empList;
            }
        }
    }
}