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
    /// Interaction logic for Analytics.xaml
    /// </summary>
    public partial class Analytics : Page
    {
        HealthServiceEntities db = new HealthServiceEntities("metadata=res://*/HealthClinicModel.csdl|res://*/HealthClinicModel.ssdl|res://*/HealthClinicModel.msl;provider=System.Data.SqlClient;provider connection string='data source=172.20.10.12;initial catalog=HealthSevice;persist security info=True;user id=paul;password=Venus1234;MultipleActiveResultSets=True;App=EntityFramework'");
        private DataDisplay dataDisplay; // declare instance of DataDisplay class that stores analytics data
        private int totalDocsOnDuty, totalNursesOnDuty, totalUsers; // declare variables to store analytics data

        public Analytics()
        {
            InitializeComponent();
        }
        // on page load set data from DB
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var doc in db.Doctors)
            {
                if (doc.OnDuty == true)
                {
                    totalDocsOnDuty++; // store total count of docs on duty
                }
            }

            foreach (var nurse in db.Nurses)
            {
                if (nurse.OnDuty == true)
                {
                    totalNursesOnDuty++; // store total count of nurses on duty
                }
            }

            foreach (var user in db.Users)
            {
                    totalUsers++; // store total count of users
            }
            // set data variables 
            dataDisplay = new DataDisplay { TotalDoctorsOnDuty = totalDocsOnDuty, TotalNursesOnDuty = totalNursesOnDuty, TotalUsers = totalUsers};
            // set context to view
            this.DataContext = dataDisplay;
        }
    }
}
