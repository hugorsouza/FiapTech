using Ecommerce.Domain.Entities.Produtos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infra.Entity.Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {


        public void Configure(EntityTypeBuilder<Categoria> builder)
        {

            builder.ToTable("tbl_Categoria");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(p => p.Nome).HasColumnType("Varchar(100)").IsRequired();
            builder.Property(p => p.Ativo).HasColumnType("Bit").IsRequired();
            builder.Property(p => p.Descricao).HasColumnType("Varchar(100)").IsRequired();
        

        }

    }
}
