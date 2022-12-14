// <auto-generated />
using Beca.CarBrandInfo.API.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Beca.CarBrandInfo.API.Migrations
{
    [DbContext(typeof(BrandInfoContext))]
    [Migration("20221104111211_MyFirstMirton")]
    partial class MyFirstMirton
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("Beca.CarBrandInfo.API.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Ford"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Peugeot"
                        });
                });

            modelBuilder.Entity("Beca.CarBrandInfo.API.Entities.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BrandId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("Models");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BrandId = 1,
                            Description = "Since 1976 to 2022. Ford has stopped production of this model.",
                            Name = "Fiesta"
                        },
                        new
                        {
                            Id = 2,
                            BrandId = 2,
                            Description = "Since 1987 to 1997. This model was replace in 2004 by Peugeot 407.",
                            Name = "405"
                        });
                });

            modelBuilder.Entity("Beca.CarBrandInfo.API.Entities.Model", b =>
                {
                    b.HasOne("Beca.CarBrandInfo.API.Entities.Brand", "Brand")
                        .WithMany("Models")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("Beca.CarBrandInfo.API.Entities.Brand", b =>
                {
                    b.Navigation("Models");
                });
#pragma warning restore 612, 618
        }
    }
}
