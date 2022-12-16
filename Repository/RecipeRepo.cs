using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowCarrot.Data;
using YellowCarrot.Model;

namespace YellowCarrot.Repository
{
    public class RecipeRepo
    {
        private readonly CarrotContext _context;

        public RecipeRepo(CarrotContext context)
        {
            this._context = context;
        }

        public List<Recipe> GetAllRecipes()
        {
            return _context.recipes.ToList();
        }
        public void GetRecipe(Recipe recipe)
        {
            _context.recipes.Add(recipe);
        }
        public void AddRecipe(Recipe recipe)
        {
            _context.recipes.Add(recipe);
        }
        public void RemoveRecipe(Recipe recipe)
        {
            _context.recipes.Remove(recipe);
        }
        public void updateRecipe(Recipe recipe)
        {
            _context.recipes.Update(recipe);
        }
    }
}
