//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InAndOut.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        public Employee()
        {
            this.AttDetails = new HashSet<AttDetail>();
        }
    
        public int EmpID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string FatherName { get; set; }
        public byte[] Emp_Biometrics { get; set; }
        public string Designation { get; set; }
        public Nullable<decimal> Salary { get; set; }
        public string JobType { get; set; }
        public string BloodGroup { get; set; }
        public string Address { get; set; }
        public Nullable<decimal> LateDeduction { get; set; }
        public Nullable<System.DateTimeOffset> JoiningDate { get; set; }
        public string ContactNumber { get; set; }
        public string EmergencyContact { get; set; }
        public Nullable<bool> OverTime { get; set; }
        public Nullable<bool> Bonus { get; set; }
        public byte[] UserPictureUrl { get; set; }
        public Nullable<int> SelfId { get; set; }
        public int FK_CompanyID { get; set; }
        public int FK_ShiftID { get; set; }
    
        public virtual ICollection<AttDetail> AttDetails { get; set; }
        public virtual Company Company { get; set; }
        public virtual Shift Shift { get; set; }
    }
}
