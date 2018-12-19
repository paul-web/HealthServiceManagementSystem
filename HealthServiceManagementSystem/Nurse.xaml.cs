using DBLibrary;
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
        HealthServiceEntities db = new HealthServiceEntities("metadata=res://*/HealthClinicModel.csdl|res://*/HealthClinicModel.ssdl|res://*/HealthClinicModel.msl;provider=System.Data.SqlClient;provider connection string='data source=172.20.10.12;initial catalog=HealthSevice;persist security info=True;user id=paul;password=Venus1234;MultipleActiveResultSets=True;App=EntityFramework'");

        List<Nurse> nurses = new List<Nurse>();

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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lstNurseList.ItemsSource = nurses;

            foreach (var nurse in db.Nurses)
            {
                nurses.Add(nurse);
            }
        }
    }
}
