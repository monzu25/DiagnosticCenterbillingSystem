using System;
using System.Collections.Generic;

namespace CityLabAndHospital.Models.Db
{
    public partial class InvestigationMaster
    {
        public string VoucherNo { get; set; }
        public DateTime Date { get; set; }
        public string PatientName { get; set; }
        public string Guardian { get; set; }
        public string Address { get; set; }
        public int? SlNo { get; set; }
        public string Mobile { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public int? DoctorId { get; set; }
        public decimal? Total { get; set; }
        public decimal? Discount { get; set; }
        public decimal? NetAmt { get; set; }
        public decimal? PaidAmt { get; set; }
        public decimal? DueAmt { get; set; }

        public Doctors Doctor { get; set; }
    }
}
