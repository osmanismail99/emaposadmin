//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmaPosAdminProject.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class proporties
    {
        public int id { get; set; }
        public Nullable<int> sectorId { get; set; }
        public string title { get; set; }
        public string text { get; set; }
    
        public virtual sector sector { get; set; }
    }
}