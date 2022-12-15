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
using YellowCarrot.Repository;

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

            btnSelect.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnChange.IsEnabled = false;


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


        //////////////////////// EVENTS /////////////////////////////
        /////////////////////////////////////////////////////////////
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Recipe recipe = new Recipe();
            using (CarrotContext context= new CarrotContext())
            {

                if(txbRecipeName.Text.Length == 0)
                {
                    MessageBox.Show("You must have letters in a name for it to exist :) ");
                }
                else if (txbRecipeName.Text.Length < 3)
                {
                    MessageBox.Show("The rule here is at least the name must have three letters! ;)");
                }
                else
                {
                    recipe.RecipeName = txbRecipeName.Text;

                    context.recipes.Add(recipe);
                    context.SaveChanges();

                    MessageBox.Show($"{recipe.RecipeName} was now added to the list");
                    txbRecipeName.Clear();
                    
                    UpdateUiList();
                    //MainWindow mainWindow = new MainWindow();
                    //mainWindow.Show();
                    //Close();
                }
            }
        }
        /////////////////////// EVENTS IN WPF ////////////////////////////////////
        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {

            using (CarrotContext context = new CarrotContext())
            {

                    ListViewItem? item = lvlRecipeList.SelectedItem as ListViewItem;
                    Recipe reciperecipe = item.Tag as Recipe;
 

                    btnSelect.IsEnabled = true;
                    DetailsWindow detailsWindow = new(reciperecipe.RecipeId);
                    detailsWindow.Show();
                 

                
            }
                    Close();
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("                                            Welcome to info\r\r " +
                "When added a recipe you can now see the recipe in the list!\r " +
                "To add ingridients or tags choose from the list and then press the details button\r\r\r Details and Delete button is disabled until user selects a Recipe from the list!");
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
                UpdateUiList();
        }


        private void lvlRecipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if(lvlRecipeList.SelectedIndex > -1)
            {
                btnSelect.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnChange.IsEnabled= true;
                
            }
            else if (lvlRecipeList.SelectedIndex == -1)
            {
                btnSelect.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnChange.IsEnabled= false;
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem selectedItem = lvlRecipeList.SelectedItem as ListViewItem;

            Recipe recipe = selectedItem.Tag as Recipe;

            ChangeRecipeNameWindow changeRecipeNameWindow = new(recipe);
            changeRecipeNameWindow.Show();
            Close();


        }


        ////////////////////////// Methods //////////////////////////////

        private void UpdateUiList()
        {
                    lvlRecipeList.Items.Clear();

            using (CarrotContext context = new CarrotContext())
            {
                List<Recipe> recipes = context.recipes.ToList();

                foreach (Recipe recipe in recipes)
                {
                    ListViewItem item = new ListViewItem();

                    item.Tag = recipe;
                    item.Content = recipe.RecipeName;
                    lvlRecipeList.Items.Add(item);
                    lvlRecipeList.SelectedItem = btnSelect;
                }

            }
        }

        //private void ComboBoxTagList()
        //{
        //    using (CarrotContext context = new CarrotContext())
        //    {
        //        Recipe recipe = new();

        //        new TagRepo(context).Gettags(recipe).ToList();

                

        //    }
        //}
    }
}
