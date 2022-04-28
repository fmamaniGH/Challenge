using Microsoft.EntityFrameworkCore;
using Challenge.Ecommerce.Domain.Entity;
using System;
using Challenge.Ecommerce.Infrastructure.Data.Mappings;

namespace Challenge.Ecommerce.Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        private readonly string _connectionString;
        public ApplicationContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {

        }

        public DbSet<Usuario> Usuario { get; set; }
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!String.IsNullOrWhiteSpace(_connectionString))
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UsuarioMap());
           
        }

    }
}
