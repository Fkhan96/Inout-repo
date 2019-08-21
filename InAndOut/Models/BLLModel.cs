using InAndOut.Helper.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InAndOut.Models
{
    public class BLLModel
    {
        #region Employee

        public static Object getlist_Employee()
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
                           EmpID = i.EmpID,
                           UserPictureUrl = i.UserPictureUrl,
                           Name = i.Name,
                           EmpBiometric = i.Emp_Biometrics
                       }).ToList();
                }
                catch (Exception ex) { }
                return empList;
            }
        }

        public static void delete_Employee(int empId)
        {
            using (DBContext db = new DBContext())
            {
                var emp = db.Employees.Where(x => x.EmpID == empId).FirstOrDefault();
                emp.IsActive = false;
                db.SaveChanges();
            }
        }

        public static void add_employee(Employee data)
        {
            using (DBContext db = new DBContext())
            {
                data.IsActive = true;
                db.Employees.Add(data);
                db.SaveChanges();
            }
        }

        #region EmployeeDetails
        public static object getEmployeeDetails(int empId)
        {
            var empDetails = new object();
            try
            {
                using (DBContext db = new DBContext())
                {
                    empDetails = db.Employees.Where(x => x.EmpID == empId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

            }
            return empDetails;
        }

        public static void update_EmployeeDetails(Employee data)
        {
            using (DBContext db = new DBContext())
            {
                var entity = db.Employees.Where(x => x.EmpID == data.EmpID).FirstOrDefault();
                entity.UserPictureUrl = data.UserPictureUrl;
                entity.Name = data.Name;
                entity.FatherName = data.FatherName;
                entity.Designation = data.Designation;
                entity.Salary = data.Salary;
                entity.JobType = data.JobType;
                entity.BloodGroup = data.BloodGroup;
                entity.Shift = data.Shift;
                entity.Address = data.Address;
                entity.LateDeduction = data.LateDeduction;
                entity.JoiningDate = data.JoiningDate;
                entity.ContactNumber = data.ContactNumber;
                entity.EmergencyContact = data.EmergencyContact;
                db.SaveChanges();
            }
        }
        
        #endregion
        
        

        #endregion

        #region other
        public static bool isAlreadyExist(string table, string column, string value, string ignorecondition)
        {
            using (DBContext db = new DBContext())
            {
                string query = "Select 1 from " + table + " where " + column + "=" + value + "and " + ignorecondition;
                var result = db.Database.SqlQuery<List<int>>(query, new SqlParameter("value", value));
                return result.Count() > 0 ? true : false;
            }
        }
        #endregion

        #region Attendance

        public static Object getlist_Attendance()
        {
            var attList = new object();
            using (DBContext db = new DBContext())
            {
                try
                {
                    attList = db.AttDetails
                       .Where(x => x.Status == true)
                       .Select(i => new
                       {
                           EmpId = i.FK_EmpID,
                           AttendanceId=i.AttDetailsID,
                           EmpName = i.Employee.Name,
                           CheckinTime = i.CheckinTime,
                           CheckoutTime = i.CheckoutTime,
                           TimeOff = i.TimeOff,
                           Status = i.Status == true ? "Active": "InActive"
                       }).ToList();
                }
                catch (Exception ex) { }
                return attList;
            }
        }

        public static object getAttendenceDetails(int empId)
        {
            var attDetails = new object();

            using (DBContext db = new DBContext())
            {
                try
                {
                    attDetails = db.AttDetails
                        .Where(x => x.FK_EmpID == empId)
                        .Select(i => new
                        {
                            EmpId = i.FK_EmpID,
                            AttendanceId = i.AttDetailsID,
                            EmpName = i.Employee.Name,
                            CheckinTime = i.CheckinTime,
                            CheckoutTime = i.CheckoutTime,
                            TimeOff = i.TimeOff,
                            Status = i.Status == true ? "Active" : "InActive"
                        }).ToList();
                }
                catch (Exception ex){ }
            }
            return attDetails;
        }
            
        #endregion

        #region Setting

        public static Object getlist_Setting(int FK_CompanyID)
        {
            var setting = new object();
            using (DBContext db = new DBContext())
            {
                try
                {
                   setting = db.Shifts.Join(db.SalaryDeductions,
                        u => u.FK_CompanyID,
                        x => x.FK_CompanyID,
                        (u, x) => new { u, x }).
                        Where(y => y.u.FK_CompanyID == FK_CompanyID).
                        FirstOrDefault();

                    //setting = db.Shifts.Where(x => x.FK_CompanyID == FK_CompanyID);
                    //setting = db.Shifts.Where(x => x.FK_CompanyID == FK_CompanyID).FirstOrDefault();
                }
                catch (Exception ex) { }
                return setting;
            }
        }

        public static void add_shift(Shift data)
        {
            using (DBContext db = new DBContext())
            {
                db.Shifts.Add(data);
                db.SaveChanges();
            }
        }

        public static void edit_shift(Shift data)
        {
            try
            {
                using (DBContext db = new DBContext())
                {
                    var entity = db.Shifts.Where(x => x.FK_CompanyID == data.FK_CompanyID).FirstOrDefault();
                    entity.Morning = data.Morning;
                    entity.Afternoon = data.Afternoon;
                    entity.Evening = data.Evening;
                    entity.Night = data.Night;
                    db.SaveChanges();
                }
            }
            catch (Exception ex) {  }
        }

        public static void add_Salary(SalaryDeduction data)
        {
            using (DBContext db = new DBContext())
            {
                db.SalaryDeductions.Add(data);
                db.SaveChanges();
            }
        }

        public static void edit_Salary(SalaryDeduction data)
        {
            try
            {
                using (DBContext db = new DBContext())
                {
                    var entity = db.SalaryDeductions.Where(x => x.FK_CompanyID == data.FK_CompanyID).FirstOrDefault();
                    entity.NoOfDays = data.NoOfDays;
                    entity.NoOfHalfDays = data.NoOfHalfDays;
                    db.SaveChanges();
                }
            }
            catch (Exception ex) { }
        }
        #endregion

        public static bool isRecordAlreadyExist(string table, string column, string value, string ignorecondition)
        {
            using (DBContext db = new DBContext())
            {
                //string query = "Select 1 from " + table + " where " + column + "=@value and " + ignorecondition;
                string query = "Select 1 from " + table + " where " + column + "=" + value + "and " + ignorecondition;
                var result = db.Database.SqlQuery<List<int>>(query, new SqlParameter("value", value));
                return result.Count() > 0 ? true : false;
            }
        }


        #region Payment Generation
        public static Object getEmployees()
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
                           EmpID = i.EmpID,
                           UserPictureUrl = i.UserPictureUrl,
                           Name = i.Name,
                           SelfId = i.SelfId
                       }).ToList();
                }
                catch (Exception ex) { }
                return empList;
            }
        }
        #endregion
    }
}