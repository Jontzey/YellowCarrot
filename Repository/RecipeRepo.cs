using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowCarrot.Data;
using YellowCarrot.Model;

namespace YellowCarrot.Repository
{
    internal class RecipeRepo
    {
        private readonly CarrotContext _context;

        public RecipeRepo(CarrotContext context)
        {
            this._context = context;
        }


        public void GetIngridient(Recipe recipe)
        {
            _context.recipes.Add(recipe);
        }
        public void AddIngrident(Recipe recipe)
        {
            _context.recipes.Add(recipe);
        }
        public void RemoveIngridient(Recipe recipe)
        {
            _context.recipes.Remove(recipe);
        }
        private void updateIngridient(Recipe recipe)
        {
            _context.recipes.Update(recipe);
        }
    }
}
