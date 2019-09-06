using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InAndOut.Helper.Custom
{
    public class PaymentSearchViewModel
    {
        public int EmpID { get; set; } 
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
    public class PaymentViewModel
    {
        public int EmpID { get; set; }
        public string Name { get; set; }
        public int AttDetailsID { get; set; }
        public DateTime? CheckinTime { get; set; }
        public DateTime? CheckoutTime { get; set; }
        public int TimeOff { get; set; }
        public bool Status { get; set; }

    }
}