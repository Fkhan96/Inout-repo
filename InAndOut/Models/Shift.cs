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
    
    public partial class Shift
    {
        public Shift()
        {
            this.CompanyShifts = new HashSet<CompanyShift>();
        }
    
        public int ShiftID { get; set; }
        public string ShiftName { get; set; }
    
        public virtual ICollection<CompanyShift> CompanyShifts { get; set; }
    }
}
