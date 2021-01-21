using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MovieLibraryUnitTest
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void GetMovieById_ShouldThrowException()
        {
            var logicHandler = new LogicHandler();
            var testMovies = GetTestProducts();
            var testId = "dsak";

            Assert.ThrowsException<ArgumentNullException>(() => logicHandler.GetSingleMovie(testMovies, testId));
        }

        [TestMethod]
        public void GetMovieById_ShouldSucceed()
        {
            var logicHandler = new LogicHandler();
            var testMovies = GetTestProducts();
            var testId = "tt0111161";

            var expected = testMovies.Where(i => i.Id == testId).First();
            var actual = logicHandler.GetSingleMovie(testMovies, testId);
       
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SortMovies_ShouldSortAscending()
        {
            var logicHandler = new LogicHandler();
            var testMovies = GetTestProducts();

            var expected = testMovies.OrderBy(i => i.Rated).ToList();
            var actual = logicHandler.SortMovies(testMovies, true);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SortMovies_ShouldSortDescending()
        {
            var logicHandler = new LogicHandler();
            var testMovies = GetTestProducts();

            var expected = testMovies.OrderByDescending(i => i.Rated).ToList();
            var actual = logicHandler.SortMovies(testMovies, false);

            CollectionAssert.AreEqual(expected, actual);
        }

        private List<Movie> GetTestProducts()
        {
            var movies = new List<MovieLibrary.Movie>()
            {
                new MovieLibrary.Movie()
                {
                    Title = "The Shawshank Redemption",
                    Id = "tt0111161",
                    Rated = double.Parse("9,2")
                },
                new MovieLibrary.Movie()
                {
                    Title = "The Godfather",
                    Id = "tt0068646",
                    Rated = double.Parse("9,1")
                },
                new MovieLibrary.Movie()
                {
                    Title = "The Godfather: Part II",
                    Id = "tt0071562",
                    Rated = double.Parse("9,0")
                },
                new MovieLibrary.Movie()
                {
                    Title = "Pulp Fiction",
                    Id = "tt0110912",
                    Rated = double.Parse("8,8")
                },
                new MovieLibrary.Movie()
                {
                    Title = "The Good, the Bad and the Ugly",
                    Id = "tt0060196",
                    Rated = double.Parse("8,8")
                },
                new MovieLibrary.Movie()
                {
                    Title = "The Dark Knight",
                    Id = "tt0468569",
                    Rated = double.Parse("9,0")
                },
            };
            return movies;
        }
    }
}
