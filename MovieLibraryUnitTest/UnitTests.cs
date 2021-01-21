using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace MovieLibraryUnitTest
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void GetMovieById_ShouldThrowException()
        {
            var logicHandler = new LogicHandler();
            var testMovies = GetTestMovies();
            var testId = "dsak";

            Assert.ThrowsException<ArgumentNullException>(() => logicHandler.GetSingleMovie(testMovies, testId));
        }

        [TestMethod]
        public void GetMovieById_ShouldSucceed()
        {
            var logicHandler = new LogicHandler();
            var testMovies = GetTestMovies();
            var testId = "tt0111161";

            var expected = testMovies.Where(i => i.Id == testId).First();
            var actual = logicHandler.GetSingleMovie(testMovies, testId);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SortMovies_ShouldSortAscending()
        {
            var logicHandler = new LogicHandler();
            var testMovies = GetTestMovies();
            var expected = GetTestMoviesOrderedAscending();

            var actual = logicHandler.SortMovies(testMovies, true);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].Title, actual[i].Title);
                Assert.AreEqual(expected[i].Rated, actual[i].Rated);
            }
        }

        [TestMethod]
        public void SortMovies_ShouldSortDescending()
        {
            var logicHandler = new LogicHandler();
            var testMovies = GetTestMovies();
            var expected = GetTestMoviesOrderedDescending();

            var actual = logicHandler.SortMovies(testMovies, false);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].Title, actual[i].Title);
                Assert.AreEqual(expected[i].Rated, actual[i].Rated);
            }
        }

        [TestMethod]
        public void ParseTopp100JsonTest()
        {
            var logicHandler = new LogicHandler();
            var testJson = "[{'title': 'The Shawshank Redemption', 'id': 'tt0111161', 'rated': '9,2'}]";
            var expected = new Movie() { Id = "tt0111161", Title = "The Shawshank Redemption", Rated = 9.2 };

            var actual = logicHandler.ParseTop100JsonToMovieObjects(testJson).Single();

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Title, actual.Title); 
            Assert.AreEqual(expected.Rated, actual.Rated);
        }

        [TestMethod]
        public void ParseDetailedJsonTest()
        {
            var logicHandler = new LogicHandler();
            var testJson = "[{'id': '1','title': 'Black Panther','year': '2018','genres': ['Action','Adventure','Sci-Fi'],'ratings': [4],'poster': 'MV5BMTg1MTY2MjYzNV5BMl5BanBnXkFtZTgwMTc4NTMwNDI@._V1_SY500_CR0,0,337,500_AL_.jpg','contentRating': '15','duration': 'PT134M','releaseDate': '2018-02-14','averageRating': 0,'originalTitle': '','storyline': 'Some People Die','actors': ['Chadwick Boseman'],'imdbRating': 7.0,'posterurl': 'https://images-na.ssl-images-amazon.com/images/M/MV5BMTg1MTY2MjYzNV5BMl5BanBnXkFtZTgwMTc4NTMwNDI@._V1_SY500_CR0,0,337,500_AL_.jpg'}]";
            var expected = new Movie() { Id = "1", Title = "Black Panther", Rated = 7.0 };

            var actual = logicHandler.ParseDetailedJsonToMovieObjects(testJson).Single();

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Rated, actual.Rated);
        }

        private List<Movie> GetTestMovies()
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
                    Rated = double.Parse("8,9")
                },
            };
            return movies;
        }

        private List<Movie> GetTestMoviesOrderedAscending()
        {
            var movies = new List<MovieLibrary.Movie>()
            {
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
                    Rated = double.Parse("8,9")
                },
                new MovieLibrary.Movie()
                {
                    Title = "The Godfather: Part II",
                    Id = "tt0071562",
                    Rated = double.Parse("9,0")
                },
                new MovieLibrary.Movie()
                {
                    Title = "The Godfather",
                    Id = "tt0068646",
                    Rated = double.Parse("9,1")
                },
                new MovieLibrary.Movie()
                {
                    Title = "The Shawshank Redemption",
                    Id = "tt0111161",
                    Rated = double.Parse("9,2")
                }
            };
            return movies;
        }

        private List<Movie> GetTestMoviesOrderedDescending()
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
                    Title = "The Good, the Bad and the Ugly",
                    Id = "tt0060196",
                    Rated = double.Parse("8,9")
                },
                new MovieLibrary.Movie()
                {
                    Title = "Pulp Fiction",
                    Id = "tt0110912",
                    Rated = double.Parse("8,8")
                }
            };
            return movies;
        }
    }
}
