using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using YellowCarrot.Data;
using YellowCarrot.Model;

namespace YellowCarrot.Repository
{
    
    public class IngridentRepository
    {
        private readonly CarrotContext _context;

        public IngridentRepository(CarrotContext context)
        {
            this._context = context;
        }

        // IF used it gets all ingridients in database
        public List<Ingridient> GetIngridients()
        {
            return _context.Ingridients.ToList();
        }

        // send all ingridients to specific Recipe where all ingridients contain that recipe.id
        public List<Ingridient> GetIngridient(int recipeId)
        {
            return _context.Ingridients.Where(x => x.recipeId == recipeId).ToList();

        }
        // adds the ingridient to database, see reference for further understanding
        public void AddIngrident(Ingridient addingridient)
        {
            _context.Ingridients.Add(addingridient);
        }
        // Picks up the listview item containing the Ingridient, then deletes it from database
        public void RemoveIngridient(Ingridient DeleteIngridient)
        {
            _context.Remove(DeleteIngridient);
        }

        // Picks up the listview item containing the Ingridient, then updates it from database
        public void updateIngridient(Ingridient UpdateIngridient)
        {
            _context.Ingridients.Update(UpdateIngridient);
        }
    }
}
