#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cinema.Data;
using cinema.Models;

namespace cinema.Controllers
{
    public class RoomTemplatesController : Controller
    {
        private readonly CinemaContext _context;

        public RoomTemplatesController(CinemaContext context)
        {
            _context = context;
        }

        // GET: RoomTemplates
        public async Task<IActionResult> Index()
        {
            return View(await _context.RoomTemplates.ToListAsync());
        }

        // GET: RoomTemplates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomTemplate = await _context.RoomTemplates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomTemplate == null)
            {
                return NotFound();
            }

            return View(roomTemplate);
        }

        // GET: RoomTemplates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoomTemplates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Setting")] RoomTemplate roomTemplate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomTemplate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roomTemplate);
        }

        // GET: RoomTemplates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomTemplate = await _context.RoomTemplates.FindAsync(id);
            if (roomTemplate == null)
            {
                return NotFound();
            }
            return View(roomTemplate);
        }

        // POST: RoomTemplates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Setting")] RoomTemplate roomTemplate)
        {
            if (id != roomTemplate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomTemplate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomTemplateExists(roomTemplate.Id))
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
            return View(roomTemplate);
        }

        // GET: RoomTemplates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomTemplate = await _context.RoomTemplates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomTemplate == null)
            {
                return NotFound();
            }

            return View(roomTemplate);
        }

        // POST: RoomTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomTemplate = await _context.RoomTemplates.FindAsync(id);
            _context.RoomTemplates.Remove(roomTemplate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomTemplateExists(int id)
        {
            return _context.RoomTemplates.Any(e => e.Id == id);
        }
    }
}
