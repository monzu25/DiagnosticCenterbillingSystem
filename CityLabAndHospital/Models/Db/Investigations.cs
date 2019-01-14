using System;
using System.Collections.Generic;

namespace CityLabAndHospital.Models.Db
{
    public partial class Investigations
    {
        public Investigations()
        {
            InvestigationDetails = new HashSet<InvestigationDetails>();
        }

        public int SlNo { get; set; }
        public string TestName { get; set; }
        public string TestGroup { get; set; }
        public string ReportGroup { get; set; }
        public decimal? Fee { get; set; }

        public ICollection<InvestigationDetails> InvestigationDetails { get; set; }
    }
}
