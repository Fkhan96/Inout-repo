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
    
    public partial class menuitem
    {
        public menuitem()
        {
            this.role_access = new HashSet<role_access>();
        }
    
        public int menuitemid { get; set; }
        public string menuitem_name { get; set; }
        public string menuitem_url { get; set; }
    
        public virtual ICollection<role_access> role_access { get; set; }
    }
}
