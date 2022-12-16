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
    /// Interaction logic for ChangeRecipeNameWindow.xaml
    /// </summary>
    public partial class ChangeRecipeNameWindow : Window
    {
        //field variable
        private Recipe Recipe;
        public ChangeRecipeNameWindow(Recipe CurrentRecipe)
        {
            InitializeComponent();
            //the fetched recipe from previous window is this.recipe
            this.Recipe = CurrentRecipe;
        }

        private void btnSaveRacipeName_Click(object sender, RoutedEventArgs e)
        {


            using (CarrotContext context= new CarrotContext())
            {
                    //if textbox is less then 3 characters print this else save
                if (txbChangeRecipeName.Text.Length < 3)
                {
                    MessageBox.Show("At least the recipe name must contain 3 letters");
                }
                else
                {
                    //current recipe name is the textbox text
                    Recipe.RecipeName = txbChangeRecipeName.Text;
                    //save it to update the recipe in database
                    new RecipeRepo(context).updateRecipe(Recipe);
                    // save the changes to database
                    context.SaveChanges();

                    // message
                    MessageBox.Show("New Recipe name has been changed!");
                    // open mainwindow
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    //close current window
                    Close();

                }


            }
        }
    }
}
