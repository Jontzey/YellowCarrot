using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowCarrot.Model;

namespace YellowCarrot.Data
{
    // to connect to sql database we need to inherit from DbContext by downloading entity framework packages
    public class CarrotContext:DbContext
    {

        public CarrotContext()
        {

        }
        public CarrotContext(DbContextOptions options) : base(options)
        {

        }
        
       
        public virtual DbSet<Recipe> recipes { get; set; }
        public virtual DbSet<Ingridient> Ingridients { get; set; }

        public virtual DbSet<Tags> tags { get; set; }  

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=YellowCarrot;Trusted_Connection=True;");
        }
       
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Recipe>().HasData(
            new Recipe()
            {
                RecipeId = 1,
                RecipeName = "Pannkaka",
            },
            new Recipe()
            {
                RecipeId = 2,
                RecipeName = "Kladkaka"
            });

            modelBuilder.Entity<Ingridient>().HasData(
            // Pannkaka
            new Ingridient()

            {
                IngridientId = 1,
                Name = "Vetemjöl",
                Quantity = "2 1/2 dl",
                recipeId = 1,
            },
            new Ingridient()
            {
                IngridientId = 2,
                Name = "Salt",
                Quantity = "1/2 tsk",
                recipeId = 1,
            },
            new Ingridient()
            {
                IngridientId = 3,
                Name = "Mjölk",
                Quantity = "6 dl",
                recipeId = 1,
            },
            new Ingridient()
            {
                IngridientId = 4,
                Name = "Ägg",
                Quantity = "3 st",
                recipeId = 1,
            },
            new Ingridient()
            {
                IngridientId = 5,
                Name = "Smör",
                Quantity = "(till Stekning)",
                recipeId = 1,
            }, 
            // Kladkaka seed
            new Ingridient()
            {
                IngridientId = 6,
                Name = "Smör",
                Quantity = "(till Stekning)",
                recipeId = 2,
            }, 
            new Ingridient()
            {
                IngridientId = 7,
                Name = "Smör",
                Quantity = "100g",
                recipeId = 2,
            },
            new Ingridient()
            {
                IngridientId = 8,
                Name = "Ägg",
                Quantity = "2 st",
                recipeId = 2,
            },
            new Ingridient()
            {
                IngridientId = 9,
                Name = "Strösocker",
                Quantity = "2 1/2 dl",
                recipeId = 2,
            },
            new Ingridient()
            {
                IngridientId = 10,
                Name = "Kakao",
                Quantity = "3 msk",
                recipeId = 2,
            }, 
            new Ingridient()
            {
                IngridientId = 11,
                Name = "VaniljSocker",
                Quantity = "2 tsk",
                recipeId = 2,
            },
            new Ingridient()
            {
                IngridientId = 12,
                Name = "VeteMjöl",
                Quantity = "1 1/2 dl",
                recipeId = 2,
            },
            new Ingridient()
            {
                IngridientId = 13,
                Name = "Salt",
                Quantity = "1 krm",
                recipeId = 2,
            });





        }

    }
}
