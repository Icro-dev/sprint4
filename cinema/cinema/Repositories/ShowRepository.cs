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

    public List<Show> ListOfShowsPerDate()
    {
        return _context.Shows.Include(s => s.Movie).ToList();
    }
    
    public Task<Show?> FindShowById(int id)
    {
        return _context.Shows
            .FirstOrDefaultAsync(m => m.Id == id);
    }
    
    public ValueTask<Show?> FindShowByIdWithoutFirstOrDefault(int id)
    {
        return _context.Shows.FindAsync(id);
    }
    
    
    public void Add( Show show)
    {
        _context.Add(show);
    }

    public void SaveShow()
    { 
        _context.SaveChangesAsync();
    }
    
    public void UpdateShow(Show show)
    {
        _context.Update(show);
    }
    
    public void RemoveShow(Show show)
    {
        _context.Shows.Remove(show);
    }

    public bool ShowExists(int id)
    {
        return _context.Shows.Any(e => e.Id == id);
    }

    public List<Show> SortedListOfShowsThatStartToday()
    {
       return _context.Shows.Include(s => s.Movie).Where(s => s.StartTime > DateTime.Now)
            .Where(s => s.StartTime.Date == DateTime.Today.Date).ToList();
    }
    
    public List<Show> ListOfMoviesThatStartSoon()
    {
        return _context.Shows.Include(s => s.Movie).Where(s => s.StartTime > DateTime.Now).ToList();
    }
   
}