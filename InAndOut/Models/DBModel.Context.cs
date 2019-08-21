﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<menuitem> menuitems { get; set; }
        public virtual DbSet<role> roles { get; set; }
        public virtual DbSet<role_access> role_access { get; set; }
        public virtual DbSet<SalaryDeduction> SalaryDeductions { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<AttDetail> AttDetails { get; set; }
    
        public virtual ObjectResult<AttendanceDetails_Result> AttendanceDetails(Nullable<System.DateTimeOffset> startDate, Nullable<System.DateTimeOffset> endDate)
        {
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(System.DateTimeOffset));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(System.DateTimeOffset));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AttendanceDetails_Result>("AttendanceDetails", startDateParameter, endDateParameter);
        }
    }
}
