using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CityLabAndHospital.Models.Db;

namespace CityLabAndHospital.Controllers
{
    [Produces("application/json")]
    [Route("api/InvestigationDetailsApi")]
    public class InvestigationDetailsApiController : Controller
    {
        private readonly citylabDBContext _context;

        public InvestigationDetailsApiController(citylabDBContext context)
        {
            _context = context;
        }

        // GET: api/InvestigationDetails
        [HttpGet]
        public IEnumerable<InvestigationDetails> GetInvestigationDetails()
        {
            return _context.InvestigationDetails;
        }




        [HttpGet("GetInvestigationByVrNo")]
        public IEnumerable<InvestigationDetails> GetInvestigationByVrNo(string voucherno)
        {
            return (from a in _context.InvestigationDetails where a.VoucherNo == voucherno orderby a.SlNo select a);
        }


        //Get data using date criteria//

        [HttpGet("GetInvestigationByDate")]
        public IEnumerable<InvestigationDetails> GetInvestigationByDate(DateTime d1, DateTime d2)
        {
            return (from a in _context.InvestigationDetails where a.Date >= d1 && a.Date <= d2 orderby a.Date,a.VoucherNo,a.SlNo select a);
        }




        // GET: api/InvestigationDetails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvestigationDetails([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var investigationDetails = await _context.InvestigationDetails.SingleOrDefaultAsync(m => m.VoucherNo == id);

            if (investigationDetails == null)
            {
                return NotFound();
            }

            return Ok(investigationDetails);
        }

        // PUT: api/InvestigationDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvestigationDetails([FromRoute] string id, [FromBody] InvestigationDetails investigationDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != investigationDetails.VoucherNo)
            {
                return BadRequest();
            }

            _context.Entry(investigationDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvestigationDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/InvestigationDetails
        [HttpPost]
        public async Task<IActionResult> PostInvestigationDetails([FromBody] InvestigationDetails investigationDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.InvestigationDetails.Add(investigationDetails);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InvestigationDetailsExists(investigationDetails.VoucherNo))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInvestigationDetails", new { id = investigationDetails.VoucherNo }, investigationDetails);
        }

        // DELETE: api/InvestigationDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestigationDetails([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var investigationDetails = await _context.InvestigationDetails.FindAsync(id);
            List<InvestigationDetails> ind = (from iv in _context.InvestigationDetails where iv.VoucherNo == id select iv).ToList();

            if (ind == null)
            {
                return NotFound();
            }

            _context.InvestigationDetails.RemoveRange(ind);
            await _context.SaveChangesAsync();

            return Ok(ind);
        }

        private bool InvestigationDetailsExists(string id)
        {
            return _context.InvestigationDetails.Any(e => e.VoucherNo == id);
        }
    }
}