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

        DBLibrary.Doctor d = new DBLibrary.Doctor();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            User validatedUser = new User();
            bool login = false;
            string currentEmail = tbxEmail.Text;
            string currentPassword = pbxPassword.Password;

            // catch invalid sign in attempts
            if (currentEmail == "" || currentPassword == "")
            {
                MessageBox.Show("Fields can not be blank! \nYou must enter a value in each field", "Error signing in", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            // if log in fields are valid run check on DB for valid user 
            else
            {

                foreach (var user in db.Users)
                {


                    if (user.Email == currentEmail && user.Password == currentPassword)
                    {
                        login = true;
                        validatedUser = user;
                        Dashboard dashboard = new Dashboard();
                        dashboard.user = validatedUser;

                        this.Hide();

                        dashboard.ShowDialog();
                    }
                    else
                    {
                        tbxEmail.Text = String.Empty;
                        pbxPassword.Password = "";
                        lblErrorMessage.Visibility = Visibility.Visible;
                    }

                }
            }


            if (login)
            {
                CreateLogEntry("Login", "Logged in.", Convert.ToInt16(validatedUser.UserId), validatedUser.Email);

            }
            else
            {
                // database null user has ID 1010 for logging failed user login attempts
                CreateLogEntry("Failed Login", "Didnt login.", 1010, currentEmail);
            }
        }

        private void CreateLogEntry(string category, string description, int userID, string email)
        {

            string comment = $"{description} user credentials = {email}";

            Log log = new Log();
            log.UserId = userID;
            log.Category = category;
            log.Description = comment;
            log.Date = DateTime.Now;
            SaveLog(log);
        }

        private void SaveLog(Log log)
        {
            db.Entry(log).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();

            // other option: int result = db.SaveChanges(), return result, return type int
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();

            this.Close();
        }
    }
}
