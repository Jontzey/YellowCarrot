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
        private Recipe recipes = new();
        private int theID = new();
        private Ingridient oof = new();
        public DetailsWindow(int recipeId)
        {
            InitializeComponent();

            showIngridient(recipeId);
            // sets the field variable to what we sent from mainwindow to this window(details window)
            this.theID = recipeId;

            // sets the button to be disabled from beginning
            btnRemoveIngridient.IsEnabled = false;
            btnChangeIngridient.IsEnabled = false;


            using (CarrotContext context= new CarrotContext())
            {
                    // the field variable is the same as the one we sent from mainwindow
                    // gets the correct recipe from database, checks the current id with the id in database
                    recipes = context.recipes.Where(x => x.RecipeId == recipeId).FirstOrDefault();

                    //textbox text same as the recipe name
                    txtRecipeName.Text = this.recipes.RecipeName;

            }
            
        }



        ///////////////////////// EVENTS WHEN SOMETHING HAPPENS IN THE UI //////////////////////////////////////////////
        
        // GOES BACK TO MAINWINDOW
        private void btnExitToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        // ADD BUTTON TO ADD A TAG TO RECIPES
        private void btnAddTag_Click(object sender, RoutedEventArgs e)
        {
            using (CarrotContext context= new CarrotContext())
            {
                Tags tag = new();

                tag.Recipe = context.recipes.Where(x => x.RecipeId == theID).FirstOrDefault();
                tag.TagName = txbTag.Text;

                context.tags.Add(tag);
                context.SaveChanges();



            }
        }

        // save button for each ingridient
        private void btnSaveIngridient_Click(object sender, RoutedEventArgs e)


    
        {
            


            using (CarrotContext context = new CarrotContext())
            {
                // create a ingridient 
                Ingridient ingridient = new();

                // filling the coloumns in database with data
                ingridient.Name = txbIngridientName.Text;
                ingridient.Quantity = txbIngridientQuantity.Text;
                ingridient.recipeId = recipes.RecipeId;

                // the recipe connected to the ingridient //
                // the primary key in Recipe is connected to the foreign key in ingrident //
                // By saying the ingrident recipe is has the same id as the one in the database
                ingridient.Recipe = context.recipes.Where(x => x.RecipeId == recipes.RecipeId).FirstOrDefault();

                // TEST KOD FOR REPO
                 IngridentRepository ingridentRepository = new(context);
                ingridentRepository.AddIngrident(ingridient);
                
                
                // Add the ingridient in database
                context.ingridients.Add(ingridient);
                // save the changes, without this nothing will update in the database
                context.SaveChanges();

            }

                // simpel message
                MessageBox.Show("Ingridient now was added!");
                // refreshes all items in the window
                UpdateUI();

                
        }


        // What happens when a item in the list is selected
        private void lvlIngridiens_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // if something in listview is selected more than nothing do this
            if(lvlIngridiens.SelectedIndex > -1 )
            {
                // if something is selected make the button available
                btnRemoveIngridient.IsEnabled = true;
                btnChangeIngridient.IsEnabled = true;
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
            DeleteIngridient(ingridient);
            // refresh UI
            UpdateUI();


        }

        /////////////////////////////////METHODS////////////////////////////////////////////////////////



        // shows all ingridients in the Recipe

        private void showIngridient(int recipeId)
        {
            using(CarrotContext context = new CarrotContext())
            {
                
                
               // save all ingridients in a list where the recipeid is the same as the recipe (id) we fetched from mainwindow
               List<Ingridient> GetAll = context.ingridients.Where(x => x.recipeId == recipeId).ToList();

                // incase if the list is empty, show this message
                if (GetAll == null)
                {
                    MessageBox.Show("this is empty");
                }
                else
                {
                    // foreach thing in this list, give it a tag and add to listview
                    foreach(Ingridient I in GetAll)
                    {
                        ListViewItem item = new();
                        item.Tag = I;
                        item.Content = $"{I.Name}  {I.Quantity}";
                        lvlIngridiens.Items.Add(item);

                    }
 

                }

            }

        }

        // method for deleting the ingridient in the list + database
        private void DeleteIngridient(Ingridient ingridient)
        {
            using (CarrotContext context = new CarrotContext())
            {

                IngridentRepository ingridentRepository = new(context);
                ingridentRepository.RemoveIngridient(ingridient);

                
                context.SaveChanges();

            }

        }

        // Updates the UI refreshes all the content
        private void UpdateUI()
        {
            // clear all boxes and items
            lvlIngridiens.Items.Clear();
            txbIngridientName.Clear();
            txbIngridientQuantity.Clear();
            // method to show stuff in listview
            showIngridient(theID);
        }

        private void btnChangeIngridient_Click(object sender, RoutedEventArgs e)
        {

            ListViewItem SelectedItem = lvlIngridiens.SelectedItem as ListViewItem;
            Ingridient ingridient = SelectedItem.Tag as Ingridient;
            
            ChangeIngridientWindow changeIngridientWindow = new(ingridient);
            changeIngridientWindow.Show();
           
        }

        private void btnSeeTags_Click(object sender, RoutedEventArgs e)
        {
            ChangeTagsWindow changeTagsWindow = new(theID);
            changeTagsWindow.Show();

        }
    }
}
