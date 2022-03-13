using cinema.Data;
using cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace cinema.Services;

public class MovieService : IMovieService
{
    private readonly CinemaContext _context;

    public MovieService(CinemaContext context)
    {
        _context = context;
    }

    public Movie GetMovieFromShow(int showId)
    {
        var show = _context.Shows.Include(s => s.Movie).FirstOrDefault(s => s.Id == showId);
        return show.Movie;
    }
}