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
        User selectedUser = new User();

        enum DBOperation
        {
            ADD,
            MODIFY,
            DELETE
        }

        DBOperation dBOperation = new DBOperation();

        public Admin()
        {
            InitializeComponent();
        }

        private void submenuAddNewUser_Click(object sender, RoutedEventArgs e)
        {
            dBOperation = DBOperation.ADD;
            stkUserDetails.Visibility = Visibility.Visible;
        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshUserList();
            
            lstLogList.ItemsSource = logs;
            foreach (var log in db.Logs)
            {
                logs.Add(log);
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (dBOperation == DBOperation.ADD)
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
                    MessageBox.Show($"User {user.UserName} has been added to the database!", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshUserList();
                    ClearUserDetails();
                }
                else
                {
                    MessageBox.Show("Error saving user record.", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            if (dBOperation == DBOperation.MODIFY)
            {
                foreach (var user in db.Users.Where(t => t.UserId == selectedUser.UserId))
                {
                    user.FirstName = tbxFirstName.Text.Trim();
                    user.LastName = tbxLastName.Text.Trim();
                    user.UserName = tbxUserName.Text.Trim();
                    user.Password = tbxPassword.Text.Trim();
                    user.Email = tbxEmail.Text.Trim();
                    user.LevelID = cbxAccessLevel.SelectedIndex;
                }
                int save = db.SaveChanges();
                if (save == 1)
                {
                    MessageBox.Show("User modified successfully!", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshUserList();
                    ClearUserDetails();
                    stkUserDetails.Visibility = Visibility.Visible;
                }
            }


        }

        public int SaveUser(User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
            int saveSuccess = db.SaveChanges();
            return saveSuccess;
        }

        private void RefreshUserList()
        {
            lstUserList.ItemsSource = users;
            users.Clear();

            foreach (var user in db.Users)
            {
                users.Add(user);
            }
            lstUserList.Items.Refresh();     

        }

        private void ClearUserDetails()
        {
            tbxFirstName.Text = "";
            tbxLastName.Text = "";
            tbxUserName.Text = "";
            tbxPassword.Text = "";
            tbxEmail.Text = "";
            cbxAccessLevel.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }



        private void lstUserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstUserList.SelectedIndex > 0)
            {
                selectedUser = users.ElementAt(lstUserList.SelectedIndex);
              
                submenuModifySelectedUser.IsEnabled = true;
                submenuDeleteSelectedUser.IsEnabled = true;

                if (dBOperation == DBOperation.ADD)
                {
                    ClearUserDetails();
                }

                tbxFirstName.Text = selectedUser.FirstName;
                tbxLastName.Text = selectedUser.LastName;
                tbxUserName.Text = selectedUser.UserName;
                tbxPassword.Text = selectedUser.Password;
                tbxEmail.Text = selectedUser.Email;
                cbxAccessLevel.SelectedIndex = selectedUser.LevelID;
                
                
            }
        }


        private void submenuModifySelectedUser_Click(object sender, RoutedEventArgs e) 
        {
            stkUserDetails.Visibility = Visibility.Visible;
            dBOperation = DBOperation.MODIFY;

        }


        private void submenuDeleteSelectedUser_Click(object sender, RoutedEventArgs e)
        {

            db.Users.RemoveRange(db.Users.Where(t => t.UserId == selectedUser.UserId));
            int saveSuccess = db.SaveChanges();
            if (saveSuccess == 1)
            {
                MessageBox.Show("User deleted successfully!", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshUserList();
                ClearUserDetails();
                stkUserDetails.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Error deleting user record.", "Delete user from Database", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
