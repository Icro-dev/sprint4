using cinema.Data;
using cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace cinema.Repositories;

public class ShowRepository : IShowRepository
{
    private readonly CinemaContext _context;

    public ShowRepository(CinemaContext cinemaContext)
    {
        _context = cinemaContext;
    }

    public Show FindShowByIdIncludeMovie(int showId)
    {
        return _context.Shows.Include(s => s.Movie).First(s => s.Id == showId);
    }

    public IQueryable<Show> FindAllIncludeMovie()
    {
        return _context.Shows.Include(s => s.Movie);
    }
}