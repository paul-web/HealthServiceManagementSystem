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
    /// Interaction logic for UserGroups.xaml
    /// </summary>
    public partial class UserGroups : Page
    {
        HealthServiceEntities db = new HealthServiceEntities("metadata=res://*/HealthClinicModel.csdl|res://*/HealthClinicModel.ssdl|res://*/HealthClinicModel.msl;provider=System.Data.SqlClient;provider connection string='data source=172.20.10.12;initial catalog=HealthSevice;persist security info=True;user id=paul;password=Venus1234;MultipleActiveResultSets=True;App=EntityFramework'");

        List<User> employeeUsers = new List<User>();
        List<User> docNurseUsers = new List<User>();
        List<User> adminUsers = new List<User>();


        public UserGroups()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var user in db.Users)
            {
                if (user.LevelID == 1)
                {
                    adminUsers.Add(user);
                }
                else if (user.LevelID == 2)
                {
                    docNurseUsers.Add(user);
                }
                else
                {
                    employeeUsers.Add(user);
                }
            }
            lstEmployeeList.ItemsSource = employeeUsers;
            lstDocNurseList.ItemsSource = docNurseUsers;
            lstAdminList.ItemsSource = adminUsers;
        }
    }
}
