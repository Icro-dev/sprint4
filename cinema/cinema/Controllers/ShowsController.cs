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
            var showList = await _context.Shows.Include(s => s.Movie).ToListAsync();
            showList.Sort((a,b) => DateTime.Compare(a.StartTime,b.StartTime));
            var dateList = new List<DateOnly>();
            var showDict = new Dictionary<DateOnly, List<Show>>();
            var showPerMoviePerDateDict = new Dictionary<DateOnly, Dictionary<Movie, List<Show>>>();
            foreach (Show show in showList)
            {
                var date = DateOnly.FromDateTime(show.StartTime);
                if(!(dateList.Contains(date)))
                {
                    dateList.Add(date);
                }
            }
            dateList.Sort((a, b) => (a.CompareTo(b)));
            foreach (var date in dateList)
            {
                showDict.Add(date,new List<Show>());
            }

            foreach (Show show in showList)
            {
                var date = DateOnly.FromDateTime(show.StartTime);
                showDict[date].Add(show);
            }


            foreach (var date in showDict.Keys)
            {
                var showsPerMovieDict = new Dictionary<Movie, List<Show>>();
                var showsPerDate = showDict[date];
                foreach (var show in showsPerDate)
                {
                    if (!showsPerMovieDict.ContainsKey(show.Movie))
                    {
                        showsPerMovieDict.Add(show.Movie,new List<Show>());
                    }
                    showsPerMovieDict[show.Movie].Add(show);
                }
                showPerMoviePerDateDict[date] = showsPerMovieDict;
            }
            return View(showPerMoviePerDateDict);
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
    }
}
