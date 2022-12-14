using Microsoft.EntityFrameworkCore;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using YellowCarrot.Data;
using YellowCarrot.Model;

namespace YellowCarrot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            
            using (CarrotContext context = new CarrotContext())
            {
                List <Recipe> recipes = context.recipes.ToList();

                foreach(Recipe recipe in recipes)
                {
                    ListViewItem item = new ListViewItem();

                    item.Tag= recipe;
                    item.Content = recipe.RecipeName;
                    lvlRecipeList.Items.Add(item);
                    lvlRecipeList.SelectedItem = btnSelect;
                }
                
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Recipe recipe = new Recipe();
            using (CarrotContext context= new CarrotContext())
            {
                recipe.RecipeName = txbRecipeName.Text;

                context.recipes.Add(recipe);
                context.SaveChanges();
            }
            MessageBox.Show($"{recipe.RecipeName} was now added to the list");
            txbRecipeName.Clear();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {

            using (CarrotContext context = new CarrotContext())
            {
                if(lvlRecipeList.SelectedItems.Count == 0)
                {
                    btnSelect.IsEnabled = false;
                    MessageBox.Show("please choose from list before pressing details");
                    return; 

                }
                ListViewItem? item = lvlRecipeList.SelectedItem as ListViewItem;
                Recipe reciperecipe = item.Tag as Recipe;
                
                if(lvlRecipeList!= null)
                {
                    DetailsWindow detailsWindow = new(reciperecipe.RecipeId);
                    detailsWindow.Show();
                }

                 


            }
            Close();
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("                                            Welcome to info\r\r " +
                "When added a recipe you can now see the recipe in the list!\r " +
                "To add ingridients or tags choose from the list and then press the details button");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem selectedItem = lvlRecipeList.SelectedItem as ListViewItem;

            Recipe recipe = selectedItem.Tag as Recipe;

            using (CarrotContext context = new CarrotContext())
            {

                recipe = context.recipes.Where(x => x == recipe).FirstOrDefault();

                context.recipes.Remove(recipe);
                context.SaveChanges();

            }
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
