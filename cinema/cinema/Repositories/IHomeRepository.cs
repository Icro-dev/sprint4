using cinema.Models;
using Microsoft.AspNetCore.Mvc;

namespace cinema.Repositories;

public interface IHomeRepository
{ 
    IQueryable<Movie> GetMoviesThatStartWithin8DaysSort();

}