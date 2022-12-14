﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YellowCarrot.Data;

#nullable disable

namespace YellowCarrot.Migrations
{
    [DbContext(typeof(CarrotContext))]
    [Migration("20221213160330_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("YellowCarrot.Model.Ingridient", b =>
                {
                    b.Property<int>("IngridientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngridientId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Quantity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("recipeId")
                        .HasColumnType("int");

                    b.HasKey("IngridientId");

                    b.HasIndex("recipeId");

                    b.ToTable("ingridients");
                });

            modelBuilder.Entity("YellowCarrot.Model.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeId"));

                    b.Property<string>("RecipeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RecipeId");

                    b.ToTable("recipes");
                });

            modelBuilder.Entity("YellowCarrot.Model.Tags", b =>
                {
                    b.Property<int>("recipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("recipeId"));

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TheRecipeRecipeId")
                        .HasColumnType("int");

                    b.HasKey("recipeId");

                    b.HasIndex("TheRecipeRecipeId");

                    b.ToTable("tags");
                });

            modelBuilder.Entity("YellowCarrot.Model.Ingridient", b =>
                {
                    b.HasOne("YellowCarrot.Model.Recipe", "TheRecipe")
                        .WithMany("Ingridients")
                        .HasForeignKey("recipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TheRecipe");
                });

            modelBuilder.Entity("YellowCarrot.Model.Tags", b =>
                {
                    b.HasOne("YellowCarrot.Model.Recipe", "TheRecipe")
                        .WithMany("Tags")
                        .HasForeignKey("TheRecipeRecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TheRecipe");
                });

            modelBuilder.Entity("YellowCarrot.Model.Recipe", b =>
                {
                    b.Navigation("Ingridients");

                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
