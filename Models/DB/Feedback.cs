//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Feedback
    {
        public int FeedbackID { get; set; }
        public int CustomerID { get; set; }
        public string Description { get; set; }
    
        public virtual CustomerUser CustomerUser { get; set; }
    }
}
