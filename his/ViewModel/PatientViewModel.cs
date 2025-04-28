using his.Models;

namespace his.ViewModel
{
    public class PatientViewModel
    {
        public List<Patient> Patients { get; set; }
        public List<Category> Departments { get; set; }
        public List<Category> EmrTypes { get; set; }
    }
}
