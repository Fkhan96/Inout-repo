using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InAndOut.Helper.General
{
    public class MYJSONTbl
    {
        public string sEcho { get; set; }
        public int iTotalRecords { get; set; }
        public int iTotalDisplayRecords { get; set; }
        public object aaData { get; set; }
    }
}