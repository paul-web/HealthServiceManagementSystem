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
    /// Interaction logic for Doctor.xaml
    /// </summary>
    public partial class Doctor : Page
    {
        HealthServiceEntities db = new HealthServiceEntities("metadata=res://*/HealthClinicModel.csdl|res://*/HealthClinicModel.ssdl|res://*/HealthClinicModel.msl;provider=System.Data.SqlClient;provider connection string='data source=172.20.10.12;initial catalog=HealthSevice;persist security info=True;user id=paul;password=Venus1234;MultipleActiveResultSets=True;App=EntityFramework'");

        // store list of doctors for listview
        List<DBLibrary.Doctor> doctors = new List<DBLibrary.Doctor>();
        // declare doctor for selecting
        DBLibrary.Doctor selectedDoctor = new DBLibrary.Doctor();

        // enum stores DB action methods
        enum DBOperation
        {
            ADD,
            MODIFY,
            DELETE
        }

        // declare and instantiate instance of DBOperation
        DBOperation dBOperation = new DBOperation();


        public Doctor()
        {
            InitializeComponent();
        }

        // submenu click to set DB method and show stackpanel
        private void submenuAddNewDoctor_Click(object sender, RoutedEventArgs e)
        {
            dBOperation = DBOperation.ADD;
            stkDoctorDetails.Visibility = Visibility.Visible;
        }

        // refresh doctor list on page load
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshDoctorList();
        }
        // method to refresh doctor list
        private void RefreshDoctorList()
        {
            lstDoctorList.ItemsSource = doctors;
            doctors.Clear();

            foreach (var doctor in db.Doctors)
            {
                doctors.Add(doctor);
            }
            lstDoctorList.Items.Refresh();

        }

        // method to clear textbox fields
        private void ClearDoctorDetails()
        {
            tbxFirstName.Text = "";
            tbxLastName.Text = "";
            tbxAddress.Text = "";
            tbxEmail.Text = "";
            tbxPhone.Text = "";
            tbxOnDuty.Text = "";
            tbxUserID.Text = "";
        }
        // method to run when doctor listing is selected
        private void lstDoctorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // run check to validate a listing is chosen
            if (lstDoctorList.SelectedIndex > 0)
            {
                // set dcotor from element chosen
                selectedDoctor = doctors.ElementAt(lstDoctorList.SelectedIndex);
                // enable submenu options
                submenuModifySelectedDoctor.IsEnabled = true;
                submenuDeleteSelectedDoctor.IsEnabled = true;

                // clear list when DB method is add
                if (dBOperation == DBOperation.ADD)
                {
                    ClearDoctorDetails();
                }

                // set text fields
                tbxFirstName.Text = selectedDoctor.FirstName;
                tbxLastName.Text = selectedDoctor.LastName;
                tbxAddress.Text = selectedDoctor.Address;
                tbxEmail.Text = selectedDoctor.Email;
                tbxPhone.Text = selectedDoctor.PhoneNo.ToString();
                tbxOnDuty.Text = selectedDoctor.OnDuty.ToString();
                tbxUserID.Text = selectedDoctor.UserID.ToString();



            }
        }
        // method to modify doctor record
        private void submenuModifySelectedDoctor_Click(object sender, RoutedEventArgs e)
        {
            stkDoctorDetails.Visibility = Visibility.Visible;
            dBOperation = DBOperation.MODIFY;
        }
        // emthod to delete a doctor record
        private void submenuDeleteSelectedDoctor_Click(object sender, RoutedEventArgs e)
        {
            // run ID check wwith where clause
            db.Doctors.RemoveRange(db.Doctors.Where(t => t.UserID == selectedDoctor.UserID));
            int saveSuccess = db.SaveChanges();
            if (saveSuccess == 1)
            {
                // print success message
                MessageBox.Show("Doctor deleted successfully!", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshDoctorList();
                ClearDoctorDetails();
                stkDoctorDetails.Visibility = Visibility.Visible;
            }
            else
            {  // print error message
                MessageBox.Show("Error deleting doctor record.", "Delete doctor from Database", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        // method to run when ok button clicked to save or modify doctor record
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (dBOperation == DBOperation.ADD)
            {
                // set doctor data
                DBLibrary.Doctor doctor = new DBLibrary.Doctor();
                doctor.FirstName = tbxFirstName.Text.Trim();
                doctor.LastName = tbxLastName.Text.Trim();
                doctor.Address = tbxAddress.Text.Trim();
                doctor.Email = tbxEmail.Text.Trim();
                doctor.PhoneNo = Int32.Parse(tbxPhone.Text.Trim());
                doctor.OnDuty = Convert.ToBoolean(tbxOnDuty.Text.Trim());
                doctor.UserID = Int32.Parse(tbxUserID.Text.Trim());
                // save success as int
                int saveSuccess = SaveDoctor(doctor);

                if (saveSuccess == 1)
                {
                    // print success message on save
                    MessageBox.Show($"Doctor {doctor.FirstName} {doctor.LastName} has been added to the database!", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshDoctorList();
                    ClearDoctorDetails();
                    stkDoctorDetails.Visibility = Visibility.Collapsed;
                }
                else
                {
                    // print error message
                    MessageBox.Show("Error saving doctor record.", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            // run if method enum is set to MODIFY
            if (dBOperation == DBOperation.MODIFY)
            {
                foreach (var doctor in db.Doctors.Where(t => t.UserID == selectedDoctor.UserID))
                {
                    doctor.FirstName = tbxFirstName.Text.Trim();
                    doctor.LastName = tbxLastName.Text.Trim();
                    doctor.Address = tbxAddress.Text.Trim();
                    doctor.Email = tbxEmail.Text.Trim();
                    doctor.PhoneNo = Int32.Parse(tbxPhone.Text.Trim());
                    doctor.OnDuty = Convert.ToBoolean(tbxOnDuty.Text.Trim());
                    doctor.UserID = Int32.Parse(tbxUserID.Text.Trim());

                }
                // save success as int
                int save = db.SaveChanges();
                if (save == 1)
                {
                    // print success
                    MessageBox.Show("Doctor modified successfully!", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshDoctorList(); // refresh dcotor list
                    ClearDoctorDetails(); // clear doctor details
                    stkDoctorDetails.Visibility = Visibility.Collapsed; // collapse stackpanel
                }
            }

        }
        // method to save doctor
        public int SaveDoctor(DBLibrary.Doctor doctor)
        {
            db.Entry(doctor).State = System.Data.Entity.EntityState.Added;
            int saveSuccess = db.SaveChanges();
            return saveSuccess;
        }
        // hide stackpanel on cancel button click
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            stkDoctorDetails.Visibility = Visibility.Collapsed;
        }

    }

    
}
