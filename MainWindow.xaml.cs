using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
        ///////// THE WINDOW ////////////////
        public MainWindow()
        {
            InitializeComponent();
            // all buttons is disabled until a current event happens
            btnSelect.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnChange.IsEnabled = false;

            ShowAllTag();
            // we can use the update Ui method to minimize the code instead of writing it twice
            UpdateUiList();
        }


        
        /////////////////////// EVENTS IN WPF ////////////////////////////////////
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // create new object
            Recipe recipe = new Recipe();
            // using database class connection
            using (CarrotContext context= new CarrotContext())
            {
                // if statement to show what happens if the textbox is null or is less than three characters
                if (txbRecipeName.Text.Length == 0)
                {
                    MessageBox.Show("You must have letters in a name for it to exist :) ");
                }
                else if (txbRecipeName.Text.Length < 3)
                {
                    MessageBox.Show("The rule here is at least the name must have three letters! ;)");
                }
                else if (cbxTag.SelectedIndex < 0)
                {
                    MessageBox.Show("Give tag first before saving");
                }
                else
                {

                    // store the input from textbox to created object
                    recipe.RecipeName = txbRecipeName.Text;
                    new RecipeRepo(context).AddRecipe(recipe);
                    // from the combobox give it a tag and what type it is
                    ComboBoxItem selectedItem = cbxTag.SelectedItem as ComboBoxItem;
                    Tags SelectedTag = selectedItem.Tag as Tags;
                    // give it the same id as the selected tag
                    Tags tagdb = context.tags.FirstOrDefault(t => t.TagId == SelectedTag.TagId);

                    // give the recipe tag id the same id
                    recipe.Tags = tagdb;
                    // save changes to database
                    context.SaveChanges();
                    // print out a message with current recipe name that was added
                    MessageBox.Show($"{recipe.RecipeName} was now added to the list");
                    // clear the textbox field
                    txbRecipeName.Clear();
                    // refreshes the listview
                    UpdateUiList();
                   
                }
            }
        }
        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {

            using (CarrotContext context = new CarrotContext())
            {
                    // store the selected item in item
                    ListViewItem? item = lvlRecipeList.SelectedItem as ListViewItem;
                    // the item that is selected is a recipe
                    Recipe reciperecipe = item.Tag as Recipe;
 

                    btnSelect.IsEnabled = true;
                    // opens a new window with current recipe 
                    DetailsWindow detailsWindow = new(reciperecipe);
                    // opens the window
                    detailsWindow.Show();
                 

                
            }
                    // close current window
                    Close();
        }
        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            // simpel info button to give out information about the app
            MessageBox.Show("                                            Welcome to info\r\r " +
                "When added a recipe you can now see the recipe in the list!\r " +
                "To add ingridients or tags choose from the list and then press the details button\r\r\r Details and Delete button is disabled until user selects a Recipe from the list!");
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // the selected item in listview 
            ListViewItem selectedItem = lvlRecipeList.SelectedItem as ListViewItem;
            // the selected item is a recipe
            Recipe recipe = selectedItem.Tag as Recipe;

            // Using the connection to datbase by using Carrotcontext class
            using (CarrotContext context = new CarrotContext())
            {
                //messagebox with yes and no option
                if (MessageBox.Show("you sure u wanna delete recipe?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //do no stuff
                }
                else
                {
                    // send the selected recipe item to the method in repo class
                    new RecipeRepo(context).RemoveRecipe(recipe);
                
                    // save changes to database
                    context.SaveChanges();
                    
                }



            }
            // update the database
                UpdateUiList();
        }
        private void lvlRecipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // event for list view if something is chosen or not

            // if something is more selected than null "do this"
            if(lvlRecipeList.SelectedIndex > -1)
            {
                btnSelect.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnChange.IsEnabled= true;
                
            }
            // if something is not selected do this
            else if (lvlRecipeList.SelectedIndex == -1)
            {
                btnSelect.IsEnabled = false;
                btnDelete.IsEnabled = false;
                btnChange.IsEnabled= false;
            }
        }
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            // The selected item in listview has a tag send it to another window for further use
            ListViewItem selectedItem = lvlRecipeList.SelectedItem as ListViewItem;
            // the selected item is a recipe
            Recipe recipe = selectedItem.Tag as Recipe;
            // Open new window and send the recipe with it
            ChangeRecipeNameWindow changeRecipeNameWindow = new(recipe);
            // Open the window
            changeRecipeNameWindow.Show();
            // close current window
            Close();


        }

        ////////////////////////// Methods //////////////////////////////

        private void UpdateUiList()
        {
            // clears the items in listview
                    lvlRecipeList.Items.Clear();
            // fills the listview again with recipe items from database
            using (CarrotContext context = new CarrotContext())
            {
                // save the data from database into AllRecipes then we use it for further use
               var AllRecipes = new RecipeRepo(context).GetAllRecipes();

                // foreach item in the List give a tag and then print out to listview
                foreach (var allRecipe in AllRecipes)
                {
                    ListViewItem item = new ListViewItem();

                    item.Tag = allRecipe;
                    item.Content = allRecipe.RecipeName;
                    lvlRecipeList.Items.Add(item);
                    lvlRecipeList.SelectedItem = btnSelect;
                }

            }
        }



        private void ShowAllTag()
        {
            using (CarrotContext context = new CarrotContext())
            {
                // save all tags in alltags which is a list, get all tags from database
                var allTags = new TagRepo(context).GetTags();

                // foreach thing in the list, give it a tag and what content to print later add to combo box
                foreach (var tag in allTags)
                {
                    ComboBoxItem item = new();
                    item.Tag = tag;
                    item.Content = tag.TagName;
                    cbxTag.Items.Add(item);
                }
            }
        }

        
    }
}
