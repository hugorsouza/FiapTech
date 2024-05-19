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
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {


        public void Configure(EntityTypeBuilder<Produto> builder)
        {

            builder.ToTable("tbl_Produto");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(p => p.Nome).HasColumnType("Varchar(100)").IsRequired();
            builder.Property(p => p.Ativo).HasColumnType("Bit").IsRequired();
            builder.Property(p => p.Descricao).HasColumnType("Varchar(100)").IsRequired();
            builder.Property(p => p.UrlImagem).HasColumnType("Varchar(100)").IsRequired();
            builder.Property(p => p.Preco).HasColumnType("Decimal(15,2)").IsRequired();


            builder.HasOne(p => p.Categoria)
            .WithMany(c => c.Produtos)
            .HasPrincipalKey(c => c.Id);


            builder.HasOne(p => p.Fabricante)
            .WithMany(c => c.Produtos)
            .HasPrincipalKey(c => c.Id);

        }

    }
}
