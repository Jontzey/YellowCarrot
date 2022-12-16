using EntityFrameworkCore.EncryptColumn.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowCarrot.Model
{
    public class Recipe
    {

        // gives recipe a unique Id which means it can special by itself even if there is recipe with same name(a todo if necessary, whenever uploads the recipe the users name would also be shown)

        [Key]
        public int RecipeId { get; set; }

        // simpel string to give the recipe name, this cannot be emty!
        // give the recipe name = null!;
        
        public string RecipeName { get; set; } = null!;

        // a recipe has many ingridents, thats why a list is needed to contain all those
        // it can be emty and filled later!
        // give the list = ?
        public List<Ingridient>? Ingridients { get; set; } = new();

        // using enum to give it a tag if its vegan or not (optional) can be null
        // but if used, its easier to sort
        public int TagsId { get; set; }

        public Tags Tags { get; set; } = null;
    }
}
