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
    /// Interaction logic for ChangeTagsWindow.xaml
    /// </summary>
    public partial class ChangeTagsWindow : Window
    {
        private int TheID;
        public ChangeTagsWindow(int RecipeID)
        {
            InitializeComponent();
            this.TheID= RecipeID;
            ShowAllTags(RecipeID);

        }

        private void btnRemoveTag_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowAllTags(int ID)
        {
            using (CarrotContext context = new CarrotContext())
            {
               List <Tags> tags = context.tags.Where(x => x.recipeId == ID).ToList();

                foreach (Tags tag in tags)
                {
                    ListViewItem item = new();
                    item.Tag = tag;
                    item.Content = tag.TagName;

                    lvlAllTags.Items.Add(item);
                }

            }
        }
    }
}
