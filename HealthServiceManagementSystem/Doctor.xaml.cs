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
    /// Interaction logic for Doctor.xaml
    /// </summary>
    public partial class Doctor : Page
    {

        public Doctor()
        {
            InitializeComponent();
        }

        private void submenuAddNewDoctor_Click(object sender, RoutedEventArgs e)
        {
            stkDoctorDetails.Visibility = Visibility.Visible;
        }

    }
}
