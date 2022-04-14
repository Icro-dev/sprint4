using cinema.Models;

namespace cinema.Repositories;

public interface IMovieRepository
{
  Task<List<Movie>> ListOfAllMovies();
  Task<Movie?> FindMovieById(string id);

  public Movie? FindMovieByIdNonTask(string id);
  void Add(Movie movie);
  void SaveMovie();
  void UpdateMovie(Movie movie);
  void RemoveMovie(Movie movie);
  bool MovieExists(string id);
}