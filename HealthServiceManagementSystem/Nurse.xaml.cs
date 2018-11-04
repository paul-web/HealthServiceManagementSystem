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

namespace HealthServiceManagementSystem
{
    /// <summary>
    /// Interaction logic for Nurse.xaml
    /// </summary>
    public partial class Nurse : Page
    {
        public Nurse()
        {
            InitializeComponent();
        }

        private void submenuAddNewNurse_Click(object sender, RoutedEventArgs e)
        {
            stkNurseDetails.Visibility = Visibility.Visible;
        }

        private void submenuAddNewNurse_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
