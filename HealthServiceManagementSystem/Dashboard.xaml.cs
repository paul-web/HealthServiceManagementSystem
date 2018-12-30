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
        // declare and instantiate new user variable
        public User user = new User();
        Doctor doctor = new Doctor(); // doctor variable to set permissions
        Nurse nurseAccess = new Nurse(); // nurse variable to set permissions
        Admin admin = new Admin(); // admin variable to set permissions


        public Dashboard()
        {
            InitializeComponent();

        }

        // method to check user access and set views accordingly
        private void CheckUserAccess()
        {
            // level 1 adds patients and admin panel to view
            if (user.LevelID == 1)
            {
                mnuPatients.Visibility = Visibility.Visible;
                mnuAdmin.Visibility = Visibility.Visible;
                doctor.mnuDoctorListOverview.IsEnabled = true;
                nurseAccess.mnuNurseListOverview.IsEnabled = true;
                admin.mnuUserListOverview.IsEnabled = true;

            }
            // level 1 adds patients to view
            if (user.LevelID == 2)
            {
                mnuPatients.Visibility = Visibility.Visible;
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckUserAccess(); // check access on window loaded

            SignInDocNurse(); // signin the doctor/nurse if they have 2nd level access

           
        }

        // when signing in to the application this method checks if user ID matches any user ID foreign key in doctor and nurses tables. If a match is found, the doctor/nurse is signed in as On Duty to the database.
        private void SignInDocNurse()
        {
            foreach (var doc in db.Doctors.Where(t => t.UserID == user.UserId))
            {
                doc.OnDuty = true; // sign in matching doc
            }
            int saveDoc = db.SaveChanges();

            foreach (var nurse in db.Nurses.Where(t => t.UserID == user.UserId))
            {
                nurse.OnDuty = true; // sign in matching nurse
            }
            int saveNurse = db.SaveChanges();

            // display success message according to nurse or doctor sign in
            if (saveDoc == 1)
            {
                MessageBox.Show("Doctor signed in successfully!", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            if (saveNurse == 1)
            {
                MessageBox.Show("Nurse signed in successfully!", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // method to sign out a doctor/nurse from the appllcation and from the clinic
        private void SignOutDocNurse()
        {
            foreach (var doc in db.Doctors.Where(t => t.UserID == user.UserId))
            {
                doc.OnDuty = false; // set on duty bool to false if IDs match
            }
            int saveDoc = db.SaveChanges();

            foreach (var nurse in db.Nurses.Where(t => t.UserID == user.UserId))
            {
                nurse.OnDuty = false; // set on duty bool to false if IDs match
            }
            int saveNurse = db.SaveChanges();

            if (saveDoc == 1 || saveNurse == 1)
            {
                // print successful sign out message
                MessageBox.Show("Signed out successfully!", "Sign out", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // method to display groups on menu item click 
        private void mnuShowGroups_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Message box has been clicked!");
        }

        // navigate to admin page
        public void mnuAdmin_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(admin);
        }
        // navigate to doctor page
        public void mnuDoctors_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(doctor);
        }

        // navigate to nurse page
        public void mnuNurses_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(nurseAccess);
        }

        // navigate to patient page
        public void mnuPatients_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Patient());
        }
        // navigate to on duty page
        public void mnuOnDuty_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new OnDuty());
        }
        // exit application on click of exit button 
        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        // navigate back through pages on back button click
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

        // sign out doctor/nurse from DB on duty setting and close application
        private void mnuSignOut_Click(object sender, RoutedEventArgs e)
        {
            SignOutDocNurse();
            this.Close();
            Environment.Exit(0);
        }

        // navigate to on analytics page
        private void mnuAnalytics_Checked(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Analytics());
        }

        // navigate to on users view
        private void manageUsers_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Admin());
        }

        // method for searching doctor listings
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            // store user search text
            string userSearch = tbxSearchItem.Text.Trim();

            // check to see that search text has the minimum of 5 characters
            if (userSearch.Length <= 2)
            {
                // print error message
                MessageBox.Show("Search must have at least 3 characters!", "Search doctors", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                // loop doctors
                foreach (var docSearch in db.Doctors)
                {
                    // if the search text is greater than 2 chars and is contained in the docs last name print all results to screen.
                    if (docSearch.LastName.Contains(userSearch))
                    {
                        MessageBox.Show("Search results:\nFound Doctor: " + docSearch.FirstName + " " + docSearch.LastName + "\nPhone: " + docSearch.PhoneNo + "\nAddress: " + docSearch.Address + "\nOn Duty = " + (docSearch.OnDuty ? "Yes" : "No"), "Search doctors", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            
        }
    }
}
