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
    public class InvestigationMastersController : Controller
    {
        private readonly citylabDBContext _context;

        public InvestigationMastersController(citylabDBContext context)
        {
            _context = context;
        }
        public List<string> GetVouchers()
        {
            return (from a in _context.InvestigationMaster orderby a.VoucherNo  ascending select a.VoucherNo).ToList();
        }
        // GET: InvestigationMasters1
        public async Task<IActionResult> Index()
        {
            var citylabDBContext = _context.InvestigationMaster.Include(i => i.Doctor);
            return View(await citylabDBContext.ToListAsync());
        }

        // GET: InvestigationMasters1/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investigationMaster = await _context.InvestigationMaster
                .Include(i => i.Doctor)
                .SingleOrDefaultAsync(m => m.VoucherNo == id);
            if (investigationMaster == null)
            {
                return NotFound();
            }

            return View(investigationMaster);
        }

        // GET: InvestigationMasters1/Create
        public IActionResult Create()
        {
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorName");
            return View();
        }

        // POST: InvestigationMasters1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VoucherNo,Date,PatientName,Guardian,Address,SlNo,Mobile,BirthDate,Gender,DoctorId,Total,Discount,NetAmt,PaidAmt,DueAmt")] InvestigationMaster investigationMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(investigationMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorName", investigationMaster.DoctorId);
            return View(investigationMaster);
        }

        // GET: InvestigationMasters1/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investigationMaster = await _context.InvestigationMaster.SingleOrDefaultAsync(m => m.VoucherNo == id);
            if (investigationMaster == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorName", investigationMaster.DoctorId);
            return View(investigationMaster);
        }

        // POST: InvestigationMasters1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("VoucherNo,Date,PatientName,Guardian,Address,SlNo,Mobile,BirthDate,Gender,DoctorId,Total,Discount,NetAmt,PaidAmt,DueAmt")] InvestigationMaster investigationMaster)
        {
            if (id != investigationMaster.VoucherNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(investigationMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvestigationMasterExists(investigationMaster.VoucherNo))
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
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "DoctorName", investigationMaster.DoctorId);
            return View(investigationMaster);
        }

        // GET: InvestigationMasters1/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investigationMaster = await _context.InvestigationMaster
                .Include(i => i.Doctor)
                .SingleOrDefaultAsync(m => m.VoucherNo == id);
            if (investigationMaster == null)
            {
                return NotFound();
            }

            return View(investigationMaster);
        }

        // POST: InvestigationMasters1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var investigationMaster = await _context.InvestigationMaster.SingleOrDefaultAsync(m => m.VoucherNo == id);
            _context.InvestigationMaster.Remove(investigationMaster);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvestigationMasterExists(string id)
        {
            return _context.InvestigationMaster.Any(e => e.VoucherNo == id);
        }
    }
}
