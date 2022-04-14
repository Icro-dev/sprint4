using cinema.Data;
using cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace cinema.Repositories
{
    public class MovieReviewRepository : IMovieReviewRepository
    {
            private readonly CinemaContext _context;

            public MovieReviewRepository(CinemaContext cinemaContext)
            {
                _context = cinemaContext;
            }
        public async Task<List<MovieReview>> ListOfAllReviews()
        {
            return await _context.MovieReview.ToListAsync();
        }

        public Task<MovieReview?> FindMovieReviewById(int Id)
        {
            return _context.MovieReview
                .FirstOrDefaultAsync(m => m.Id == Id);
        }

  
        public List<MovieReview> ListOfAllReviewsWithMovieName(string movieName)
        {
            List<MovieReview> results = _context.MovieReview.ToList().FindAll(mr => FindMovieReview(mr, movieName));
            if(results.Count != 0)
            {
                return results;
            }
            else
            {
                var emptyList = new List<MovieReview>();
                return emptyList;
            }
        }

        private static bool FindMovieReview(MovieReview mr, string movieName)
        {
            if (mr.NameOfMovie == movieName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Add(MovieReview movieReview)
        {
            _context.Add(movieReview);
        }

        public void SaveMovieReview()
        {
            _context.SaveChangesAsync();
        }

        public ValueTask<MovieReview?> FindMovieReviewEdit(int Id)
        {
            return _context.MovieReview.FindAsync(Id);
        }

        public void UpdateMovieReview(MovieReview movieReview)
        {
            _context.Update(movieReview);
        }

        public void RemovieMovieReview(MovieReview movieReview)
        {
            _context.MovieReview.Remove(movieReview);
        }

        public bool MovieReviewExists(int Id)
        {
            return _context.MovieReview.Any(e => e.Id == Id);
        }  
    }
}
