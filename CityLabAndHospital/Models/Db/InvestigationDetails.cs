using System;
using System.Collections.Generic;

namespace CityLabAndHospital.Models.Db
{
    public partial class InvestigationDetails
    {
        public string VoucherNo { get; set; }
        public DateTime? Date { get; set; }
        public int? SlNo { get; set; }
        public string TestName { get; set; }
        public string TestGroup { get; set; }
        public string ReportGroup { get; set; }
        public decimal? Fee { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Commision { get; set; }

        public Investigations TestNameNavigation { get; set; }
    }
}
