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
using cinema.Services;
using cinema.Filters;

namespace cinema.Controllers
{
    public class ShowsController : Controller
    {
        private readonly CinemaContext _context;
        private readonly ShowService _showService;

        public ShowsController(CinemaContext context, IShowService service)
        {
            
            _context = context;
            _showService = (ShowService) service;
        }

        // GET: Shows
        public async Task<IActionResult> Index()
        {
            
            var showList =  _context.Shows.Include(s => s.Movie).ToList();
            var showPerMoviePerDateDict = _showService.GetShowsPerMoviePerDay(showList);

            ShowsFilter filter = new ShowsFilter(showList);
            ViewData["ShowsFilter"] = filter;

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

        [HttpGet, HttpPost, ActionName("Filter")]
        public IActionResult Filter()
        {
            List<Show> shows = _context.Shows.Include(s => s.Movie).Where(s => s.StartTime > DateTime.Now).ToList();
            ShowsFilter filter = new ShowsFilter(shows);      
            if(Request.Method == HttpMethod.Post.Method)
            {
                if(Request.Form["search"] != "")
                    filter.input = Request.Form["search"];
                if (Request.Form["len"] != "")
                    filter.maxlength = Convert.ToDouble(Request.Form["len"]);
                if (Request.Form["date"] != "")
                    filter.date = Convert.ToDateTime(Request.Form["date"]);
                if (Request.Form["threed"] != "")
                    if (Request.Form["threed"] == "on")
                        filter.threed = true;
                    else
                        filter.threed = false;
                foreach (string language in filter.languages.Keys)
                    if (Request.Form[language] == "on")
                        filter.languages[language] = true;
                    else
                        filter.languages[language] = false;
                foreach (string genre in filter.genres.Keys)
                    if (Request.Form[genre] == "on")
                        filter.genres[genre] = true;
                    else
                        filter.genres[genre] = false;
                foreach (string kijkwijzer in filter.kijkwijzers.Keys)
                    if (Request.Form[kijkwijzer] == "on")
                        filter.kijkwijzers[kijkwijzer] = true;
                    else
                        filter.kijkwijzers[kijkwijzer] = false;
            }
            shows = filter.Apply(shows);
            ViewData["ShowsFilter"] = filter;
            return View("Index", _showService.GetShowsPerMoviePerDay(shows));
        }
    }
}
