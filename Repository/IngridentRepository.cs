using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private void GetIngridient()
        {
            
        }
        public void AddIngrident(Ingridient addingridient)
        {
            _context.ingridients.Add(addingridient);
        }
        public void RemoveIngridient(Ingridient DeleteIngridient)
        {
            _context.Remove(DeleteIngridient);
        }
        private void updateIngridient(Ingridient UpdateIngridient)
        {
            _context.ingridients.Update(UpdateIngridient);
        }
    }
}
