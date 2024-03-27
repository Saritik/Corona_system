using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Corona_virus_management_system.Data;
using Corona_virus_management_system.Models;

namespace Corona_virus_management_system.Controllers
{
    public class VaccinesController : Controller
    {
        private readonly Corona_virus_management_systemContext _context;

        public VaccinesController(Corona_virus_management_systemContext context)
        {
            _context = context;
        }

        // GET: Vaccines
        public async Task<IActionResult> Index(int memberId)
        {
            var vaccinesForMember = await _context.Vaccine.Where(v => v.MemberId == memberId).ToListAsync();
            ViewBag.MemberId = memberId;
            return View(vaccinesForMember);
        }

        // GET: Vaccines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vaccine == null)
            {
                return NotFound();
            }

            var vaccine = await _context.Vaccine
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vaccine == null)
            {
                return NotFound();
            }

            return View(vaccine);
        }

        // GET: Vaccines/Create
        public IActionResult Create(int memberId)
        {
            ViewBag.MemberId = memberId;
            return View();
        }

        // POST: Vaccines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Date,Manufacturer,MemberId")] Vaccine vaccine)
        {
            if (ModelState.IsValid)
            {
                var member = _context.Member.Find(vaccine.MemberId);
                if (member == null)
                {
                    return NotFound();
                }

                var vaccinesForMember = _context.Vaccine.Where(v => v.MemberId == vaccine.MemberId).ToList();

                if (vaccinesForMember.Count >= 4)
                {
                    ModelState.AddModelError(string.Empty, "Cannot add more than 4 vaccines for each member.");
                    return View(vaccine);
                }

                var lastVaccine = vaccinesForMember.OrderByDescending(v => v.Date).FirstOrDefault();
                if (lastVaccine != null && lastVaccine.Date > vaccine.Date)
                {
                    ModelState.AddModelError(string.Empty, "This date is not valid because it cannot occur before the vaccination date before it.");
                    return View(vaccine);
                }

                _context.Add(vaccine);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", "Members", new { id = vaccine.MemberId });
            }
            return View(vaccine);
        }

        // GET: Vaccines/Edit/5
        public async Task<IActionResult> Edit(int memberId, int? id)
        {
            if (id == null || _context.Vaccine == null)
            {
                return NotFound();
            }

            ViewBag.MemberId = memberId;
            var vaccine = await _context.Vaccine.FindAsync(id);
            if (vaccine == null)
            {
                return NotFound();
            }
            return View(vaccine);
        }

        // POST: Vaccines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Date,Manufacturer,MemberId")] Vaccine vaccine)
        {
            if (id != vaccine.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var vaccinesForMember = _context.Vaccine.Where(v => v.MemberId == vaccine.MemberId).ToList();

                    foreach(var vac in vaccinesForMember)
                    {
                        if (vac != null && vac.Date > vaccine.Date)
                        {
                            ModelState.AddModelError(string.Empty, "This date is not valid because it cannot occur before the vaccination date before it.");
                            return View(vaccine);
                        }
                    }
                    _context.Update(vaccine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccineExists(vaccine.ID))
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
            return View(vaccine);
        }

        // GET: Vaccines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vaccine == null)
            {
                return NotFound();
            }

            var vaccine = await _context.Vaccine
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vaccine == null)
            {
                return NotFound();
            }

            return View(vaccine);
        }

        // POST: Vaccines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vaccine == null)
            {
                return Problem("Entity set 'Corona_virus_management_systemContext.Vaccine'  is null.");
            }
            var vaccine = await _context.Vaccine.FindAsync(id);
            if (vaccine != null)
            {
                _context.Vaccine.Remove(vaccine);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaccineExists(int id)
        {
          return (_context.Vaccine?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
