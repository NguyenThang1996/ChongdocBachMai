﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class OsWebEntities : DbContext
    {
        public OsWebEntities()
            : base("name=OsWebEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Advertise> Advertise { get; set; }
        public virtual DbSet<Agency> Agency { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Config> Config { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Gallery> Gallery { get; set; }
        public virtual DbSet<GalleryDetail> GalleryDetail { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuPage> MenuPage { get; set; }
        public virtual DbSet<MenuType> MenuType { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsImages> NewsImages { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<PageMenu> PageMenu { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductImages> ProductImages { get; set; }
        public virtual DbSet<Regional> Regional { get; set; }
        public virtual DbSet<Setting> Setting { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Visitor> Visitor { get; set; }

        public virtual ObjectResult<DacapMenu_Result> DacapMenu(Nullable<int> iDLang)
        {
            var iDLangParameter = iDLang.HasValue ?
                new ObjectParameter("IDLang", iDLang) :
                new ObjectParameter("IDLang", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<DacapMenu_Result>("DacapMenu", iDLangParameter);
        }

        public virtual ObjectResult<News> News_Get4Cap(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<News>("News_Get4Cap", idParameter);
        }

        public virtual ObjectResult<News> News_Get4Cap(Nullable<int> id, MergeOption mergeOption)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<News>("News_Get4Cap", mergeOption, idParameter);
        }

        public virtual ObjectResult<Product> Product_Get4Cap(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Product>("Product_Get4Cap", idParameter);
        }

        public virtual ObjectResult<Product> Product_Get4Cap(Nullable<int> id, MergeOption mergeOption)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Product>("Product_Get4Cap", mergeOption, idParameter);
        }

        public virtual ObjectResult<DacapProduct_Result> DacapProduct(Nullable<int> iDLang, Nullable<int> position)
        {
            var iDLangParameter = iDLang.HasValue ?
                new ObjectParameter("IDLang", iDLang) :
                new ObjectParameter("IDLang", typeof(int));

            var positionParameter = position.HasValue ?
                new ObjectParameter("Position", position) :
                new ObjectParameter("Position", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<DacapProduct_Result>("DacapProduct", iDLangParameter, positionParameter);
        }

        public virtual ObjectResult<sp_Visitor_GetByAll_Result> sp_Visitor_GetByAll()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Visitor_GetByAll_Result>("sp_Visitor_GetByAll");
        }

        public virtual ObjectResult<News> News_Others(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<News>("News_Others", idParameter);
        }

        public virtual ObjectResult<News> News_Others(Nullable<int> id, MergeOption mergeOption)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("Id", id) :
                new ObjectParameter("Id", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<News>("News_Others", mergeOption, idParameter);
        }
    }
}
