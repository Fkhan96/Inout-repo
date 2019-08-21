using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InAndOut.Helper.Custom
{
    public class UserAuthenticate
    {
        public int roleid { get; set; }
        public string rolename { get; set; }
        public string username { get; set; }
        public string menuitem_name { get; set; }
        public string menuitem_url { get; set; }
        public bool? hasaccess { get; set; }
        public long userid { get; set; }
    }
}