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
    /// Interaction logic for Nurse.xaml
    /// </summary>
    public partial class Nurse : Page
    {
        HealthServiceEntities db = new HealthServiceEntities("metadata=res://*/HealthClinicModel.csdl|res://*/HealthClinicModel.ssdl|res://*/HealthClinicModel.msl;provider=System.Data.SqlClient;provider connection string='data source=172.20.10.12;initial catalog=HealthSevice;persist security info=True;user id=paul;password=Venus1234;MultipleActiveResultSets=True;App=EntityFramework'");

        List<DBLibrary.Nurse> nurses = new List<DBLibrary.Nurse>();
        DBLibrary.Nurse selectedNurse = new DBLibrary.Nurse();

        enum DBOperation
        {
            ADD,
            MODIFY,
            DELETE
        }

        DBOperation dBOperation = new DBOperation();


        public Nurse()
        {
            InitializeComponent();
        }

        private void submenuAddNewNurse_Click(object sender, RoutedEventArgs e)
        {
            dBOperation = DBOperation.ADD;
            stkNurseDetails.Visibility = Visibility.Visible;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshNurseList();

        }

        private void RefreshNurseList()
        {
            lstNurseList.ItemsSource = nurses;
            nurses.Clear();

            foreach (var nurse in db.Nurses)
            {
                nurses.Add(nurse);
            }
            lstNurseList.Items.Refresh();

        }

        private void ClearNurseDetails()
        {
            tbxFirstName.Text = "";
            tbxLastName.Text = "";
            tbxAddress.Text = "";
            tbxEmail.Text = "";
            tbxPhone.Text = "";
            tbxOnDuty.Text = "";
            tbxUserID.Text = "";
        }

        private void submenuModifySelectedNurse_Click(object sender, RoutedEventArgs e)
        {
            dBOperation = DBOperation.MODIFY;
            stkNurseDetails.Visibility = Visibility.Visible;
        }

        private void submenuDeleteSelectedNurse_Click(object sender, RoutedEventArgs e)
        {
            db.Nurses.RemoveRange(db.Nurses.Where(t => t.UserID == selectedNurse.UserID));
            int saveSuccess = db.SaveChanges();
            if (saveSuccess == 1)
            {
                MessageBox.Show("Nurse deleted successfully!", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshNurseList();
                ClearNurseDetails();
            }
            else
            {
                MessageBox.Show("Error deleting nurse record.", "Delete nurse from Database", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void lstNurseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstNurseList.SelectedIndex > 0)
            {
                selectedNurse = nurses.ElementAt(lstNurseList.SelectedIndex);

                submenuModifySelectedNurse.IsEnabled = true;
                submenuDeleteSelectedNurse.IsEnabled = true;

                if (dBOperation == DBOperation.ADD)
                {
                    ClearNurseDetails();
                }

                tbxFirstName.Text = selectedNurse.FirstName;
                tbxLastName.Text = selectedNurse.LastName;
                tbxAddress.Text = selectedNurse.Address;
                tbxEmail.Text = selectedNurse.Email;
                tbxPhone.Text = selectedNurse.PhoneNo.ToString();
                tbxOnDuty.Text = selectedNurse.OnDuty.ToString();
                tbxUserID.Text = selectedNurse.UserID.ToString();



            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (dBOperation == DBOperation.ADD)
            {
                DBLibrary.Nurse nurse = new DBLibrary.Nurse();
                nurse.FirstName = tbxFirstName.Text.Trim();
                nurse.LastName = tbxLastName.Text.Trim();
                nurse.Address = tbxAddress.Text.Trim();
                nurse.Email = tbxEmail.Text.Trim();
                nurse.PhoneNo = Int32.Parse(tbxPhone.Text.Trim());
                nurse.OnDuty = Convert.ToBoolean(tbxOnDuty.Text.Trim());
                nurse.UserID = Int32.Parse(tbxUserID.Text.Trim());

                int saveSuccess = SaveNurse(nurse);

                if (saveSuccess == 1)
                {
                    MessageBox.Show($"Nurse {nurse.FirstName} {nurse.LastName} has been added to the database!", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshNurseList();
                    ClearNurseDetails();
                    stkNurseDetails.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Error saving nurse record.", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            if (dBOperation == DBOperation.MODIFY)
            {
                foreach (var nurse in db.Nurses.Where(t => t.UserID == selectedNurse.UserID))
                {
                    nurse.FirstName = tbxFirstName.Text.Trim();
                    nurse.LastName = tbxLastName.Text.Trim();
                    nurse.Address = tbxAddress.Text.Trim();
                    nurse.Email = tbxEmail.Text.Trim();
                    nurse.PhoneNo = Int32.Parse(tbxPhone.Text.Trim());
                    nurse.OnDuty = Convert.ToBoolean(tbxOnDuty.Text.Trim());
                    nurse.UserID = Int32.Parse(tbxUserID.Text.Trim());

                }
                int save = db.SaveChanges();
                if (save == 1)
                {
                    MessageBox.Show("Nurse modified successfully!", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshNurseList();
                    ClearNurseDetails();
                    stkNurseDetails.Visibility = Visibility.Collapsed;
                }
            }

        }

        public int SaveNurse(DBLibrary.Nurse nurse)
        {
            db.Entry(nurse).State = System.Data.Entity.EntityState.Added;
            int saveSuccess = db.SaveChanges();
            return saveSuccess;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            stkNurseDetails.Visibility = Visibility.Collapsed;
        }
    }
}
