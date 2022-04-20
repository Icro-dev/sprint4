using cinema.Controllers;
using cinema.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using cinema.Models;
using cinema.Repositories;
using cinema.Controllers;
using cinema.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace cinema_unit_testing_sprint4
{

    public class MoviesControllerTests
    {

        [Fact]
        public async Task IndexTest()
        {
            // Arrange
            var movieRepo = new Mock<IMovieRepository>();
            var movieReviewRepo = new Mock<IMovieReviewRepository>();
            var controller = new MoviesController(movieRepo.Object, movieReviewRepo.Object);

            // Act
            var result = await controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        public static Movie getOneMovie()
        {
            var movie = new Movie();
            movie.Cast = "test";
            movie.Description = "test";
            movie.Director = "test";
            movie.Genre = "test";
            movie.Kijkwijzer = "test";
            movie.Language = "test";
            movie.Length = 100;
            movie.Name = "Avatar";
            movie.Poster = "test";
            movie.ReleaseYear = 1999;
            movie.ThreeD = true;
            movie.CountryOfOrigin = "test";
            return movie;
        }

        public static Movie EditOneMovie()
        {
            var movie = new Movie();
            movie.Cast = "edit";
            movie.Description = "edit";
            movie.Director = "edit";
            movie.Genre = "edit";
            movie.Kijkwijzer = "edit";
            movie.Language = "edit";
            movie.Length = 100;
            movie.Name = "Avatar";
            movie.Poster = "edit";
            movie.ReleaseYear = 1999;
            movie.ThreeD = true;
            movie.CountryOfOrigin = "edit";
            return movie;
        }

        public static MovieReview getOneMovieReview()
        {
            var movieReview = new MovieReview();
            movieReview.NameOfMovie = "Avatar";
            movieReview.PostTime = new DateTime(2022, 04, 22, 12, 00, 00);
            movieReview.Review = "test";
            movieReview.UserName = "test";
            return movieReview;
        }

        [Fact]
        public async Task Details()
        {
            // Arrange
            var movieRepo = new Mock<IMovieRepository>();
            var movieReviewRepo = new Mock<IMovieReviewRepository>();
            var controller = new MoviesController(movieRepo.Object, movieReviewRepo.Object);
            var movie = getOneMovie();
            movieRepo.Setup(s => s.FindMovieById("Avatar")).ReturnsAsync(movie);

            // Act
            var result = await controller.Details("Avatar");
            var nullResult = await controller.Details(null);

            // Assert
            Assert.IsType<ViewResult>(result);
            Assert.IsType(typeof(NotFoundResult), nullResult);
        }

        [Fact]
        public async Task ReviewsForMovie()
        {
            // Arrange
            var movieRepo = new Mock<IMovieRepository>();
            var movieReviewRepo = new Mock<IMovieReviewRepository>();
            var controller = new MoviesController(movieRepo.Object, movieReviewRepo.Object);
            var movie = getOneMovie();
            var movieReview = getOneMovieReview();
            movieRepo.Setup(s => s.FindMovieByIdNonTask("Avatar")).Returns(movie);
            movieReviewRepo.Setup(mr => mr.ListOfAllReviewsWithMovieName("Avatar")).Returns(() => new List<MovieReview> { movieReview });

            // Act
            var result =  controller.ReviewsForMovie("Avatar");
            var nonExistentMovie =  controller.ReviewsForMovie("test");

            // Assert
            Assert.IsType<ViewResult>(result);
            Assert.IsType<String>("NotFound");
        }

        [Fact]
        public async Task EditAdd()
        {
            // Arrange
            var movieRepo = new Mock<IMovieRepository>();
            var movieReviewRepo = new Mock<IMovieReviewRepository>();
            var controller = new MoviesController(movieRepo.Object, movieReviewRepo.Object);
            var movie = EditOneMovie();
            movieRepo.Setup(s => s.Add(movie));

            // Act
            var result = await controller.Edit("Avatar", movie);

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }

    }
}
