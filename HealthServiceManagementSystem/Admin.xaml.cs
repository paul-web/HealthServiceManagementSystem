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

        List<User> users = new List<User>(); // list for users
        List<Log> logs = new List<Log>(); // list to store logs
        User selectedUser = new User(); // global user variable

        // enum to store DB operation methods types 
        enum DBOperation
        {
            ADD, // used to add user to DB
            MODIFY, // used when modifying user from DB
            DELETE // used when deleting user from DB
        }

        DBOperation dBOperation = new DBOperation();

        public Admin()
        {
            InitializeComponent();
        }

        private void submenuAddNewUser_Click(object sender, RoutedEventArgs e)
        {
            dBOperation = DBOperation.ADD;
            stkUserDetails.Visibility = Visibility.Visible; // display Stackpanel
        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // refresh user list on page load
            RefreshUserList();
            
            // set logs into listview
            lstLogList.ItemsSource = logs;
            foreach (var log in db.Logs)
            {
                logs.Add(log);
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            // if user right clicked on add new user submenu, set user data from textboxes
            if (dBOperation == DBOperation.ADD)
            {
                User user = new User();
                user.FirstName = tbxFirstName.Text.Trim();
                user.LastName = tbxLastName.Text.Trim();
                user.UserName = tbxUserName.Text.Trim();
                user.Password = tbxPassword.Text.Trim();
                user.Email = tbxEmail.Text.Trim();
                user.LevelID = cbxAccessLevel.SelectedIndex;

                // store success of user save
                int saveSuccess = SaveUser(user);

                // if success, display success message, refresh user listview and clear fields
                if (saveSuccess == 1)
                {
                    MessageBox.Show($"User {user.UserName} has been added to the database!", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshUserList();
                    ClearUserDetails();
                }
                // otherwise display an error message
                else
                {
                    MessageBox.Show("Error saving user record.", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            // if user right clicked on modify user submenu, set user data from textboxes, and return user that has same UserId as the element number selected and set as selectedUser 
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
                // store success of user save
                int save = db.SaveChanges();
                // if success, display success message, refresh user listview and clear fields
                if (save == 1)
                {
                    MessageBox.Show("User modified successfully!", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshUserList();
                    ClearUserDetails();
                    stkUserDetails.Visibility = Visibility.Visible;
                }
            }


        }

        // method to save a user to DB
        public int SaveUser(User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
            int saveSuccess = db.SaveChanges();
            return saveSuccess; // return success of save as int
        }

        // method to refresh users listview
        private void RefreshUserList()
        {
            lstUserList.ItemsSource = users; // set itemsource
            users.Clear(); // clear users list

            foreach (var user in db.Users)
            {
                // remove user listing for error catching with ID 1010 from displaying in listview
                if (user.UserId != 1010)
                {
                    users.Add(user);
                }
            }
            lstUserList.Items.Refresh();     

        }

        // method to clear user details from texboxes
        private void ClearUserDetails()
        {
            tbxFirstName.Text = "";
            tbxLastName.Text = "";
            tbxUserName.Text = "";
            tbxPassword.Text = "";
            tbxEmail.Text = "";
            cbxAccessLevel.SelectedIndex = 0;
        }

        // cancel button to hide again the Stackpanel
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            stkUserDetails.Visibility = Visibility.Collapsed;
        }


        // method to set data on selection changed
        private void lstUserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstUserList.SelectedIndex > 0)
            {
                selectedUser = users.ElementAt(lstUserList.SelectedIndex);
              
                submenuModifySelectedUser.IsEnabled = true;
                submenuDeleteSelectedUser.IsEnabled = true;

                if (dBOperation == DBOperation.ADD)
                {
                    ClearUserDetails(); // reset user details after add operation
                }

                tbxFirstName.Text = selectedUser.FirstName;
                tbxLastName.Text = selectedUser.LastName;
                tbxUserName.Text = selectedUser.UserName;
                tbxPassword.Text = selectedUser.Password;
                tbxEmail.Text = selectedUser.Email;
                cbxAccessLevel.SelectedIndex = selectedUser.LevelID;
                
                
            }
        }

        // method for modifying a user
        private void submenuModifySelectedUser_Click(object sender, RoutedEventArgs e) 
        {
            stkUserDetails.Visibility = Visibility.Visible;
            dBOperation = DBOperation.MODIFY;

        }

        // method for deleting a user
        private void submenuDeleteSelectedUser_Click(object sender, RoutedEventArgs e)
        {
            // use where clause to return the specified user with matching ID
            db.Users.RemoveRange(db.Users.Where(t => t.UserId == selectedUser.UserId));
            int saveSuccess = db.SaveChanges(); // store success of operation as int
            // if successful deletion occurred display message
            if (saveSuccess == 1)
            {
                MessageBox.Show("User deleted successfully!", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshUserList();
                ClearUserDetails();
                stkUserDetails.Visibility = Visibility.Visible;
            }
            // otherwise display error message
            else
            {
                MessageBox.Show("Error deleting user record.", "Delete user from Database", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
