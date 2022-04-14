using cinema.Models;

namespace cinema.ViewModels
{
    public class MovieReviewMovieViewModel
    {
        public Movie? Movie { get; set; }

        public List<MovieReview?> MovieReviews { get; set; }
    }
}
