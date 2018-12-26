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
using System.Windows.Navigation;

namespace HealthServiceManagementSystem
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        HealthServiceEntities db = new HealthServiceEntities("metadata=res://*/HealthClinicModel.csdl|res://*/HealthClinicModel.ssdl|res://*/HealthClinicModel.msl;provider=System.Data.SqlClient;provider connection string='data source=172.20.10.12;initial catalog=HealthSevice;persist security info=True;user id=paul;password=Venus1234;MultipleActiveResultSets=True;App=EntityFramework'");

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

            SignInDocNurse();

           
        }

        private void SignInDocNurse()
        {
            foreach (var doc in db.Doctors.Where(t => t.UserID == user.UserId))
            {
                doc.OnDuty = true;
            }
            int saveDoc = db.SaveChanges();

            foreach (var nurse in db.Nurses.Where(t => t.UserID == user.UserId))
            {
                nurse.OnDuty = true;
            }
            int saveNurse = db.SaveChanges();

            if (saveDoc == 1 || saveNurse == 1)
            {
                MessageBox.Show("On Duty updated successfully!", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SignOutDocNurse()
        {
            foreach (var doc in db.Doctors.Where(t => t.UserID == user.UserId))
            {
                doc.OnDuty = false;
            }
            int saveDoc = db.SaveChanges();

            foreach (var nurse in db.Nurses.Where(t => t.UserID == user.UserId))
            {
                nurse.OnDuty = false;
            }
            int saveNurse = db.SaveChanges();

            if (saveDoc == 1 || saveNurse == 1)
            {
                MessageBox.Show("Signed out successfully!", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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
            mainFrame.Navigate(new Admin());
        }

        public void mnuDoctors_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Doctor());
        }

        public void mnuNurses_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Nurse());
        }

        public void mnuPatients_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Patient());
        }

        public void mnuOnDuty_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new OnDuty());
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.mainFrame.CanGoBack)
            {
                this.mainFrame.GoBack();
            }
            else
            {
                MessageBox.Show("No entries in back navigation history!");
            }
        }

        private void mnuSignOut_Click(object sender, RoutedEventArgs e)
        {
            SignOutDocNurse();
            this.Close();
            Environment.Exit(0);
        }
    }
}
