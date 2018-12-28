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

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.FirstName = tbxFirstName.Text.Trim();
            user.LastName = tbxLastName.Text.Trim();
            user.UserName = tbxUserName.Text.Trim();
            user.Password = pbxPassword.Password;
            user.Email = tbxEmail.Text.Trim();
            user.LevelID = 3; // all new users get default 3rd level access until administrator approval

            int saveSuccess = SaveUser(user);

            if (saveSuccess == 1)
            {
                MessageBox.Show("You have successfully registered!", "User registration", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error registering a user account!", "User registration", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public int SaveUser(User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
            int saveSuccess = db.SaveChanges();
            return saveSuccess;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
