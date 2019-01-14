using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CityLabAndHospital.Controllers
{
    public class ReportViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Report()
        {
            return View();
        }
        public IActionResult DailyReport()
        {
            return View();
        }

        public IActionResult DailyReportSummury()
        {
            return View();
        }
    }
}