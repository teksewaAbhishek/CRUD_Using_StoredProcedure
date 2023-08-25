using AspNetCoreApi6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreApi6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _dbContext;

        public MoviesController(MovieContext dbContext)
        {
            _dbContext = dbContext;
        }

       
        //GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            if(_dbContext.Movies == null)
            {
                return NotFound();
            }
            return await _dbContext.Movies.ToListAsync();

        }

        //GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            if(_dbContext.Movies == null)
            {
                return NotFound();
            }
            var movie = await _dbContext.Movies.FindAsync(id);
            if(movie == null) 
            {
                return NotFound();
            }
            return movie;
        }

        //[Authorize]
        //POST: api/Movies
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _dbContext.Movies.Add(movie);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        [AllowAnonymous]
        //GET: api/Movies/Search
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Movie>>> SearchMovies()
        {
            string searchTerm = HttpContext.Request.Query["searchTerm"];

            if (string.IsNullOrEmpty(searchTerm))
            {
                var allMovies = await _dbContext.Movies.ToListAsync();
                return allMovies;
            }

            var matchingMovies = await _dbContext.Movies
                .Where(movie => movie.Title.Contains(searchTerm) || movie.Genre.Contains(searchTerm))
                .ToListAsync();

          
                if (matchingMovies.Count == 0)
                {
                return NotFound("No records available");
                }

              

            return matchingMovies;
        }

        /*public async Task<ActionResult<IEnumerable<Movie>>> SearchMovies([FromQuery] string searchTerm = null)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                // If search term is null or empty, return all movies
                var allMovies = await _dbContext.Movies.ToListAsync();
                return allMovies;
            }

            var matchingMovies = await _dbContext.Movies
                .Where(movie => movie.Title.Contains(searchTerm) || movie.Genre.Contains(searchTerm))
                .ToListAsync();

            if (matchingMovies.Count == 0)
            {
                return NotFound("No records found");
            }



            return matchingMovies;
        }*/










        //[Authorize]
        //PUT: api/Movies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if(id != movie.Id)
            {
                return BadRequest();
            }
            _dbContext.Entry(movie).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        //[Authorize]
        //DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if(_dbContext.Movies == null)
            {
                return NotFound();
            }
            var movie = await _dbContext.Movies.FindAsync(id);
            if(movie == null) 
            {
                return NotFound();
            }

            _dbContext.Movies.Remove(movie);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(long id)
        {
            return (_dbContext.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
