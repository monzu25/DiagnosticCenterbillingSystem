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
    public class InvestigationsController : Controller
    {
        private readonly citylabDBContext _context;

        public InvestigationsController(citylabDBContext context)
        {
            _context = context;
        }

        // GET: Investigations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Investigations.ToListAsync());
        }

        // GET: Investigations/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investigations = await _context.Investigations
                .SingleOrDefaultAsync(m => m.TestName == id);
            if (investigations == null)
            {
                return NotFound();
            }

            return View(investigations);
        }

        // GET: Investigations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Investigations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SlNo,TestName,TestGroup,ReportGroup,Fee")] Investigations investigations)
        {
            if (ModelState.IsValid)
            {
                _context.Add(investigations);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(investigations);
        }

        // GET: Investigations/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investigations = await _context.Investigations.SingleOrDefaultAsync(m => m.TestName == id);
            if (investigations == null)
            {
                return NotFound();
            }
            return View(investigations);
        }

        // POST: Investigations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("SlNo,TestName,TestGroup,ReportGroup,Fee")] Investigations investigations)
        {
            if (id != investigations.TestName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(investigations);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvestigationsExists(investigations.TestName))
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
            return View(investigations);
        }

        // GET: Investigations/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investigations = await _context.Investigations
                .SingleOrDefaultAsync(m => m.TestName == id);
            if (investigations == null)
            {
                return NotFound();
            }

            return View(investigations);
        }

        // POST: Investigations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var investigations = await _context.Investigations.SingleOrDefaultAsync(m => m.TestName == id);
            _context.Investigations.Remove(investigations);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvestigationsExists(string id)
        {
            return _context.Investigations.Any(e => e.TestName == id);
        }
    }
}
