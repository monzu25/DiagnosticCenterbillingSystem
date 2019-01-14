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
    [Route("api/InvestigationMastersApi")]
    public class InvestigationMastersApiController : Controller
    {
        private readonly citylabDBContext _context;

        public InvestigationMastersApiController(citylabDBContext context)
        {
            _context = context;
        }

        // GET: api/InvestigationMasters
        [HttpGet]
        public IEnumerable<InvestigationMaster> GetInvestigationMaster()
        {
            return _context.InvestigationMaster;
        }

        // GET: api/InvestigationMasters/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInvestigationMaster([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var investigationMaster = await _context.InvestigationMaster.SingleOrDefaultAsync(m => m.VoucherNo == id);

            if (investigationMaster == null)
            {
                return NotFound();
            }

            return Ok(investigationMaster);
        }


        [HttpGet("GetVouchers")]
        public List<string> GetVouchers()
        {
            return (from a in _context.InvestigationMaster orderby a.VoucherNo ascending select a.VoucherNo).ToList();
        }
        [HttpGet("CreateVoucher")]
        public Int16 CreateVoucher()
        {
            Int16 a1 = 0;
            a1 = (from a in _context.InvestigationMaster select Int16.Parse(a.VoucherNo)).DefaultIfEmpty().Max();
            // a1 = int.Parse(_context.InvestigationMaster.Select(p => int.Parse(p.VoucherNo)).DefaultIfEmpty().Max());
            a1++;
            return a1;
        }

        [HttpGet("GetInvestigationByDate")]
        public IEnumerable<InvestigationMaster> GetInvestigationByDate(DateTime d1, DateTime d2)
        {
            return (from a in _context.InvestigationMaster where a.Date >= d1 && a.Date <= d2 orderby a.Date, a.VoucherNo, a.SlNo select a);
        }


        [HttpGet("GetVoucherNo")]
        public List<string> GetVoucherNo()
        {
            return (from a in _context.InvestigationMaster orderby a.VoucherNo ascending select a.VoucherNo).ToList();
        }









        // PUT: api/InvestigationMasters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvestigationMaster([FromRoute] string id, [FromBody] InvestigationMaster investigationMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != investigationMaster.VoucherNo)
            {
                return BadRequest();
            }

            _context.Entry(investigationMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvestigationMasterExists(id))
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

        // POST: api/InvestigationMasters
        [HttpPost]
        public async Task<IActionResult> PostInvestigationMaster([FromBody] InvestigationMaster investigationMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.InvestigationMaster.Add(investigationMaster);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InvestigationMasterExists(investigationMaster.VoucherNo))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInvestigationMaster", new { id = investigationMaster.VoucherNo }, investigationMaster);
        }

        // DELETE: api/InvestigationMasters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestigationMaster([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var investigationMaster = await _context.InvestigationMaster.SingleOrDefaultAsync(m => m.VoucherNo == id);
            if (investigationMaster == null)
            {
                return NotFound();
            }

            _context.InvestigationMaster.Remove(investigationMaster);
            await _context.SaveChangesAsync();

            return Ok(investigationMaster);
        }

        private bool InvestigationMasterExists(string id)
        {
            return _context.InvestigationMaster.Any(e => e.VoucherNo == id);
        }
    }
}