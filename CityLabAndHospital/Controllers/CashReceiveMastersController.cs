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
    public class CashReceiveMastersController : Controller
    {
        private readonly citylabDBContext _context;

        public CashReceiveMastersController(citylabDBContext context)
        {
            _context = context;
        }

        // GET: CashReceiveMasters
        public async Task<IActionResult> Index()
        {
            return View(await _context.CashReceiveMaster.ToListAsync());
        }

        // GET: CashReceiveMasters/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashReceiveMaster = await _context.CashReceiveMaster
                .SingleOrDefaultAsync(m => m.VoucherNo == id);
            if (cashReceiveMaster == null)
            {
                return NotFound();
            }

            return View(cashReceiveMaster);
        }

        // GET: CashReceiveMasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CashReceiveMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VoucherNo,Date,InvestigationId,PatientName,TotalAmount")] CashReceiveMaster cashReceiveMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cashReceiveMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cashReceiveMaster);
        }

        // GET: CashReceiveMasters/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashReceiveMaster = await _context.CashReceiveMaster.SingleOrDefaultAsync(m => m.VoucherNo == id);
            if (cashReceiveMaster == null)
            {
                return NotFound();
            }
            return View(cashReceiveMaster);
        }

        // POST: CashReceiveMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("VoucherNo,Date,InvestigationId,PatientName,TotalAmount")] CashReceiveMaster cashReceiveMaster)
        {
            if (id != cashReceiveMaster.VoucherNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cashReceiveMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CashReceiveMasterExists(cashReceiveMaster.VoucherNo))
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
            return View(cashReceiveMaster);
        }

        // GET: CashReceiveMasters/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cashReceiveMaster = await _context.CashReceiveMaster
                .SingleOrDefaultAsync(m => m.VoucherNo == id);
            if (cashReceiveMaster == null)
            {
                return NotFound();
            }

            return View(cashReceiveMaster);
        }

        // POST: CashReceiveMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cashReceiveMaster = await _context.CashReceiveMaster.SingleOrDefaultAsync(m => m.VoucherNo == id);
            _context.CashReceiveMaster.Remove(cashReceiveMaster);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CashReceiveMasterExists(string id)
        {
            return _context.CashReceiveMaster.Any(e => e.VoucherNo == id);
        }
    }
}
