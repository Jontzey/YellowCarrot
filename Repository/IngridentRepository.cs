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
        private readonly CarrotContext Context;

        private void GetIngridient()
        {
            
        }
        private void AddIngrident(Ingridient addingridient)
        {
            Context.ingridients.Add(addingridient);
        }
        private void RemoveIngridient(Ingridient DeleteIngridient)
        {
            Context.Remove(DeleteIngridient);
        }
        private void updateIngridient(Ingridient UpdateIngridient)
        {
            Context.ingridients.Update(UpdateIngridient);
        }
    }
}
