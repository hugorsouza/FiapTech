using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities.Produtos;

namespace Ecommerce.Infra.Entity.Configurations
{
    public class FabricanteConfiguration : IEntityTypeConfiguration<Fabricante>
    {
        public void Configure(EntityTypeBuilder<Fabricante> builder)
        {

            builder.ToTable("tbl_Fabricante");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(p => p.Nome).HasColumnType("Varchar(100)").IsRequired();
            builder.Property(p => p.Ativo).HasColumnType("Bit").IsRequired();
            builder.Property(p => p.CNPJ).HasColumnType("Varchar(14)").IsRequired();
            builder.Property(p => p.EnderecoId).HasColumnType("INT");

            builder.HasOne(p => p.Endereco)
                .WithMany(c => c.Fabricantes)
                .HasPrincipalKey(c => c.Id);              
     

        }     
    }
}
