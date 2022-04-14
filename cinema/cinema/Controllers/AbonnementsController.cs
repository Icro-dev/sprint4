﻿#nullable disable
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
    public class AbonnementsController : Controller
    {
        private readonly CinemaContext _context;

        public AbonnementsController(CinemaContext context)
        {
            _context = context;
        }

        // GET: Abonnements
        public async Task<IActionResult> Index()
        {
            return View(await _context.Abonnement.ToListAsync());
        }

        // GET: Abonnements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonnement = await _context.Abonnement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (abonnement == null)
            {
                return NotFound();
            }

            return View(abonnement);
        }

        // GET: Abonnements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Abonnements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AbboQR,StartDate,ExpireDate,AbboName,Expired")] Abonnement abonnement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(abonnement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(abonnement);
        }

        // GET: Abonnements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonnement = await _context.Abonnement.FindAsync(id);
            if (abonnement == null)
            {
                return NotFound();
            }
            return View(abonnement);
        }

        // POST: Abonnements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AbboQR,StartDate,ExpireDate,AbboName,Expired")] Abonnement abonnement)
        {
            if (id != abonnement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(abonnement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbonnementExists(abonnement.Id))
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
            return View(abonnement);
        }

        // GET: Abonnements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonnement = await _context.Abonnement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (abonnement == null)
            {
                return NotFound();
            }

            return View(abonnement);
        }

        // POST: Abonnements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var abonnement = await _context.Abonnement.FindAsync(id);
            _context.Abonnement.Remove(abonnement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbonnementExists(int id)
        {
            return _context.Abonnement.Any(e => e.Id == id);
        }
    }
}
