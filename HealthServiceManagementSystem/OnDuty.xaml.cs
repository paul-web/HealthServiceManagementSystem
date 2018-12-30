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
    /// Interaction logic for OnDuty.xaml
    /// </summary>
    public partial class OnDuty : Page
    {
        HealthServiceEntities db = new HealthServiceEntities("metadata=res://*/HealthClinicModel.csdl|res://*/HealthClinicModel.ssdl|res://*/HealthClinicModel.msl;provider=System.Data.SqlClient;provider connection string='data source=172.20.10.12;initial catalog=HealthSevice;persist security info=True;user id=paul;password=Venus1234;MultipleActiveResultSets=True;App=EntityFramework'");
        // declare list to display on duty doctors and nurses  
        List<DBLibrary.Doctor> doctors = new List<DBLibrary.Doctor>();
        List<DBLibrary.Nurse> nurses = new List<DBLibrary.Nurse>();

        public OnDuty()
        {
            InitializeComponent();
        }
        // on page load set lists of on duty doctors and nurses
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // loop doctors in DB
            foreach (var doctor in db.Doctors)
            {
                if (doctor.OnDuty == true) 
                {   // if doctor is on duty add to list
                    doctors.Add(doctor);
                }
            }
            // loop nurses in DB
            foreach (var nurse in db.Nurses)
            {
                if (nurse.OnDuty == true)
                {   // if nurse is on duty add to list
                    nurses.Add(nurse);
                }
            }
            // set list item sourses from lists
            lstDoctorOnDutyList.ItemsSource = doctors;
            lstNurseOnDutyList.ItemsSource = nurses;

        }
    }
}
