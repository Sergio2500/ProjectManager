﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project.Manager.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ProjectManagerConnection : DbContext
    {
        public ProjectManagerConnection()
            : base("name=ProjectManagerConnection")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CadCliente> CadCliente { get; set; }
        public virtual DbSet<CadColaborador> CadColaborador { get; set; }
        public virtual DbSet<CadProjeto> CadProjeto { get; set; }
        public virtual DbSet<ColaboradorProjeto> ColaboradorProjeto { get; set; }
        public virtual DbSet<Contato> Contato { get; set; }
        public virtual DbSet<HoraTrabalhada> HoraTrabalhada { get; set; }
        public virtual DbSet<Skill> Skill { get; set; }
    }
}
