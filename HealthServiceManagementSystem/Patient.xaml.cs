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
    /// Interaction logic for Patient.xaml
    /// </summary>
    public partial class Patient : Page
    {
        HealthServiceEntities db = new HealthServiceEntities("metadata=res://*/HealthClinicModel.csdl|res://*/HealthClinicModel.ssdl|res://*/HealthClinicModel.msl;provider=System.Data.SqlClient;provider connection string='data source=172.20.10.12;initial catalog=HealthSevice;persist security info=True;user id=paul;password=Venus1234;MultipleActiveResultSets=True;App=EntityFramework'");

        List<DBLibrary.Patient> patients = new List<DBLibrary.Patient>();
        DBLibrary.Patient selectedPatient = new DBLibrary.Patient();

        enum DBOperation
        {
            ADD,
            MODIFY,
            DELETE
        }

        DBOperation dBOperation = new DBOperation();



        public Patient()
        {
            InitializeComponent();
        }

        private void submenuAddNewPatient_Click(object sender, RoutedEventArgs e)
        {
            dBOperation = DBOperation.ADD;
            stkPatientDetails.Visibility = Visibility.Visible;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshPatientList();

        }


        private void RefreshPatientList()
        {
            lstPatientList.ItemsSource = patients;
            patients.Clear();

            foreach (var patient in db.Patients)
            {
                patients.Add(patient);
            }
            lstPatientList.Items.Refresh();

        }

        private void ClearPatientDetails()
        {
            tbxFirstName.Text = "";
            tbxLastName.Text = "";
            tbxAddress.Text = "";
            tbxEmail.Text = "";
            tbxPhone.Text = "";
            tbxDoctorID.Text = "";
        }


        private void lstPatientList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstPatientList.SelectedIndex > 0)
            {
                selectedPatient = patients.ElementAt(lstPatientList.SelectedIndex);

                submenuModifySelectedPatient.IsEnabled = true;
                submenuDeleteSelectedPatient.IsEnabled = true;

                if (dBOperation == DBOperation.ADD)
                {
                    ClearPatientDetails();
                }

                tbxFirstName.Text = selectedPatient.FirstName;
                tbxLastName.Text = selectedPatient.LastName;
                tbxAddress.Text = selectedPatient.Address;
                tbxEmail.Text = selectedPatient.Email;
                tbxPhone.Text = selectedPatient.PhoneNo.ToString();
                tbxDoctorID.Text = selectedPatient.DoctorID.ToString();

            }
        }

        private void submenuModifySelectedPatient_Click(object sender, RoutedEventArgs e)
        {
            dBOperation = DBOperation.MODIFY;
            stkPatientDetails.Visibility = Visibility.Visible;
        }


        private void submenuDeleteSelectedPatient_Click(object sender, RoutedEventArgs e)
        {
            db.Patients.RemoveRange(db.Patients.Where(t => t.PatientID == selectedPatient.PatientID));
            int saveSuccess = db.SaveChanges();
            if (saveSuccess == 1)
            {
                MessageBox.Show("Patient deleted successfully!", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                RefreshPatientList();
                ClearPatientDetails();
                stkPatientDetails.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Error deleting patient record.", "Delete patient from Database", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (dBOperation == DBOperation.ADD)
            {
                DBLibrary.Patient patient = new DBLibrary.Patient();
                patient.FirstName = tbxFirstName.Text.Trim();
                patient.LastName = tbxLastName.Text.Trim();
                patient.Address = tbxAddress.Text.Trim();
                patient.Email = tbxEmail.Text.Trim();
                patient.PhoneNo = Int32.Parse(tbxPhone.Text.Trim());
                patient.DoctorID = Int32.Parse(tbxDoctorID.Text.Trim());

                int saveSuccess = SavePatient(patient);

                if (saveSuccess == 1)
                {
                    MessageBox.Show($"Patient {patient.FirstName} {patient.LastName} has been added to the database!", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshPatientList();
                    ClearPatientDetails();
                    stkPatientDetails.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Error saving patient record.", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            if (dBOperation == DBOperation.MODIFY)
            {
                foreach (var patient in db.Patients.Where(t => t.DoctorID == selectedPatient.DoctorID))
                {
                    patient.FirstName = tbxFirstName.Text.Trim();
                    patient.LastName = tbxLastName.Text.Trim();
                    patient.Address = tbxAddress.Text.Trim();
                    patient.Email = tbxEmail.Text.Trim();
                    patient.PhoneNo = Int32.Parse(tbxPhone.Text.Trim());
                    patient.DoctorID = Int32.Parse(tbxDoctorID.Text.Trim());

                }
                int save = db.SaveChanges();
                if (save == 1)
                {
                    MessageBox.Show("Patient modified successfully!", "Save to Database", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshPatientList();
                    ClearPatientDetails();
                    stkPatientDetails.Visibility = Visibility.Collapsed;
                }
            }

        }

        public int SavePatient(DBLibrary.Patient patient)
        {
            db.Entry(patient).State = System.Data.Entity.EntityState.Added;
            int saveSuccess = db.SaveChanges();
            return saveSuccess;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
