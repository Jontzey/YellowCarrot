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
        public List<Ingridient> GetIngridients()
        {
            return _context.Ingridients.ToList();
        }
        public List<Ingridient> GetIngridient(int recipeId)
        {
            return _context.Ingridients.Where(x => x.recipeId == recipeId).ToList();

        }
        public void AddIngrident(Ingridient addingridient)
        {
            _context.Ingridients.Add(addingridient);
        }
        public void RemoveIngridient(Ingridient DeleteIngridient)
        {
            _context.Remove(DeleteIngridient);
        }
        public void updateIngridient(Ingridient UpdateIngridient)
        {
            _context.Ingridients.Update(UpdateIngridient);
        }
    }
}
