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
    
    public partial class role_access
    {
        public int role_accessid { get; set; }
        public int menuitemid { get; set; }
        public int roleid { get; set; }
        public bool has_access { get; set; }
    
        public virtual menuitem menuitem { get; set; }
        public virtual role role { get; set; }
    }
}
