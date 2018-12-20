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

namespace HealthServiceManagementSystem
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();

            

        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            mainFrame.Navigate(admin);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDoctors_Click(object sender, RoutedEventArgs e)
        {
            DoctorView doctorView = new DoctorView();
            mainFrame.Navigate(doctorView);
        }

        private void btnNurses_Click(object sender, RoutedEventArgs e)
        {
            NurseView nurseView = new NurseView();
            mainFrame.Navigate(nurseView);
        }

        private void btnPatients_Click(object sender, RoutedEventArgs e)
        {
            PatientView patientView = new PatientView();
            mainFrame.Navigate(patientView);
        }

        private void btnOnDuty_Click(object sender, RoutedEventArgs e)
        {
            OnDuty onduty = new OnDuty();
            mainFrame.Navigate(onduty);
        }
    }
}
