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
            this.theID = recipeId;

            btnRemoveIngridient.IsEnabled = false;
            
               
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

                
                
                
                // Add the ingridient in database
                context.ingridients.Add(ingridient);
                // save the changes, without this nothing will update in the database
                context.SaveChanges();

            }

                
                // simpel message
                MessageBox.Show("Ingridient now was added!");

                // open upp a new window with current recipe, just to update UI
                DetailsWindow detailsWindow = new(recipes.RecipeId);
                detailsWindow.Show();
                // closes the current window after the new window pops up
                Close();

                
        }


        // What happens when a item in the list is selected
        private void lvlIngridiens_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lvlIngridiens.SelectedIndex > 0 )
            {
                btnRemoveIngridient.IsEnabled = true;
            }
            else if (lvlIngridiens.SelectedIndex == -1)
            {
                btnRemoveIngridient.IsEnabled = false;
                
            }
        }

        // REMOVE BUTTON
        private void btnRemoveIngridient_Click(object sender, RoutedEventArgs e)
        {

            ListViewItem selectedItem = lvlIngridiens.SelectedItem as ListViewItem;
            Ingridient ingridient = selectedItem.Tag as Ingridient;

            IngridentRepository ingridentRepository = new();

            DeleteIngridient(ingridient);
        }

        /////////////////////////////////METHODS////////////////////////////////////////////////////////



        // shows all ingridients in the Recipe
        private void showIngridient(int recipeId)
        {
            using(CarrotContext context = new CarrotContext())
            {
                
                
                
               List<Ingridient> GetAll = context.ingridients.Where(x => x.recipeId == recipeId).ToList();


                if (GetAll == null)
                {
                    MessageBox.Show("this is empty");
                }
                else
                {
                    foreach(Ingridient d in GetAll)
                    {
                        ListViewItem item = new();
                        item.Tag = d;
                        item.Content = $"{d.Name}  {d.Quantity}";
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

                

                context.ingridients.Remove(ingridient);
                context.SaveChanges();

            }

            DetailsWindow detailsWindow = new(theID);
            detailsWindow.Show();
            Close();
        }

    }
}
