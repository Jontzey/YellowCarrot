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
    /// Interaction logic for ChangeTagsWindow.xaml
    /// </summary>
    public partial class ChangeTagsWindow : Window
    {
        private Recipe currentRecipe = new();
        public ChangeTagsWindow(Recipe Recipe)
        {
            InitializeComponent();
            // the Recipe we fetched is the same as the one we created as field variable
            this.currentRecipe = Recipe;
            //Methods to show tags
            ShowAllTags();
            ShowCurrentTag();
            

        }

     
        // exit button
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            //goes back to previous window
            DetailsWindow detailsWindow = new(currentRecipe);
            detailsWindow.Show();
            // closes current window
            Close();
        }

        private void btnChangeTag_Click(object sender, RoutedEventArgs e)
        {
            using(CarrotContext context = new CarrotContext())
            {
                // gives combo box selection a tag then say it is a tag class
                ComboBoxItem selectedItem = cbxAllTags.SelectedItem as ComboBoxItem;
                Tags theTag = selectedItem.Tag as Tags;

                // uses the database to see if the tag is the same in combobox
                Tags tagdb = context.tags.FirstOrDefault(t => t.TagId == theTag.TagId);

                // input the new tag into current recipe 
                currentRecipe.Tags = tagdb;
                // update the recipe tag coloumn
                context.recipes.Update(currentRecipe);
                // save changes
                context.SaveChanges();

                DetailsWindow detailsWindow = new(currentRecipe);
                detailsWindow.Show();
                Close();
                
            }
        }

        private void ShowAllTags()
        {
            using (CarrotContext context = new CarrotContext())
            {
                // Gets all tags in database then save in a list == Alltags
               var AllTags = new TagRepo(context).GetTags();

                // foreach thing in the list do this
                foreach (var tag in AllTags)
                {
                    ComboBoxItem item = new();
                    item.Tag = tag;
                    item.Content = tag.TagName;
                    cbxAllTags.Items.Add(item);
                }
            }
        }

        private void ShowCurrentTag()
        {
            // samething as showalltags method but only show the current recipes tag
            using (CarrotContext context =new CarrotContext())
            {
                //Gets all tags witg the same id as the recipe tags id
                var CurrentTag = context.tags.Where(x=> x.TagId == currentRecipe.TagsId).ToList();

                foreach(var alltags in CurrentTag)
                {
                    ListViewItem item = new ListViewItem();
                    item.Tag = alltags;
                    item.Content = alltags.TagName;
                    lvlCurrentTags.Items.Add(item);
                }
               
            }
        }
    }
}
