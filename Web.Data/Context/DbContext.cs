using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            builder.Properties<DateOnly>()
                   .HaveConversion<DateOnlyConverter>()
                   .HaveColumnType("Date");
        }

        public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
        {
            /// <summary>
            /// Creates a new instance of this converter.
            /// </summary>
            public DateOnlyConverter() : base(
                    d => d.ToDateTime(TimeOnly.MinValue),
                    d => DateOnly.FromDateTime(d))
            { }
        }

        public virtual DbSet<Users> Users { get; set; }

        public virtual DbSet<Roles> Roles { get; set; }

        public virtual DbSet<UserRoles> UserRoles { get; set; }

        public virtual DbSet<Components> Components { get; set; }

        public virtual DbSet<ComponentAccess> ComponentAccess { get; set; }

        public virtual DbSet<Courses> Courses { get; set; }

        public virtual DbSet<Bookings> Bookings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<Roles>().ToTable("Roles");
            modelBuilder.Entity<UserRoles>().ToTable("UserRoles");
            modelBuilder.Entity<Components>().ToTable("Components");
            modelBuilder.Entity<ComponentAccess>().ToTable("ComponentAccess");
            modelBuilder.Entity<Courses>().ToTable("Courses");
            modelBuilder.Entity<Bookings>().ToTable("Bookings");
        }
    }

}

   
