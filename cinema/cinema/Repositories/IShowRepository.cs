using cinema.Data;
using cinema.Models;

namespace cinema.Repositories;

public interface IShowRepository
{
    public Show FindShowByIdIncludeMovie(int showId);
    
    public IQueryable<Show> FindAllIncludeMovie();

}