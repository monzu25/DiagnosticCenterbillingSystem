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
    public class InvestigationDetailsController : Controller
    {
        private readonly citylabDBContext _context;

        public InvestigationDetailsController(citylabDBContext context)
        {
            _context = context;
        }

        // GET: InvestigationDetails1
        public async Task<IActionResult> Index()
        {
            var citylabDBContext = _context.InvestigationDetails.Include(i => i.TestNameNavigation);
            return View(await citylabDBContext.ToListAsync());
        }

        // GET: InvestigationDetails1/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investigationDetails = await _context.InvestigationDetails
                .Include(i => i.TestNameNavigation)
                .SingleOrDefaultAsync(m => m.VoucherNo == id);
            if (investigationDetails == null)
            {
                return NotFound();
            }

            return View(investigationDetails);
        }

        // GET: InvestigationDetails1/Create
        public IActionResult Create()
        {
            ViewData["TestName"] = new SelectList(_context.Investigations, "TestName", "TestName");
            return View();
        }
        [HttpGet("GetInvestigationByDate")]
        public IEnumerable<InvestigationDetails> GetInvestigationByDate(DateTime d1, DateTime d2)
        {
            return (from a in _context.InvestigationDetails where a.Date >= d1 && a.Date <= d2 orderby a.Date, a.VoucherNo, a.SlNo select a);
        }

     
        // POST: InvestigationDetails1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VoucherNo,Date,SlNo,TestName,TestGroup,ReportGroup,Fee,Amount,Commision")] InvestigationDetails investigationDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(investigationDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TestName"] = new SelectList(_context.Investigations, "TestName", "TestName", investigationDetails.TestName);
            return View(investigationDetails);
        }

        // GET: InvestigationDetails1/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investigationDetails = await _context.InvestigationDetails.SingleOrDefaultAsync(m => m.VoucherNo == id);
            if (investigationDetails == null)
            {
                return NotFound();
            }
            ViewData["TestName"] = new SelectList(_context.Investigations, "TestName", "TestName", investigationDetails.TestName);
            return View(investigationDetails);
        }

        // POST: InvestigationDetails1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("VoucherNo,Date,SlNo,TestName,TestGroup,ReportGroup,Fee,Amount,Commision")] InvestigationDetails investigationDetails)
        {
            if (id != investigationDetails.VoucherNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(investigationDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvestigationDetailsExists(investigationDetails.VoucherNo))
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
            ViewData["TestName"] = new SelectList(_context.Investigations, "TestName", "TestName", investigationDetails.TestName);
            return View(investigationDetails);
        }

        // GET: InvestigationDetails1/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investigationDetails = await _context.InvestigationDetails
                .Include(i => i.TestNameNavigation)
                .SingleOrDefaultAsync(m => m.VoucherNo == id);
            if (investigationDetails == null)
            {
                return NotFound();
            }

            return View(investigationDetails);
        }

        // POST: InvestigationDetails1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var investigationDetails = await _context.InvestigationDetails.SingleOrDefaultAsync(m => m.VoucherNo == id);
            _context.InvestigationDetails.Remove(investigationDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvestigationDetailsExists(string id)
        {
            return _context.InvestigationDetails.Any(e => e.VoucherNo == id);
        }
    }
}
