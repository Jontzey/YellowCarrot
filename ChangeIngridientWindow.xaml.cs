using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YellowCarrot.Data;
using YellowCarrot.Model;
using YellowCarrot.Repository;

namespace YellowCarrot
{
    /// <summary>
    /// Interaction logic for ChangeIngridientWindow.xaml
    /// </summary>
    public partial class ChangeIngridientWindow : Window
    {
        // Field variables
        private Ingridient Grönsaken;
        private Recipe recipe;
        
        public ChangeIngridientWindow(Ingridient ingridient,Recipe theRecipe)
        {
            InitializeComponent();
            // filed variables same as the one we fetched from previous window
            this.Grönsaken = ingridient;
            this.recipe = theRecipe;
            
        }

        private void btnSaveChange_Click(object sender, RoutedEventArgs e)
        {
            using (CarrotContext context = new CarrotContext())
            {

                if(txbName.Text.Length == 0)
                {
                    // message
                    MessageBox.Show("To save you must filled the ingrident textbox with something");
                }
                else
                {

                    // contains the inputs for recipe
                    Grönsaken.Name = txbName.Text;
                    Grönsaken.Quantity = txbInfo.Text;  

                    // send to Repo
                    new IngridentRepository(context).updateIngridient(Grönsaken);
                    context.SaveChanges();

                    // Goes back to previous window with the current Recipe
                    DetailsWindow detailsWindow = new(recipe);
                    detailsWindow.Show();
                    // close current window
                    Close();
                }
            }


        }

        private void btnExitToDetailsWindow_Click(object sender, RoutedEventArgs e)
        {
            // opens details window
            DetailsWindow detailsWindow = new(recipe);
            detailsWindow.Show();
            // close current window
            Close();
        }
    }
}
