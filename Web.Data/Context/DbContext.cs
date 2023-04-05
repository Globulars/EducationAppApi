using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Entities.Models;


namespace Web.Data.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("Database");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public virtual DbSet<Users> Users { get; set; }

        public virtual DbSet<Roles> Roles { get; set; }

        public virtual DbSet<UserRoles> UserRoles { get; set; }

        public virtual DbSet<Components> Components { get; set; }

        public virtual DbSet<ComponentAccess> ComponentAccess { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<Roles>().ToTable("Roles");
            modelBuilder.Entity<UserRoles>().ToTable("UserRoles");
            modelBuilder.Entity<Components>().ToTable("Components");
            modelBuilder.Entity<ComponentAccess>().ToTable("ComponentAccess");
        }
    }

}

   
