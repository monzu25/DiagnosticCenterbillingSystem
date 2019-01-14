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
    [Route("api/CashReceiveMastersApi")]
    public class CashReceiveMastersApiController : Controller
    {
        private readonly citylabDBContext _context;

        public CashReceiveMastersApiController(citylabDBContext context)
        {
            _context = context;
        }

        // GET: api/CashReceiveMastersApi
        [HttpGet]
        public IEnumerable<CashReceiveMaster> GetCashReceiveMaster()
        {
            return _context.CashReceiveMaster;
        }

        // GET: api/CashReceiveMastersApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCashReceiveMaster([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cashReceiveMaster = await _context.CashReceiveMaster.SingleOrDefaultAsync(m => m.VoucherNo == id);

            if (cashReceiveMaster == null)
            {
                return NotFound();
            }

            return Ok(cashReceiveMaster);
        }






        // PUT: api/CashReceiveMastersApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCashReceiveMaster([FromRoute] string id, [FromBody] CashReceiveMaster cashReceiveMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cashReceiveMaster.VoucherNo)
            {
                return BadRequest();
            }

            _context.Entry(cashReceiveMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CashReceiveMasterExists(id))
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

        // POST: api/CashReceiveMastersApi
        [HttpPost]
        public async Task<IActionResult> PostCashReceiveMaster([FromBody] CashReceiveMaster cashReceiveMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CashReceiveMaster.Add(cashReceiveMaster);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CashReceiveMasterExists(cashReceiveMaster.VoucherNo))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCashReceiveMaster", new { id = cashReceiveMaster.VoucherNo }, cashReceiveMaster);
        }

        // DELETE: api/CashReceiveMastersApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCashReceiveMaster([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cashReceiveMaster = await _context.CashReceiveMaster.SingleOrDefaultAsync(m => m.VoucherNo == id);
            if (cashReceiveMaster == null)
            {
                return NotFound();
            }

            _context.CashReceiveMaster.Remove(cashReceiveMaster);
            await _context.SaveChangesAsync();

            return Ok(cashReceiveMaster);
        }

        private bool CashReceiveMasterExists(string id)
        {
            return _context.CashReceiveMaster.Any(e => e.VoucherNo == id);
        }
    }
}