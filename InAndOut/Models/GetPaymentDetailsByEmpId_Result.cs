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
    
    public partial class GetPaymentDetailsByEmpId_Result
    {
        public int EmpID { get; set; }
        public string Name { get; set; }
        public int AttDetailsID { get; set; }
        public Nullable<System.DateTime> CheckinTime { get; set; }
        public Nullable<System.DateTime> CheckoutTime { get; set; }
        public Nullable<int> TimeOff { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}