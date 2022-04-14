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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace cinema.Controllers
{ 
[Authorize(Roles = "Kassamedewerker")]
public class LostAndFoundsController : Controller
    {
        private readonly CinemaContext _context;

        public LostAndFoundsController(CinemaContext context)
        {
            _context = context;
        }

        // GET: LostAndFounds
        public async Task<IActionResult> Index()
        {
            var LaFList = new List<LostAndFound>();
            var list = await _context.LostAndFound.ToListAsync();
            LaFList.AddRange(list);

            foreach(var laf in LaFList)
            {
                var lafTime = laf.FoundTime;
                var lafTimeExceeded = laf.FoundTime.AddMonths(5);
                var currentTime = DateTime.Now;
                if(lafTimeExceeded < currentTime)
                {
                    ViewBag.LostObject = laf.LostObject;
                }
            }

            return View(await _context.LostAndFound.ToListAsync());
        }

        // GET: LostAndFounds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lostAndFound = await _context.LostAndFound
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lostAndFound == null)
            {
                return NotFound();
            }

            return View(lostAndFound);
        }

        // GET: LostAndFounds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LostAndFounds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LostObject,Description,FoundTime,RetrievedTime")] LostAndFound lostAndFound)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lostAndFound);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lostAndFound);
        }

        // GET: LostAndFounds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lostAndFound = await _context.LostAndFound.FindAsync(id);
            if (lostAndFound == null)
            {
                return NotFound();
            }
            return View(lostAndFound);
        }

        // POST: LostAndFounds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LostObject,Description,FoundTime,RetrievedTime")] LostAndFound lostAndFound)
        {
            if (id != lostAndFound.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lostAndFound);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LostAndFoundExists(lostAndFound.Id))
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
            return View(lostAndFound);
        }

        // GET: LostAndFounds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lostAndFound = await _context.LostAndFound
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lostAndFound == null)
            {
                return NotFound();
            }

            return View(lostAndFound);
        }

        // POST: LostAndFounds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lostAndFound = await _context.LostAndFound.FindAsync(id);
            _context.LostAndFound.Remove(lostAndFound);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LostAndFoundExists(int id)
        {
            return _context.LostAndFound.Any(e => e.Id == id);
        }
    }
}
