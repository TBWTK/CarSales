﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarSales.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CarSalesEntities : DbContext
    {
        public CarSalesEntities()
            : base("name=CarSalesEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Advertisements> Advertisements { get; set; }
        public virtual DbSet<Baskets> Baskets { get; set; }
        public virtual DbSet<Bodyworks> Bodyworks { get; set; }
        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<Colors> Colors { get; set; }
        public virtual DbSet<Cylinders> Cylinders { get; set; }
        public virtual DbSet<EngineCapacity> EngineCapacity { get; set; }
        public virtual DbSet<Engines> Engines { get; set; }
        public virtual DbSet<EngineTypes> EngineTypes { get; set; }
        public virtual DbSet<Fuels> Fuels { get; set; }
        public virtual DbSet<Genders> Genders { get; set; }
        public virtual DbSet<Handlebars> Handlebars { get; set; }
        public virtual DbSet<Marks> Marks { get; set; }
        public virtual DbSet<Models> Models { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Photos> Photos { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Statuses> Statuses { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Transmissions> Transmissions { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Wheeldrives> Wheeldrives { get; set; }
    }
}
