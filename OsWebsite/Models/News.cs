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
    
    public partial class News
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public News()
        {
            this.NewsImages = new HashSet<NewsImages>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
        public Nullable<int> MenuID { get; set; }
        public string Decription { get; set; }
        public string DecriptionTag { get; set; }
        public string ContentNew { get; set; }
        public Nullable<System.DateTime> DateCreate { get; set; }
        public Nullable<System.DateTime> DateUpdate { get; set; }
        public string Img { get; set; }
        public string Title { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public Nullable<int> View { get; set; }
        public int Position { get; set; }
        public int IDLang { get; set; }
        public int IsOder { get; set; }
        public bool IsActive { get; set; }
        public bool ActiveTop { get; set; }
    
        public virtual Language Language { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual Module Module { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NewsImages> NewsImages { get; set; }
    }
}
