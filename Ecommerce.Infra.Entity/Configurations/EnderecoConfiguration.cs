using Ecommerce.Domain.Entities.Produtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infra.Entity.Configurations
{
    public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {

            builder.ToTable("tbl_Endereco");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(p => p.Logradouro).HasColumnType("Varchar(100)").IsRequired();
            builder.Property(p => p.Numero).HasColumnType("Varchar(100)").IsRequired();
            builder.Property(p => p.CEP).HasColumnType("Varchar(100)").IsRequired();
            builder.Property(p => p.Bairro).HasColumnType("Varchar(100)").IsRequired();
            builder.Property(p => p.Cidade).HasColumnType("Varchar(100)").IsRequired();
            builder.Property(p => p.Estado).HasColumnType("Varchar(100)").IsRequired();

        }
    }
}
