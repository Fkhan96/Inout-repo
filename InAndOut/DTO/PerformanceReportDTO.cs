using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InAndOut.DTO
{
    public class PerformanceReportDTO
    {
        public int PerformanceReportID { get; set; }
        public int FK_EmpID { get; set; }
        public int FK_ReportTypeID { get; set; }
        public int FK_QuarterID { get; set; }
        public int FK_DocumentUploadID { get; set; }
        public int FK_ExpectationID { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public string notes { get; set; }
        public int year { get; set; }
    }
}