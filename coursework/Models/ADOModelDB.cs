using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace coursework.Models
{
    public partial class ADOModelDB : DbContext
    {
        public ADOModelDB()
            : base("name=ADOModelDB")
        {
        }

        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Requests> Requests { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<ServiceCategories> ServiceCategories { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clients>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Clients>()
                .Property(e => e.ContactInfo)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.Position)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.Salary)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Requests>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Requests>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Roles)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<ServiceCategories>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ServiceCategories>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Services>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Services>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Services>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);
        }
    }
}
