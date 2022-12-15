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

namespace YellowCarrot
{
    /// <summary>
    /// Interaction logic for ChangeRecipeNameWindow.xaml
    /// </summary>
    public partial class ChangeRecipeNameWindow : Window
    {
        private Recipe Recipe;
        public ChangeRecipeNameWindow(Recipe CurrentRecipe)
        {
            InitializeComponent();
            this.Recipe = CurrentRecipe;
        }

        private void btnSaveRacipeName_Click(object sender, RoutedEventArgs e)
        {


            using (CarrotContext context= new CarrotContext())
            {

                Recipe recipe = context.recipes.Where(x => x.RecipeId == Recipe.RecipeId).FirstOrDefault();

                if (txbChangeRecipeName.Text.Length < 3)
                {
                    MessageBox.Show("At least the recipe name must contain 3 letters");
                }
                else
                {
                    recipe.RecipeName = txbChangeRecipeName.Text;
                    context.recipes.Update(recipe);
                    context.SaveChanges();
                    MessageBox.Show("New Recipe name has been changed!");
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();

                }


            }
        }
    }
}
