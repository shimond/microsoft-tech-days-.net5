using CatalogApi.Contracts;
using CatalogApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;
        private readonly ILogger<MoviesController> _logger;
        private readonly IConfiguration _configuration;

        public MoviesController(IMoviesService moviesService, ILogger<MoviesController> logger,  
            IConfiguration configuration)
        {
            (_moviesService, _logger , _configuration) = (moviesService, logger, configuration);
        }

        [HttpGet(Name = nameof(GetAllMovies))]
        public async Task<ActionResult<List<Movie>>> GetAllMovies()
        {
            var result = await this._moviesService.GetAll();
            return Ok(result);
        }

        [HttpGet("GetConfig", Name = nameof(GetConfig))]
        public IActionResult GetConfig()
        {
           var value = _configuration["ConnectionSrings:dbConnection"]; // from config files (appsetting + appseting.env)
            return Ok(value);

        }

        [HttpGet("GetOS", Name = nameof(GetOS))]
        public IActionResult GetOS()
        {
            var value = Environment.OSVersion;
            return Ok(value);
        }





        [HttpGet("{id}", Name = nameof(GetMovieById))]
        public async Task<ActionResult<Movie>> GetMovieById(int id)
        {
            try
            {
                var result = await this._moviesService.GetById(id);
                return Ok(result);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("item cannot be found"))
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}", Name = nameof(DeleteMovie))]
        public async Task<ActionResult<Movie>> DeleteMovie(int id)
        {
            try
            {
                var result = await this._moviesService.DeleteMovieItem(id);
                return Ok(result);
            }
            catch (Exception ex) when (ex.Message.Contains("item cannot be found"))
            {
                return NotFound();
            }
        }

        [HttpPost(Name = nameof(AddNewMovie))]
        public async Task<ActionResult<Movie>> AddNewMovie(Movie m)
        {
            var result = await this._moviesService.AddMovie(m);
            return Created($"movies/{result.ID}", result);
        }
    }



}
