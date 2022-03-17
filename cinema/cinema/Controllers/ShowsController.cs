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
using cinema.Filters;

namespace cinema.Controllers
{
    public class ShowsController : Controller
    {
        private readonly CinemaContext _context;

        public ShowsController(CinemaContext context)
        {
            _context = context;
        }

        // GET: Shows
        public async Task<IActionResult> Index()
        {
            return View(await _context.Shows.Include(s => s.Movie).ToListAsync());
        }

        // GET: Shows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var show = await _context.Shows
                .FirstOrDefaultAsync(m => m.Id == id);
            if (show == null)
            {
                return NotFound();
            }

            return View(show);
        }

        // GET: Shows/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ThreeD,Room,StartTime,Break")] Show show)
        {
            if (ModelState.IsValid)
            {
                _context.Add(show);
                await _context.SaveChangesAsync();
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(show);
        }

        // GET: Shows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var show = await _context.Shows.FindAsync(id);
            if (show == null)
            {
                return NotFound();
            }
            return View(show);
        }

        // POST: Shows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ThreeD,Room,StartTime,Break")] Show show)
        {
            if (id != show.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(show);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowExists(show.Id))
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
            return View(show);
        }

        // GET: Shows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var show = await _context.Shows
                .FirstOrDefaultAsync(m => m.Id == id);
            if (show == null)
            {
                return NotFound();
            }

            return View(show);
        }

        // POST: Shows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var show = await _context.Shows.FindAsync(id);
            _context.Shows.Remove(show);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShowExists(int id)
        {
            return _context.Shows.Any(e => e.Id == id);
        }

        //GET: Shows/Daily
        public IActionResult Daily()
        {
            return View(_context.Shows.Include(s => s.Movie).Where(s => s.StartTime > DateTime.Now).Where(s => s.StartTime.Date == DateTime.Today.Date).ToList());
        }

        [HttpGet, HttpPost, ActionName("Filter")]
        public IActionResult Filter()
        {
            List<Show> shows = _context.Shows.Include(s => s.Movie).Where(s => s.StartTime > DateTime.Now).ToList();
            ShowsFilter filter = new ShowsFilter(shows);      
            if(Request.Method == HttpMethod.Post.Method && ViewData["ShowsFilter"] != null)
            {
                filter = (ShowsFilter) ViewData["ShowsFilter"];
                filter.input = Request.Form["search"];
                filter.maxlength = Convert.ToDouble(Request.Form["len"]);
                filter.date = Convert.ToDateTime(Request.Form["date"]);
                filter.threed = Convert.ToBoolean(Request.Form["threed"]);
                foreach (string language in filter.languages.Keys)
                    filter.languages[language] = Convert.ToBoolean(Request.Form[language]);
                foreach (string genre in filter.genres.Keys)
                    filter.genres[genre] = Convert.ToBoolean(Request.Form[genre]);
                foreach(string kijkwijzer in filter.kijkwijzers.Keys)
                    filter.kijkwijzers[kijkwijzer] = Convert.ToBoolean(Request.Form[kijkwijzer]);
            }
            shows = filter.Apply(shows);
            ViewData["ShowsFilter"] = filter;
            return View(shows);
        }
    }
}
