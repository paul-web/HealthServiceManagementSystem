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
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        HealthServiceEntities db = new HealthServiceEntities("metadata=res://*/HealthClinicModel.csdl|res://*/HealthClinicModel.ssdl|res://*/HealthClinicModel.msl;provider=System.Data.SqlClient;provider connection string='data source=172.20.10.12;initial catalog=HealthSevice;persist security info=True;user id=paul;password=Venus1234;MultipleActiveResultSets=True;App=EntityFramework'");

        List<User> users = new List<User>();
        List<Log> logs = new List<Log>();

        public Admin()
        {
            InitializeComponent();
        }

        private void submenuAddNewUser_Click(object sender, RoutedEventArgs e)
        {
            stkUserDetails.Visibility = Visibility.Visible;
        }

  
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lstUserList.ItemsSource = users;
            foreach (var user in db.Users)
            {
                users.Add(user);
            }

            lstLogList.ItemsSource = logs;
            foreach (var log in db.Logs)
            {
                logs.Add(log);
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            User user = new User();
            user.FirstName = tbxFirstName.Text.Trim();
            user.LastName = tbxLastName.Text.Trim();
            user.UserName = tbxUserName.Text.Trim();
            user.Password = tbxPassword.Text.Trim();
            user.Email = tbxEmail.Text.Trim();
            user.LevelID = cbxAccessLevel.SelectedIndex;

            int saveSuccess = SaveUser(user);

            if (saveSuccess == 1)
            {
                MessageBox.Show($"User: {user.UserName} has been added to the database!", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Error saving user record.", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


            tbxFirstName.Text = "";
            tbxLastName.Text = "";
            tbxUserName.Text = "";
            tbxPassword.Text = "";
            tbxEmail.Text = "";


        }

        public int SaveUser(User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
            int saveSuccess = db.SaveChanges();
            return saveSuccess;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
