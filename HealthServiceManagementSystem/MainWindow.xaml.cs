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
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace HealthServiceManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HealthServiceEntities db = new HealthServiceEntities("metadata=res://*/HealthClinicModel.csdl|res://*/HealthClinicModel.ssdl|res://*/HealthClinicModel.msl;provider=System.Data.SqlClient;provider connection string='data source=172.20.10.12;initial catalog=HealthSevice;persist security info=True;user id=paul;password=Venus1234;MultipleActiveResultSets=True;App=EntityFramework'");

        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            // declare class level user to set logs and dashboard user
            User validatedUser = new User();
            // store login success in bool
            bool login = false;
            // string variable stores email
            string currentEmail = tbxEmail.Text;
            // string variable stores password
            string currentPassword = pbxPassword.Password;

            // catch invalid sign in attempts and print error message
            if (currentEmail.Length == 0 || currentPassword.Length == 0)
            {
                MessageBox.Show("Fields can not be blank! \nYou must enter a value in each field", "Error signing in", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            // catch invalid password length and print error message
            else if (currentPassword.Length < 5)
            {
                MessageBox.Show("Passwords must have at least 5 characters!", "Error signing in", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            // if log in fields are valid run check on DB for valid user 
            else
            {
                foreach (var user in db.Users)
                {

                    if (user.Email == currentEmail && user.Password == currentPassword)
                    {
                        // if data matches DB user set login bool and set validatedUser
                        login = true; 
                        validatedUser = user;
                       
                    }
                    else
                    {
                        // reset textboxes and show errormessage label
                        tbxEmail.Text = String.Empty;
                        pbxPassword.Password = "";
                        lblErrorMessage.Visibility = Visibility.Visible;
                    }

                }
            }

            // if login is valid create validated log and set dashboard user
            if (login)
            {
                CreateLogEntry("Login", "Logged in.", Convert.ToInt16(validatedUser.UserId), validatedUser.Email);
                Dashboard dashboard = new Dashboard();

                dashboard.user = validatedUser;

                this.Hide();

                dashboard.ShowDialog();

            }
            else
            {
                // database null user has ID 1010 for logging failed user login attempts
                CreateLogEntry("Failed Login", "Didnt login.", 1010, currentEmail);
            }
        }

        // method for creating logs
        private void CreateLogEntry(string category, string description, int userID, string email)
        {
            // declare string to store log description
            string comment = $"{description} user credentials = {email}";

            Log log = new Log();
            log.UserId = userID;
            log.Category = category;
            log.Description = comment;
            log.Date = DateTime.Now;
            SaveLog(log);
        }

        // method for saving logs
        private void SaveLog(Log log)
        {
            db.Entry(log).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();

        }

        // method to exit application
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        // launch register window when register button is clicked
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();

            this.Close();
        }
    }
}
