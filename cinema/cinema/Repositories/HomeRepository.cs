using System.Linq;
using cinema.Data;
using cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cinema.Repositories;

public class HomeRepository : IHomeRepository
{
    private readonly CinemaContext _context;

        public HomeRepository(CinemaContext cinemaContext)
        {
            _context = cinemaContext;
        }

        public IQueryable<Movie> GetMoviesThatStartWithin8DaysSort() {
            DateTime movieWeek = DateTime.Now.AddDays(8);
          return _context.Shows.Include(s => s.Movie).Where(s => s.StartTime <= movieWeek && s.StartTime >= DateTime.Now)
                .OrderBy(s => s.StartTime).Select(s => s.Movie).Distinct();
        }
}