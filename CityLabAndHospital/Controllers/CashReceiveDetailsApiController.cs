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
    [Route("api/CashReceiveDetailsApi")]
    public class CashReceiveDetailsApiController : Controller
    {
        private readonly citylabDBContext _context;

        public CashReceiveDetailsApiController(citylabDBContext context)
        {
            _context = context;
        }

        // GET: api/CashReceiveDetailsApi
        [HttpGet]
        public IEnumerable<CashReceiveDetails> GetCashReceiveDetails()
        {
            return _context.CashReceiveDetails;
        }




        [HttpGet("GetVouchers")]
        public List<string> GetVouchers()
        {
            return (from a in _context.CashReceiveDetails orderby a.VoucherNo ascending select a.VoucherNo).ToList();
        }


        [HttpGet("CreateVoucher")]
        public Int16 CreateVoucher()
        {
            Int16 a1 = 0;
            a1 = (from a in _context.CashReceiveDetails select Int16.Parse(a.VoucherNo)).DefaultIfEmpty().Max();
            // a1 = int.Parse(_context.InvestigationMaster.Select(p => int.Parse(p.VoucherNo)).DefaultIfEmpty().Max());
            a1++;
            return a1;
        }


        // GET: api/CashReceiveDetailsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCashReceiveDetails([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cashReceiveDetails = await _context.CashReceiveDetails.SingleOrDefaultAsync(m => m.VoucherNo == id);

            if (cashReceiveDetails == null)
            {
                return NotFound();
            }

            return Ok(cashReceiveDetails);
        }

        // PUT: api/CashReceiveDetailsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCashReceiveDetails([FromRoute] string id, [FromBody] CashReceiveDetails cashReceiveDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cashReceiveDetails.VoucherNo)
            {
                return BadRequest();
            }

            _context.Entry(cashReceiveDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CashReceiveDetailsExists(id))
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

        // POST: api/CashReceiveDetailsApi
        [HttpPost]
        public async Task<IActionResult> PostCashReceiveDetails([FromBody] CashReceiveDetails cashReceiveDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CashReceiveDetails.Add(cashReceiveDetails);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CashReceiveDetailsExists(cashReceiveDetails.VoucherNo))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCashReceiveDetails", new { id = cashReceiveDetails.VoucherNo }, cashReceiveDetails);
        }

        // DELETE: api/CashReceiveDetailsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCashReceiveDetails([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cashReceiveDetails = await _context.CashReceiveDetails.SingleOrDefaultAsync(m => m.VoucherNo == id);
            if (cashReceiveDetails == null)
            {
                return NotFound();
            }

            _context.CashReceiveDetails.Remove(cashReceiveDetails);
            await _context.SaveChangesAsync();

            return Ok(cashReceiveDetails);
        }

        private bool CashReceiveDetailsExists(string id)
        {
            return _context.CashReceiveDetails.Any(e => e.VoucherNo == id);
        }
    }
}