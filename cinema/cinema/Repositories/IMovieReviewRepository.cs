using cinema.Models;
namespace cinema.Repositories
{
    public interface IMovieReviewRepository
    {
        Task<List<MovieReview>> ListOfAllReviews();

        Task<MovieReview?> FindMovieReviewById(int Id);

        List<MovieReview> ListOfAllReviewsWithMovieName(string movieName);

        void Add(MovieReview movieReview);

        void SaveMovieReview();

        public ValueTask<MovieReview?> FindMovieReviewEdit(int Id);

        public void UpdateMovieReview(MovieReview movieReview);

        public void RemovieMovieReview(MovieReview movieReview);

        public bool MovieReviewExists(int Id);
    }
}
