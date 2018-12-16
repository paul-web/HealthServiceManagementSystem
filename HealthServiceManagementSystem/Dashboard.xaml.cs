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


        private void CheckUserAccess()
        {
            if (user.LevelID == 1)
            {
                mnuPatients.Visibility = Visibility.Visible;
                mnuAdmin.Visibility = Visibility.Visible;
              
            }

            if (user.LevelID == 2)
            {
                mnuPatients.Visibility = Visibility.Visible;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckUserAccess();
        }

        private void MenuItem_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void mnuShowGroups_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Message box has been clicked!");
        }

        public void mnuAdmin_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            adminFrame.Navigate(admin);
        }

        public void mnuDoctors_Click(object sender, RoutedEventArgs e)
        {
            Doctor doctor = new Doctor();
            doctorFrame.Navigate(doctor);
        }

        public void mnuNurses_Click(object sender, RoutedEventArgs e)
        {
            Nurse nurse = new Nurse();
            nurseFrame.Navigate(nurse);
        }

        public void mnuPatients_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = new Patient();
            patientFrame.Navigate(patient);
        }

        public void mnuOnDuty_Click(object sender, RoutedEventArgs e)
        {
            OnDuty onduty = new OnDuty();
            onDutyFrame.Navigate(onduty);
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
