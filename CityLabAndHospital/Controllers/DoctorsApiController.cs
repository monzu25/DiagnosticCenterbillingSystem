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
    [Route("api/DoctorsApi")]
    public class DoctorsApiController : Controller
    {
        private readonly citylabDBContext _context;

        public DoctorsApiController(citylabDBContext context)
        {
            _context = context;
        }

        // GET: api/DoctorsApi
        [HttpGet]
        public IEnumerable<Doctors> GetDoctors()
        {
            return _context.Doctors;
        }
        [HttpGet("GetDoctorsIds")]
        public List<int> GetDoctorsIds()
        {
            return (from a in _context.Doctors orderby a.DoctorId ascending select a.DoctorId).ToList();
        }
        // GET: api/DoctorsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctors([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctors = await _context.Doctors.SingleOrDefaultAsync(m => m.DoctorId == id);

            if (doctors == null)
            {
                return NotFound();
            }

            return Ok(doctors);
        }

        // PUT: api/DoctorsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctors([FromRoute] int id, [FromBody] Doctors doctors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != doctors.DoctorId)
            {
                return BadRequest();
            }

            _context.Entry(doctors).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorsExists(id))
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

        // POST: api/DoctorsApi
        [HttpPost]
        public async Task<IActionResult> PostDoctors([FromBody] Doctors doctors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Doctors.Add(doctors);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDoctors", new { id = doctors.DoctorId }, doctors);
        }

        // DELETE: api/DoctorsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctors([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctors = await _context.Doctors.SingleOrDefaultAsync(m => m.DoctorId == id);
            if (doctors == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctors);
            await _context.SaveChangesAsync();

            return Ok(doctors);
        }

        private bool DoctorsExists(int id)
        {
            return _context.Doctors.Any(e => e.DoctorId == id);
        }
    }
}