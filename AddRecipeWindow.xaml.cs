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

namespace YellowCarrot
{
    /// <summary>
    /// Interaction logic for AddRecipeWindow.xaml
    /// </summary>
    public partial class AddRecipeWindow : Window
    {

       // function to this window //
       // textboxes to fill information about the recipe
       // a button to save the recipe
       // if something is null in the process proceed with a warning message
        public AddRecipeWindow()
        {
            InitializeComponent();
        }
    }
}
