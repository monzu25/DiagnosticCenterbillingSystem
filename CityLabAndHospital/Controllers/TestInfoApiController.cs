using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityLabAndHospital.Models;
using CityLabAndHospital.Models.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityLabAndHospital.Controllers
{
    [Produces("application/json")]
    [Route("api/TestInfoApi")]
    public class TestInfoApiController : Controller
    {
        private citylabDBContext ctx = new citylabDBContext();

        [HttpGet]
        public IEnumerable<TestInfo> GetAlltest()
        {
            IEnumerable<TestInfo> allTest = from tst in ctx.Investigations
                                              select new TestInfo
                                              {
                                                  SlNo = tst.SlNo,
                                                  TestName = tst.TestName,
                                                  TestGroup = tst.TestGroup,
                                                  ReportGroup=tst.ReportGroup,
                                                  Fee=tst.Fee
                                              };
            return allTest;
        }
        [HttpGet("{id}", Name = "TestInfo")]
        public IActionResult Get(string id)
        {
            TestInfo test = (from tst in ctx.Investigations
                                  where id == tst.TestName
                                  select new TestInfo
                                  {
                                      SlNo = tst.SlNo,
                                      TestName = tst.TestName,
                                      TestGroup = tst.TestGroup,
                                      ReportGroup = tst.ReportGroup,
                                      Fee = tst.Fee
                                  }).First();
            return Ok(test);
        }

        [HttpGet("TestNameById")]
        public IEnumerable<Investigations> GetInvestigationByVrNo(string testName)
        {
            return (from a in ctx.Investigations where a.TestName == testName orderby a.TestName select a);
        }


    }
}