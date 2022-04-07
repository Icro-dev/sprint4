using cinema.Data;
using cinema.Models;

namespace cinema.Repositories;

public interface IShowRepository
{
    public Show FindShowByIdIncludeMovie(int showId);
    
    public IQueryable<Show> FindAllIncludeMovie();

    public List<Show> ListOfShowsPerDate();

    public Task<Show?> FindShowById(int id);

    public ValueTask<Show?> FindShowByIdWithoutFirstOrDefault(int id);

    public void Add(Show show);

    public void SaveShow();

    public void UpdateShow(Show show);

    public void RemoveShow(Show show);

    public bool ShowExists(int id);

    public List<Show> SortedListOfShowsThatStartToday();
    public List<Show> ListOfMoviesThatStartSoon();

}