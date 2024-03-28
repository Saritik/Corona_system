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
    // The VaccinesController class controls the behavior of views related to the Vaccine model.
    public class VaccinesController : Controller
    {
        private readonly Corona_virus_management_systemContext _context;

        // Constructor injection: The controller receives an instance of the context via dependency injection.
        public VaccinesController(Corona_virus_management_systemContext context)
        {
            _context = context;
        }

        // GET: Vaccines
        // Displays a list of vaccines for a specific member
        public async Task<IActionResult> Index(int memberId)
        {
            var vaccinesForMember = await _context.Vaccine.Where(v => v.MemberId == memberId).ToListAsync();
            ViewBag.MemberId = memberId;
            return View(vaccinesForMember);
        }

        // GET: Vaccines/Details/5
        // Displays details of a specific vaccine
        public async Task<IActionResult> Details(int? id)
        {
            // Check if the id is null or the Vaccine entity set is null.
            if (id == null || _context.Vaccine == null)
            {
                return NotFound();
            }

            // Find the vaccine with the specified id.
            var vaccine = await _context.Vaccine
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vaccine == null)
            {
                return NotFound();
            }

            return View(vaccine);
        }

        // GET: Vaccines/Create
        // Displays the form to create a new vaccine for a member
        public IActionResult Create(int memberId)
        {
            ViewBag.MemberId = memberId;
            return View();
        }

        // POST: Vaccines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Handles the creation of a new vaccine for a member.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Date,Manufacturer,MemberId")] Vaccine vaccine)
        {
            if (ModelState.IsValid)
            {
                // Find the member associated with the vaccine.
                var member = _context.Member.Find(vaccine.MemberId);
                if (member == null)
                {
                    return NotFound();
                }

                // Check the number of vaccines already associated with the member.
                var vaccinesForMember = _context.Vaccine.Where(v => v.MemberId == vaccine.MemberId).ToList();

                if (vaccinesForMember.Count >= 4)
                {
                    ModelState.AddModelError(string.Empty, "Cannot add more than 4 vaccines for each member.");
                    return View(vaccine);
                }

                // Check the date of the new vaccine against the date of the last vaccine for the member.
                var lastVaccine = vaccinesForMember.OrderByDescending(v => v.Date).FirstOrDefault();
                if (lastVaccine != null && lastVaccine.Date > vaccine.Date)
                {
                    ModelState.AddModelError(string.Empty, "This date is not valid because it cannot occur before the vaccination date before it.");
                    return View(vaccine);
                }

                // Add the new vaccine to the context and save changes.
                _context.Add(vaccine);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", "Members", new { id = vaccine.MemberId });
            }
            return View(vaccine);
        }

        // GET: Vaccines/Edit/5
        // Displays the form to edit a specific vaccine
        public async Task<IActionResult> Edit(int memberId, int? id)
        {
            // Check if the id is null or the Vaccine entity set is null.
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
        // Edits a specific vaccine
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
                int myMemberId = vaccine.MemberId;
                try
                {
                    // Find the existing vaccine in the context.
                    var existingVaccine = await _context.Vaccine.FindAsync(id);
                    if (existingVaccine == null)
                    {
                        return NotFound();
                    }

                    // Update the existing vaccine with the new information.
                    existingVaccine.Date = vaccine.Date;
                    existingVaccine.Manufacturer = vaccine.Manufacturer;
                    existingVaccine.MemberId = vaccine.MemberId;

                    // Check the new vaccine date against the dates of other vaccines for the member.
                    var vaccinesForMember = _context.Vaccine.Where(v => v.MemberId == vaccine.MemberId).ToList();

                    foreach(var vac in vaccinesForMember)
                    {
                        if (vac != null && vac.Date < vaccine.Date && vac.ID > vaccine.ID)
                        {
                            ModelState.AddModelError(string.Empty, "This date is not valid because it cannot occur before the vaccination date before it.");
                            return View(existingVaccine);
                        }
                    }
                    _context.Update(existingVaccine);
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
                return RedirectToAction(nameof(Index), new { memberId = myMemberId });
            }
            return View(vaccine);
        }

        // GET: Vaccines/Delete/5
        // Displays the form to delete a specific vaccine
        public async Task<IActionResult> Delete(int? id)
        {
            // Check if the id is null or the Vaccine entity set is null.
            if (id == null || _context.Vaccine == null)
            {
                return NotFound();
            }

            // Find the vaccine with the specified id.
            var vaccine = await _context.Vaccine
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vaccine == null)
            {
                return NotFound();
            }

            return View(vaccine);
        }

        // POST: Vaccines/Delete/5
        // Deletes a specific vaccine
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Check if the Vaccine entity set is null.
            if (_context.Vaccine == null)
            {
                return Problem("Entity set 'Corona_virus_management_systemContext.Vaccine'  is null.");
            }
            // Find the vaccine with the specified id.
            var vaccine = await _context.Vaccine.FindAsync(id);
            int myMemberId = vaccine.MemberId;
            if (vaccine != null)
            {
                _context.Vaccine.Remove(vaccine);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { memberId = myMemberId });
        }

        // Helper method to check if a member exists in the context based on id.
        private bool VaccineExists(int id)
        {
          return (_context.Vaccine?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
