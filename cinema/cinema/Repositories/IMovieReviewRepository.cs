using cinema.Models;

namespace cinema.Repositories;

public interface IMovieReviewRepository
{
    public Task<List<MovieReview>> ListOfMovieReviews();

    public Task<MovieReview?> FindMovieReviewById(int id);

    public void AddMovieReview(MovieReview movieReview);

    public void SaveMovieReview();

    public void UpdateMovieReview(MovieReview movieReview);

    public void RemoveMovieReview(MovieReview movieReview);

    public bool MovieReviewExists(int id);
}