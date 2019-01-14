using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityLabAndHospital.Models.Db
{
    public class CashReceiveDetails
    {
        public string VoucherNo { get; set; }
        public DateTime? Date { get; set; }
        public int SlNo { get; set; }
        public string InvestigationId { get; set; }
        public string PatientName { get; set; }
        public decimal? Amount { get; set; }
    }
}


