using Ecommerce.Domain.Entities.Produtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infra.Entity.Repository
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;

        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Endereco> MyProperty { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Endereco>(e =>
            {
                e.ToTable("tbl_Endereco");
                e.HasKey(p => p.Id);
                e.Property(p => p.Id).HasColumnType("INT").ValueGeneratedNever().UseIdentityColumn();
                e.Property(p => p.Logradouro).HasColumnType("Varchar(100)").IsRequired();
                e.Property(p => p.Numero).HasColumnType("Varchar(100)").IsRequired();
                e.Property(p => p.CEP).HasColumnType("Varchar(100)").IsRequired();
                e.Property(p => p.Bairro).HasColumnType("Varchar(100)").IsRequired();
                e.Property(p => p.Cidade).HasColumnType("Varchar(100)").IsRequired();
                e.Property(p => p.Estado).HasColumnType("Varchar(100)").IsRequired();
            });

            //public required string Logradouro { get; set; }
            //public required string Numero { get; set; }
            //public required string CEP { get; set; }
            //public required string Bairro { get; set; }
            //public required string Cidade { get; set; }
            //public required string Estado { get; set; }

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if (!optionBuilder.IsConfigured)
            {
                optionBuilder.UseSqlServer(_connectionString);
            }
        }
    }
}
