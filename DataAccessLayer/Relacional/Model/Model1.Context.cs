﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class frontofficedbEntities : DbContext
    {
        public frontofficedbEntities()
            : base("name=frontofficedbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Disenador> Disenadores { get; set; }
        public virtual DbSet<Raza> Razas { get; set; }
        public virtual DbSet<TipoEntidad> TipoEntidades { get; set; }
        public virtual DbSet<Accion> Acciones { get; set; }
        public virtual DbSet<TipoRecurso> TipoRecursos { get; set; }
        public virtual DbSet<EntidadRecursoCostos> EntidadRecursoCostos { get; set; }
        public virtual DbSet<TipoEdificio> TipoEdificios { get; set; }
        public virtual DbSet<TipoUnidad> TipoUnidades { get; set; }
    }
}
