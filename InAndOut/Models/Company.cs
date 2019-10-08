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
    
    public partial class Company
    {
        public Company()
        {
            this.CompanyShifts = new HashSet<CompanyShift>();
            this.SalaryDeductions = new HashSet<SalaryDeduction>();
            this.Employees = new HashSet<Employee>();
        }
    
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public Nullable<int> SelfId { get; set; }
        public byte[] PictureUrl { get; set; }
    
        public virtual ICollection<CompanyShift> CompanyShifts { get; set; }
        public virtual ICollection<SalaryDeduction> SalaryDeductions { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
