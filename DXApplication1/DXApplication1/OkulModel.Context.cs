﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DXApplication1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class DbOkulEntities : DbContext
    {
        public DbOkulEntities()
            : base("name=DbOkulEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<sysdiagrams> sysdiagrams { get; set; }
        public DbSet<TBL_BRANSLAR> TBL_BRANSLAR { get; set; }
        public DbSet<TBL_ILCELER> TBL_ILCELER { get; set; }
        public DbSet<TBL_ILLER> TBL_ILLER { get; set; }
        public DbSet<TBL_OGRENCILER> TBL_OGRENCILER { get; set; }
        public DbSet<TBL_OGRETMENLER> TBL_OGRETMENLER { get; set; }
        public DbSet<TBL_VELILER> TBL_VELILER { get; set; }
        public DbSet<TBL_AYARLAR> TBL_AYARLAR { get; set; }
        public DbSet<TBL_OGRAYARLAR> TBL_OGRAYARLAR { get; set; }
    
        public virtual ObjectResult<AyarlarOgrenciler_Result> AyarlarOgrenciler()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AyarlarOgrenciler_Result>("AyarlarOgrenciler");
        }
    
        public virtual ObjectResult<AyarlarOgretmenler_Result> AyarlarOgretmenler()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AyarlarOgretmenler_Result>("AyarlarOgretmenler");
        }
    }
}
