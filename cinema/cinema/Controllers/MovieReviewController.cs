using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cinema.Data;
using cinema.Models;
using cinema.Repositories;

namespace cinema.Controllers
{
    public class MovieReviewController : Controller
    {
        private readonly IMovieReviewRepository _movieReviewRepository;

        public MovieReviewController(IMovieReviewRepository movieReviewRepository)
        {
            _movieReviewRepository = movieReviewRepository;
        }

        // GET: MovieReview
        public async Task<IActionResult> Index()
        {
            return View(await _movieReviewRepository.ListOfMovieReviews());
        }

        // GET: MovieReview/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieReview = await _movieReviewRepository.FindMovieReviewById(id);
            if (movieReview == null)
            {
                return NotFound();
            }

            return View(movieReview);
        }

        // GET: MovieReview/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MovieReview/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, UserName, UserReview, PostDateTime, MovieName")] MovieReview movieReview)
        {
            if (ModelState.IsValid)
            {
                _movieReviewRepository.AddMovieReview(movieReview);
                _movieReviewRepository.SaveMovieReview();
                return RedirectToAction(nameof(Index));
            }
            return View(movieReview);
        }

        // GET: MovieReview/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieReviewRepository.FindMovieReviewById(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: MovieReview/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, UserName, UserReview, PostDateTime, MovieName")] MovieReview movieReview)
        {
            if (id != movieReview.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _movieReviewRepository.UpdateMovieReview(movieReview);
                    _movieReviewRepository.SaveMovieReview();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieReviewExists(movieReview.Id))
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
            return View(movieReview);
        }

        // GET: MovieReview/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieReviewRepository.FindMovieReviewById(id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: MovieReview/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieReview = await _movieReviewRepository.FindMovieReviewById(id);
            _movieReviewRepository.RemoveMovieReview(movieReview);
            _movieReviewRepository.SaveMovieReview();
            return RedirectToAction(nameof(Index));
        }

        public bool MovieReviewExists(int id)
        {
            return _movieReviewRepository.MovieReviewExists(id);
        }
    }
}


