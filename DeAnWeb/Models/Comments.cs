//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeAnWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comments
    {
        public int commentID { get; set; }
        public string proID { get; set; }
        public string commentMessage { get; set; }
    
        public virtual Products Products { get; set; }
    }
}
