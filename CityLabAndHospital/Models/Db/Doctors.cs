using System;
using System.Collections.Generic;

namespace CityLabAndHospital.Models.Db
{
    public partial class Doctors
    {
        public Doctors()
        {
            InvestigationMaster = new HashSet<InvestigationMaster>();
        }

        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Qualification { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }

        public ICollection<InvestigationMaster> InvestigationMaster { get; set; }
    }
}
