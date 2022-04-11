using cinema.Data;
using cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace cinema.Repositories;

public class MovieReviewRepository
{
    private readonly CinemaContext _context;

    public MovieReviewRepository(CinemaContext cinemaContext)
    {
        _context = cinemaContext;
    }
    
    public Task<List<MovieReview>> ListOfMovieReviews()
    {
        return _context.MovieReviews.ToListAsync();
    }
    
    public Task<MovieReview?> FindMovieReviewById(int id)
    {
        return _context.MovieReviews
            .FirstOrDefaultAsync(m => m.Id == id);
       
    }
    
    public void AddMovieReview(MovieReview movieReview)
    {
        _context.Add(movieReview);
    }

    public void SaveMovieReview()
    { 
        _context.SaveChangesAsync();
    }

    public void UpdateMovieReview(MovieReview movieReview)
    {
        _context.Update(movieReview);
    }
    
    

    public void RemoveMovieReview(MovieReview movieReview)
    {
        _context.MovieReviews.Remove(movieReview);
    }

    public bool MovieReviewExists(int id)
    {
        return _context.MovieReviews.Any(e => e.Id == id);
    }
    
}