using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
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
            var movies = MoviesClient.GetInstance().GetMovies();
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
            var movies = MoviesClient.GetInstance().GetMovies();
            Movie movieToReturn = null;
            try
            {
                movieToReturn = logicHandler.GetSingleMovie(movies, id);
            }
            catch (Exception ex)
            {
                if(ex is ArgumentNullException)
                {
                    return new BadRequestObjectResult("No movie with that Id");
                }
            }
            return new OkObjectResult(movieToReturn);
        }
    }
}