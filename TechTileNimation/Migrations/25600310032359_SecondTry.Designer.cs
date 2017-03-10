using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TechTileNimation.Models;

namespace TechTileNimation.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("25600310032359_SecondTry")]
    partial class SecondTry
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TechTileNimation.Models.SensationEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnimationLink");

                    b.Property<string>("Name");

                    b.Property<string>("PreviewImageLink");

                    b.Property<string>("SensationSoundLink");

                    b.HasKey("Id");

                    b.ToTable("SensationEntry");
                });
        }
    }
}
