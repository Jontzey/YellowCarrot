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
        public virtual DbSet<Ingridient> ingridients { get; set; }

        public virtual DbSet<Tags> tags { get; set; }  

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=YellowCarrot;Trusted_Connection=True;");
        }
       
       

    }
}
