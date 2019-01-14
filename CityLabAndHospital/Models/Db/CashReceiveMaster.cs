using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityLabAndHospital.Models.Db
{
    public class CashReceiveMaster
    {
        public string VoucherNo { get; set; }
        public DateTime? Date { get; set; }
        public string InvestigationId { get; set; }
        public string PatientName { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}


