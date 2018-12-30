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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        HealthServiceEntities db = new HealthServiceEntities("metadata=res://*/HealthClinicModel.csdl|res://*/HealthClinicModel.ssdl|res://*/HealthClinicModel.msl;provider=System.Data.SqlClient;provider connection string='data source=172.20.10.12;initial catalog=HealthSevice;persist security info=True;user id=paul;password=Venus1234;MultipleActiveResultSets=True;App=EntityFramework'");

        public Register()
        {
            InitializeComponent();
        }

        // ok button to register a user
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            int saveSuccess = 0; // DB save variable

            // string variables store textbox user  text
            string firstName = tbxFirstName.Text.Trim();
            string lastName = tbxLastName.Text.Trim();
            string userName = tbxUserName.Text.Trim();
            string password = pbxPassword.Password;
            string email = tbxEmail.Text.Trim();
            int levelID = 3;

            // declare a user to store in DB
            User user = new User();
            // set user data from text fields
            user.FirstName = firstName;
            user.LastName = lastName;
            user.UserName = userName;
            user.Password = password;
            user.Email = tbxEmail.Text.Trim();
            user.LevelID = levelID; // all new users get default 3rd level access until administrator approval

            // error check on textboxes
            if (firstName.Length != 0 && lastName.Length != 0 && userName.Length != 0 && password.Length != 0 && email.Length != 0)
            {
                // error check on password length
                if (password.Length >= 5)
                {
                    saveSuccess = SaveUser(user);
                    if (saveSuccess == 1)
                    {
                        // print register success message
                        MessageBox.Show("You have successfully registered!", "User registration", MessageBoxButton.OK, MessageBoxImage.Information);
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        // print error message if DB save is unsuccessful
                        MessageBox.Show("Error registering a user account!", "User registration", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {   // print error is password is less then 5 chars
                    MessageBox.Show("Passwords must be at least 5 characters in length.", "Error signing in", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            
            else
            {
                // if any fields are blank print error message
                MessageBox.Show("Fields can not be blank! \nYou must enter a value in each field.", "Error signing in", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        // method to save user
        public int SaveUser(User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
            int saveSuccess = db.SaveChanges();
            return saveSuccess;
        }

        // close and exit method
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }
    }
}
