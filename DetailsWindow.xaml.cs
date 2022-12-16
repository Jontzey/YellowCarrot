using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        //Functions for this window //-
        //TODO
        // a listview showing detials of a recipe name,ingridients and possibly (tags and steps)
        // a button to allow to unlock to possibilty to change information about the recipe
        // a button to save all changes
        // a button to remove a ingridient


        //field variables
        private Recipe recipes = new Recipe();
        private int theID = new();
        
        public DetailsWindow(Recipe recipeId)
        {
            InitializeComponent();

            showIngridient(recipeId.RecipeId);
            // sets the field variable to what we sent from mainwindow to this window(details window)
            this.theID = recipeId.RecipeId;
            recipes = recipeId;

            // sets the button to be disabled from beginning
            btnRemoveIngridient.IsEnabled = false;
            btnChangeIngridient.IsEnabled = false;
            
            btnSaveIngridient.IsEnabled = false;
            btnSeeTags.IsEnabled = false;

            

            using (CarrotContext context= new CarrotContext())
            {
                    // the field variable is the same as the one we sent from mainwindow
                    // gets the correct recipe from database, checks the current id with the id in database
                    recipes = context.recipes.Where(x => x.RecipeId == recipeId.RecipeId).FirstOrDefault();

                    //textbox text same as the recipe name
                    txtRecipeName.Text = this.recipes.RecipeName;

            }
            
        } //Check



        ///////////////////////// EVENTS WHEN SOMETHING HAPPENS IN THE UI //////////////////////////////////////////////
        
        // GOES BACK TO MAINWINDOW
        private void btnExitToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            // Opens mainwindow and closes current window
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            //closes current window
            Close();
        }

        

        // save button for each ingridient
        private void btnSaveIngridient_Click(object sender, RoutedEventArgs e)

        {
            using (CarrotContext context = new CarrotContext())
            {
                // if textbox is the same as no charactes == null
                if(txbIngridientName.Text.Length == 0)
                {
                    MessageBox.Show("Ingridient must have a name! even if its one letter!");
                }
                else
                {

                    // using repo class and its method and creating new ingrident object in the constructor and givin it data
                    new IngridentRepository(context).AddIngrident(new Ingridient()
                    {
                        Name = txbIngridientName.Text,
                        Quantity = txbIngridientQuantity.Text,
                        recipeId = recipes.RecipeId,
                    });
                
                    // save the changes, without this nothing will update in the database
                    context.SaveChanges();
                    // simpel message
                    MessageBox.Show("Ingridient now was added!");
                    // refreshes all items in the window
                    UpdateUI();
                }

            }
                
        }


        // What happens when a item in the list is selected
        private void lvlIngridiens_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // if something in listview is selected more than nothing do this
            if(lvlIngridiens.SelectedIndex > -1 )
            {
               
            }
            // if nothing is selected do this
            else if (lvlIngridiens.SelectedIndex == -1)
            {
                // if something is not selected the button shall not be available
                btnRemoveIngridient.IsEnabled = false;
                btnChangeIngridient.IsEnabled = false;
                
            }
        }

        // REMOVE BUTTON
        private void btnRemoveIngridient_Click(object sender, RoutedEventArgs e)
        {
            //Set the selected item in listview
            ListViewItem selectedItem = lvlIngridiens.SelectedItem as ListViewItem;
            // say what the item is and give it a tag
            Ingridient ingridient = selectedItem.Tag as Ingridient;

            // send it to repository class for deletion
            //DeleteIngridient(ingridient);
            using (CarrotContext context = new CarrotContext())
            {
                
                // send to repo.cs file, see that file for further understanding
                new IngridentRepository(context).RemoveIngridient(ingridient);
                context.SaveChanges();

            }
            // refresh UI
            UpdateUI();


        }       



        private void btnUnlock_Click(object sender, RoutedEventArgs e)
        {
            // Button to unlock all buttons and disable the unlock button
            btnRemoveIngridient.IsEnabled = true;
            btnChangeIngridient.IsEnabled = true;
            
            btnSaveIngridient.IsEnabled = true;
            btnSeeTags.IsEnabled = true;
            btnUnlock.IsEnabled = false;
        }


        /////////////////////////////////METHODS////////////////////////////////////////////////////////



        // shows all ingridients in the current Recipe

        private void showIngridient(int recipeId)
        {

            using (CarrotContext context = new CarrotContext())
            {
                // shows the current recipes ingridients, the code saves all ingridients from database with the same recipe id in the var I which is a list
                var I = new IngridentRepository(context).GetIngridient(recipeId);

                // forach thing in the list
                foreach(Ingridient currentRecipe in I)
                {
                    ListViewItem item = new();
                    item.Tag = currentRecipe;
                    item.Content = $"{currentRecipe.Name}  {currentRecipe.Quantity}";
                    lvlIngridiens.Items.Add(item);

                }
            }

        } //Check

        // Updates the UI refreshes all the content
        private void UpdateUI()
        {
            // clear all boxes and items
            lvlIngridiens.Items.Clear();
            txbIngridientName.Clear();
            txbIngridientQuantity.Clear();
            // method to show stuff in listview
            showIngridient(theID);
        } // Check

        private void btnChangeIngridient_Click(object sender, RoutedEventArgs e)
        {
            // if nothing in listview is selected when pressing the button
            if(lvlIngridiens.SelectedItem == null)
            {
                MessageBox.Show("You forgot to choose what ingridient from list!");
            }
            else
            {
                // give the ingridient a tag and then send it to another window
                ListViewItem SelectedItem = lvlIngridiens.SelectedItem as ListViewItem;
                Ingridient ingridient = SelectedItem.Tag as Ingridient;

            
                ChangeIngridientWindow changeIngridientWindow = new(ingridient,recipes);
                changeIngridientWindow.Show();
                Close();
            }

           
        }

        private void btnSeeTags_Click(object sender, RoutedEventArgs e)
        {
            //Opens the see tags window
            ChangeTagsWindow changeTagsWindow = new(recipes);
            changeTagsWindow.Show();
            //close current window
            Close();

        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            // simpel message
            MessageBox.Show
                ("Here you can add Ingrident to recipe and make changes and remove if you want\r \r" +
                "NOTE!\r" +
                "In Quantity section you can write text as well!\r " +
                "this is so you as User can put in as well the measure of the quantity\r" +
                "its not a must to add quantity, you can later add it if you want by pressing change button");
        }

        

    }
}
