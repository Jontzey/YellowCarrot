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
    /// Interaction logic for ChangeIngridientWindow.xaml
    /// </summary>
    public partial class ChangeIngridientWindow : Window
    {
        private Ingridient Grönsaken;
        public ChangeIngridientWindow(Ingridient ingridient)
        {
            InitializeComponent();
            
            this.Grönsaken = ingridient;
            
        }

        private void btnSaveChange_Click(object sender, RoutedEventArgs e)
        {
            using (CarrotContext context = new CarrotContext())
            {

                Grönsaken.Name = txbName.Text;
                Grönsaken.Quantity = txbInfo.Text;  

                new IngridentRepository(context).updateIngridient(Grönsaken);
                context.SaveChanges();

                DetailsWindow detailsWindow = new(Grönsaken.recipeId);
                detailsWindow.Show();
                Close();
            }


        }
    }
}
