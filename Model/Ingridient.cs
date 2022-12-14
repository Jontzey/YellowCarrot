using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowCarrot.Model
{
    public class Ingridient
    {
        // TODO ??
        // make it so once added in database, use it for other recipe? give it a key?
        // if it already exist in data base make it so they cant use same ingrident? but instead choose from list using combobox?


        // Unique ID for the ingridient
        [Key]
        public int IngridientId { get; set; }
        
        //Simple string to give ingridient a name
        public string Name { get; set; } = null!;

        // how many of these are needed in the recipe
        public string? Quantity { get; set; }



        // foreign key thats connected to the primary key in Recipe class
        // which means what ever created in this class will be connected to the same kind of ID
        public int recipeId { get; set; }

       
        public Recipe Recipe { get; set; } = null!;

        

        

       
    }
}
