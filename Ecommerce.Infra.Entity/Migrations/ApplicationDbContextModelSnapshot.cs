﻿// <auto-generated />
using Ecommerce.Infra.Entity.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ecommerce.Infra.Entity.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ecommerce.Domain.Entities.Produtos.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("Bit");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("tbl_Categoria", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.Produtos.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.Property<int>("EntidadeId")
                        .HasColumnType("int");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("tbl_Endereco", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.Produtos.Fabricante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("Bit");

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasColumnType("Varchar(14)");

                    b.Property<int>("EnderecoId")
                        .HasColumnType("INT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId");

                    b.ToTable("tbl_Fabricante", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.Produtos.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INT");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("Bit");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("INT");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.Property<int>("FabricanteId")
                        .HasColumnType("INT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.Property<decimal>("Preco")
                        .HasColumnType("Decimal(15,2)");

                    b.Property<string>("UrlImagem")
                        .IsRequired()
                        .HasColumnType("Varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("FabricanteId");

                    b.ToTable("tbl_Produto", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.Produtos.Fabricante", b =>
                {
                    b.HasOne("Ecommerce.Domain.Entities.Produtos.Endereco", "Endereco")
                        .WithMany("Fabricantes")
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.Produtos.Produto", b =>
                {
                    b.HasOne("Ecommerce.Domain.Entities.Produtos.Categoria", "Categoria")
                        .WithMany("Produtos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ecommerce.Domain.Entities.Produtos.Fabricante", "Fabricante")
                        .WithMany("Produtos")
                        .HasForeignKey("FabricanteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Fabricante");
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.Produtos.Categoria", b =>
                {
                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.Produtos.Endereco", b =>
                {
                    b.Navigation("Fabricantes");
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.Produtos.Fabricante", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
