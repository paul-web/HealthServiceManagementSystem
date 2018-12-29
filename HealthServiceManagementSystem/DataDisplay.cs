using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthServiceManagementSystem
{
    // class for storing analytics data for displaying
    class DataDisplay
    {
        // int stores total number of on duty doctors
        public int TotalDoctorsOnDuty { get; set; }
        // int stores total number of on duty nurses
        public int TotalNursesOnDuty { get; set; }
        // int stores total number of users
        public int TotalUsers { get; set; }
    }
}
