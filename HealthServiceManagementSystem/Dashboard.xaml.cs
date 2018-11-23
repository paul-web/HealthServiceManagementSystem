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
using System.Windows.Shapes;

namespace HealthServiceManagementSystem
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public User user = new User();

        public Dashboard()
        {
            InitializeComponent();

            

        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            adminFrame.Navigate(admin);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDoctors_Click(object sender, RoutedEventArgs e)
        {
            Doctor doctor = new Doctor();
            doctorFrame.Navigate(doctor);
        }

        private void btnNurses_Click(object sender, RoutedEventArgs e)
        {
            Nurse nurse = new Nurse();
            nurseFrame.Navigate(nurse);
        }

        private void btnPatients_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = new Patient();
            patientFrame.Navigate(patient);
        }

        private void btnOnDuty_Click(object sender, RoutedEventArgs e)
        {
            OnDuty onduty = new OnDuty();
            onDutyFrame.Navigate(onduty);
        }

        private void CheckUserAccess()
        {
            if (user.LevelID == 1)
            {
                btnAdmin.Visibility = Visibility.Visible;
            }

            if (user.LevelID == 2)
            {
                btnPatients.Visibility = Visibility.Visible;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckUserAccess();
        }
    }
}
