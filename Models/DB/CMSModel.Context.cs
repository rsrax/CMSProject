﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CMSProject.Models.DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CMSProjectEntities : DbContext
    {
        public CMSProjectEntities()
            : base("name=CMSProjectEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<BranchList> BranchLists { get; set; }
        public virtual DbSet<Complaint> Complaints { get; set; }
        public virtual DbSet<ComplaintStatusLookup> ComplaintStatusLookups { get; set; }
        public virtual DbSet<CustomerOrder> CustomerOrders { get; set; }
        public virtual DbSet<CustomerUser> CustomerUsers { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<OrderStatusLookup> OrderStatusLookups { get; set; }
        public virtual DbSet<PaymentStatusLookup> PaymentStatusLookups { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<PaymentTBL> PaymentTBLs { get; set; }
        public virtual DbSet<TrackingTBL> TrackingTBLs { get; set; }
    }
}
