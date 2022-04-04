using cinema.Data;
using cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace cinema.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly CinemaContext _context;

    public MovieRepository(CinemaContext cinemaContext)
    {
        _context = cinemaContext;
    }
    
    public Task<List<Movie>> ListOfAllMovies()
    {
        return _context.Movies.ToListAsync();
    }

    public Task<Movie?> FindMovieById(string id)
    {
        return _context.Movies
            .FirstOrDefaultAsync(m => m.Name == id);
    }

    public void Add(Movie movie)
    {
        _context.Add(movie);
    }

    public void SaveMovie()
    { 
        _context.SaveChangesAsync();
    }

    public void UpdateMovie(Movie movie)
    {
        _context.Update(movie);
    }
    
    

    public void RemoveMovie(Movie movie)
    {
        _context.Movies.Remove(movie);
    }

    public bool MovieExists(string id)
    {
        return _context.Movies.Any(e => e.Name == id);
    }
}