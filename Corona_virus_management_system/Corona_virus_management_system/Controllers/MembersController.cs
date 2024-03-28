using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Corona_virus_management_system.Data;
using Corona_virus_management_system.Models;
using System.Reflection.PortableExecutable;

namespace Corona_virus_management_system.Controllers
{
    // The MembersController class controls the behavior of views related to the Member model.
    public class MembersController : Controller
    {
        private readonly Corona_virus_management_systemContext _context;

        // Constructor injection: The controller receives an instance of the context via dependency injection.
        public MembersController(Corona_virus_management_systemContext context)
        {
            _context = context;
        }

        // GET: Members
        // Displays a list of all members in the database.
        public async Task<IActionResult> Index()
        {
            // Check if the Member entity set is null, and handle the situation appropriately.
            return _context.Member != null ? 
                          View(await _context.Member.ToListAsync()) :
                          Problem("Entity set 'Corona_virus_management_systemContext.Member'  is null.");
        }

        // GET: Members/Details/5
        // Displays detailed information about a specific member.
        public async Task<IActionResult> Details(int? id)
        {
            // Check if the id is null or the Member entity set is null.
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            // Find the member with the specified id.
            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.ID == id);
            if (member == null)
            {
                return NotFound();
            }

            // Find all vaccines associated with the member.
            var vaccines = await _context.Vaccine
                .Where(v => v.MemberId == id)
                .ToListAsync();

            // Pass the list of vaccines to the view using ViewBag.
            ViewBag.Vaccines = vaccines;

            return View(member);
        }

        // GET: Members/Create
        // Displays a form to create a new member.
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Handles the creation of a new member.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Image,Address,City,HouseNumber,BearthDate,Phone,CellPhone,PositiveResult,RecoveryDate")] Member member)
        {
            // Check if the model state is valid.
            if (ModelState.IsValid)
            {
                // Validate positive result and recovery dates.
                if (member.PositiveResult > member.RecoveryDate)
                {
                    ModelState.AddModelError(string.Empty, "This date is not valid. You cannot recover until you test positive");
                    return View(member);
                }
                // Add the new member to the context and save changes.
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Edit/5
        // Displays a form to edit an existing member.
        public async Task<IActionResult> Edit(int? id)
        {
            // Check if the id is null or the Member entity set is null.
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            // Find the member with the specified id.
            var member = await _context.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // Handles the editing of an existing member.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,Image,Address,City,HouseNumber,BearthDate,Phone,CellPhone,PositiveResult,RecoveryDate")] Member member)
        {
            // Check if the id in the URL matches the id in the model.
            if (id != member.ID)
            {
                return NotFound();
            }

            // Check if the model state is valid.
            if (ModelState.IsValid)
            {
                try
                {
                    // Validate positive result and recovery dates.
                    if (member.PositiveResult > member.RecoveryDate)
                    {
                        ModelState.AddModelError(string.Empty, "This date is not valid. You cannot recover until you test positive");
                        return View(member);
                    }
                    // Update the member in the context and save changes.
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.ID))
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
            return View(member);
        }

        // GET: Members/Delete/5
        // Displays a confirmation page for deleting a member.
        public async Task<IActionResult> Delete(int? id)
        {
            // Check if the id is null or the Member entity set is null.
            if (id == null || _context.Member == null)
            {
                return NotFound();
            }

            // Find the member with the specified id.
            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.ID == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        // Handles the deletion of a member.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Check if the Member entity set is null.
            if (_context.Member == null)
            {
                return Problem("Entity set 'Corona_virus_management_systemContext.Member'  is null.");
            }
            // Find the member with the specified id.
            var member = await _context.Member.FindAsync(id);
            // Find all vaccines associated with the member.
            var vaccines = await _context.Vaccine
                .Where(v => v.MemberId == id)
                .ToListAsync();

            // If the member exists, remove the member and associated vaccines from the context and save changes.
            if (member != null)
            {
                foreach (var vaccine in vaccines)
                {
                    _context.Vaccine.Remove(vaccine);
                }
                _context.Member.Remove(member);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Helper method to check if a member exists in the context based on id.
        private bool MemberExists(int id)
        {
          return (_context.Member?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
