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
            this.SalaryDeductions = new HashSet<SalaryDeduction>();
            this.Shifts = new HashSet<Shift>();
        }
    
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public Nullable<int> SelfId { get; set; }
        public byte[] PictureUrl { get; set; }
    
        public virtual ICollection<SalaryDeduction> SalaryDeductions { get; set; }
        public virtual ICollection<Shift> Shifts { get; set; }
    }
}
