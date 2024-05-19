﻿// <auto-generated />
using Ecommerce.Infra.Entity.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ecommerce.Infra.Entity.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240516112823_RelacionamentoFabracanteEndereco")]
    partial class RelacionamentoFabracanteEndereco
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ecommerce.Domain.Entities.Produtos.Endereco", b =>
                {
                    b.Property<int>("Id")
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

            modelBuilder.Entity("Ecommerce.Domain.Entities.Produtos.Fabricante", b =>
                {
                    b.HasOne("Ecommerce.Domain.Entities.Produtos.Endereco", "Endereco")
                        .WithMany("Fabricantes")
                        .HasForeignKey("EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("Ecommerce.Domain.Entities.Produtos.Endereco", b =>
                {
                    b.Navigation("Fabricantes");
                });
#pragma warning restore 612, 618
        }
    }
}
