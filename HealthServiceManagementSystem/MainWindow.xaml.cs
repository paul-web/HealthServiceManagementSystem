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
            string currentEmail = tbxEmail.Text;
            string currentPassword = pbxPassword.Password;

            foreach (var user in db.Users)
            {
                if (user.Email == currentEmail && user.Password == currentPassword)
                {
                    Dashboard dashboard = new Dashboard();
                    dashboard.user = user;
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
    }
}
