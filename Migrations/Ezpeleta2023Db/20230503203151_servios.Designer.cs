﻿// <auto-generated />
using System;
using Ezpeleta2023.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ezpeleta2023.Migrations.Ezpeleta2023Db
{
    [DbContext(typeof(Ezpeleta2023DbContext))]
    [Migration("20230503203151_servios")]
    partial class servios
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Ezpeleta2023.Models.Categoria", b =>
                {
                    b.Property<int>("CategoriaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoriaID"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Eliminado")
                        .HasColumnType("bit");

                    b.HasKey("CategoriaID");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Ezpeleta2023.Models.Servicios", b =>
                {
                    b.Property<int>("ServicioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServicioID"), 1L, 1);

                    b.Property<bool>("Desabilitado")
                        .HasColumnType("bit");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubCategoriaID")
                        .HasColumnType("int");

                    b.Property<int?>("SubCategoriasSubCategoriaID")
                        .HasColumnType("int");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.HasKey("ServicioID");

                    b.HasIndex("SubCategoriasSubCategoriaID");

                    b.ToTable("Servicios");
                });

            modelBuilder.Entity("Ezpeleta2023.Models.SubCategorias", b =>
                {
                    b.Property<int>("SubCategoriaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubCategoriaID"), 1L, 1);

                    b.Property<string>("CategoriaDescripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoriaID")
                        .HasColumnType("int");

                    b.Property<string>("SubDescripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SubEliminado")
                        .HasColumnType("bit");

                    b.HasKey("SubCategoriaID");

                    b.HasIndex("CategoriaID");

                    b.ToTable("SubCategorias");
                });

            modelBuilder.Entity("Ezpeleta2023.Models.Servicios", b =>
                {
                    b.HasOne("Ezpeleta2023.Models.SubCategorias", "SubCategorias")
                        .WithMany("Servicios")
                        .HasForeignKey("SubCategoriasSubCategoriaID");

                    b.Navigation("SubCategorias");
                });

            modelBuilder.Entity("Ezpeleta2023.Models.SubCategorias", b =>
                {
                    b.HasOne("Ezpeleta2023.Models.Categoria", "Categorias")
                        .WithMany("subCategorias")
                        .HasForeignKey("CategoriaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categorias");
                });

            modelBuilder.Entity("Ezpeleta2023.Models.Categoria", b =>
                {
                    b.Navigation("subCategorias");
                });

            modelBuilder.Entity("Ezpeleta2023.Models.SubCategorias", b =>
                {
                    b.Navigation("Servicios");
                });
#pragma warning restore 612, 618
        }
    }
}
