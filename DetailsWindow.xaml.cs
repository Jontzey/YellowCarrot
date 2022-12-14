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
        // a button to allow to unlock to possibilty to change information om det recept
        // a button to save all changes
        // a button to remove a ingridient


        //field variables
        private Recipe recipes = new();
        private Ingridient oof = new();
       
        public DetailsWindow(int recipeId)
        {
            InitializeComponent();
            showIngridient(recipeId);
            
           

            

               
            using (CarrotContext context= new CarrotContext())
            {

                if(lvlIngridiens.Items.Count < 0)
                {
                    MessageBox.Show("Right now the list is empty");
                }

                else
                {
                    //this.Ingridient = context.ingridients.Where(i => i.recipeId == recipeId).FirstOrDefault();

                    // the field variable is the same as the one we sent from mainwindow
                    recipes = context.recipes.Where(x => x.RecipeId == recipeId).FirstOrDefault();
                    txtRecipeName.Text = this.recipes.RecipeName;



                    
                    

                }

                

                

            }
            
        }

        private void btnExitToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void btnAddTag_Click(object sender, RoutedEventArgs e)
        {

        }

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

                
                
                

                context.ingridients.Add(ingridient);
                
                context.SaveChanges();

            }


            txbIngridientName.Clear();
                MessageBox.Show("Ingridient now was added!");
                DetailsWindow detailsWindow = new(recipes.RecipeId);
                detailsWindow.Show();
                Close();

                
        }


        private void showIngridient(int recipeId)
        {
            using(CarrotContext context = new CarrotContext())
            {
                
                //oof = context.ingridients.Where(x => x.recipeId == recipeId).FirstOrDefault();
                
               List<Ingridient> GetAll = context.ingridients.Where(x => x.recipeId == recipeId).ToList();


                if (GetAll == null)
                {
                    MessageBox.Show("this is emty");
                }
                else
                {
                    foreach(Ingridient d in GetAll)
                    {
                        ListViewItem item = new();
                        item.Tag = d;
                        item.Content = d.Name;
                        lvlIngridiens.Items.Add(item);

                    }
                    //ListViewItem item = new();
                    //item.Tag = oof;
                    //item.Content = oof.Name;

                    //lvlIngridiens.Items.Add(item);



                }






            }

        }
    }
}
