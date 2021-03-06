// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using payment_bac.api.Data;

#nullable disable

namespace payment_bac.api.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20220615145028_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("payment_bac.api.Models.Policy", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<double>("AmountPaid")
                        .HasColumnType("float");

                    b.Property<string>("PolicyDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PolicyDue")
                        .HasColumnType("datetime2");

                    b.Property<string>("PolicyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PolicyTotal")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.ToTable("Policies");
                });

            modelBuilder.Entity("payment_bac.api.Models.Session", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("bit");

                    b.Property<int>("PolicyID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PolicyID");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("payment_bac.api.Models.Session", b =>
                {
                    b.HasOne("payment_bac.api.Models.Policy", "Policy")
                        .WithMany()
                        .HasForeignKey("PolicyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Policy");
                });
#pragma warning restore 612, 618
        }
    }
}
