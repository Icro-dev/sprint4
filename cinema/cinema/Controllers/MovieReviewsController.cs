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
using cinema.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace cinema.Controllers
{
    public class MovieReviewsController : Controller
    {
        private readonly IMovieReviewRepository _movieReviewRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public MovieReviewsController(IMovieReviewRepository movieReviewRepository, UserManager<IdentityUser> userManager)
        {
            _movieReviewRepository = movieReviewRepository;
            _userManager = userManager;
        }
   

        // GET: MovieReviews
        public async Task<IActionResult> Index()
        {
            return View(await _movieReviewRepository.ListOfAllReviews());
        }

        // GET: MovieReviews/Details/5
        public async Task<IActionResult> Details(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var movieReview = await _movieReviewRepository.FindMovieReviewById(Id);
            if (movieReview == null)
            {
                return NotFound();
            }

            return View(movieReview);
        }

        // GET: MovieReviews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MovieReviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, NameOfMovie,UserName,Review,PostTime")] MovieReview movieReview)
        {
            if (ModelState.IsValid)
            {
                _movieReviewRepository.Add(movieReview);
                _movieReviewRepository.SaveMovieReview();
                return RedirectToAction(nameof(Index));
            }
            return View(movieReview);
        }

        // GET: MovieReviews/Edit/5
        public async Task<IActionResult> Edit(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var movieReview = await _movieReviewRepository.FindMovieReviewEdit(Id);
            if (movieReview == null)
            {
                return NotFound();
            }
            return View(movieReview);
        }

        // POST: MovieReviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("Id, NameOfMovie,UserName,Review,PostTime")] MovieReview movieReview)
        {
            if (Id != movieReview.Id)
            {
                return NotFound();
            }

            var reviewUserName = movieReview.UserName;
            var currentlyLoggedInUser = await _userManager.GetUserAsync(HttpContext.User);
            var stringOfCLU = currentlyLoggedInUser.ToString();
            if(reviewUserName != stringOfCLU)
            {
                return RedirectToAction(nameof(Index));
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

        // GET: MovieReviews/Delete/5
        public async Task<IActionResult> Delete(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var movieReview = await _movieReviewRepository.FindMovieReviewById(Id);
            
            if (movieReview == null)
            {
                return NotFound();
            }

            return View(movieReview);
        }

        // POST: MovieReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var movieReview = await _movieReviewRepository.FindMovieReviewEdit(Id);
            _movieReviewRepository.RemovieMovieReview(movieReview);
            _movieReviewRepository.SaveMovieReview();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieReviewExists(int Id)
        {
            return _movieReviewRepository.MovieReviewExists(Id);
        }
    }
}
