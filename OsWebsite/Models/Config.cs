//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OsWebsite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Config
    {
        public int Id { get; set; }
        public string Logo { get; set; }
        public string PlaceHead { get; set; }
        public string PlaceBody { get; set; }
        public string Contact { get; set; }
        public string Copyright { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keyword { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string HotLine { get; set; }
        public Nullable<int> IDLang { get; set; }
        public string GoogleId { get; set; }
        public string FacebookId { get; set; }
        public string TwitterID { get; set; }
        public string InstagramID { get; set; }
        public string PhotoID { get; set; }
        public string MailFooter { get; set; }
    
        public virtual Language Language { get; set; }
    }
}
