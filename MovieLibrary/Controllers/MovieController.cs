using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace MovieLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController
    {
        LogicHandler logicHandler;
        public MovieController()
        {
            logicHandler = new LogicHandler();
        }

        [HttpGet]
        [Route("/movies")]
        public IActionResult GetMovies([FromQuery]bool ascending)
        {      
            var movies = MovieHttpClient.GetInstance().GetMovies();
            var sortedMovies = logicHandler.SortMovies(movies, ascending);
            if (sortedMovies.Any())
            {
                return new OkObjectResult(sortedMovies.Select(i => i.Title));
            } else
            {
                return new NoContentResult();
            }
        }
        
        [HttpGet]
        [Route("/movie/{id}")]
        public IActionResult GetMovieById(string id) 
        {
            Movie movieToReturn = null;
            var movies = MovieHttpClient.GetInstance().GetMovies();
            if (!movies.Any())
            {
                return new NoContentResult();
            }
            try
            {
                movieToReturn = logicHandler.GetSingleMovie(movies, id);
            }
            catch (Exception ex)
            {
                if(ex is ArgumentNullException)
                {
                    return new BadRequestObjectResult("No movie with that id");
                }
            }
            return new OkObjectResult(movieToReturn);
        }
    }
}