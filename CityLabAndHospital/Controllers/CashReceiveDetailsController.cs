using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CityLabAndHospital.Models.Db;

namespace CityLabAndHospital.Controllers
{
    public class CashReceiveDetailsController : Controller
    {
        private readonly citylabDBContext _context;

        public CashReceiveDetailsController(citylabDBContext context)
        {
            _context = context;
        }

        // GET: CashReceiveDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.CashReceiveDetails.ToListAsync());
        }

        // GET: CashReceiveDetails/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashReceiveDetails = await _context.CashReceiveDetails
                .SingleOrDefaultAsync(m => m.VoucherNo == id);
            if (cashReceiveDetails == null)
            {
                return NotFound();
            }

            return View(cashReceiveDetails);
        }

        // GET: CashReceiveDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CashReceiveDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VoucherNo,Date,SlNo,InvestigationId,PatientName,Amount")] CashReceiveDetails cashReceiveDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cashReceiveDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cashReceiveDetails);
        }

        // GET: CashReceiveDetails/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashReceiveDetails = await _context.CashReceiveDetails.SingleOrDefaultAsync(m => m.VoucherNo == id);
            if (cashReceiveDetails == null)
            {
                return NotFound();
            }
            return View(cashReceiveDetails);
        }

        // POST: CashReceiveDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("VoucherNo,Date,SlNo,InvestigationId,PatientName,Amount")] CashReceiveDetails cashReceiveDetails)
        {
            if (id != cashReceiveDetails.VoucherNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cashReceiveDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CashReceiveDetailsExists(cashReceiveDetails.VoucherNo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cashReceiveDetails);
        }

        // GET: CashReceiveDetails/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashReceiveDetails = await _context.CashReceiveDetails
                .SingleOrDefaultAsync(m => m.VoucherNo == id);
            if (cashReceiveDetails == null)
            {
                return NotFound();
            }

            return View(cashReceiveDetails);
        }

        // POST: CashReceiveDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cashReceiveDetails = await _context.CashReceiveDetails.SingleOrDefaultAsync(m => m.VoucherNo == id);
            _context.CashReceiveDetails.Remove(cashReceiveDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CashReceiveDetailsExists(string id)
        {
            return _context.CashReceiveDetails.Any(e => e.VoucherNo == id);
        }
    }
}
