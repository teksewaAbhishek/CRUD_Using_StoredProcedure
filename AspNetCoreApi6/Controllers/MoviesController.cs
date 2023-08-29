using AspNetCoreApi6.Contextes;
using AspNetCoreApi6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Telerik.Web.UI.Barcode;

namespace AspNetCoreApi6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _dbContext;
        private readonly AuthDemoDbContext _authDemoDbContext;
        //private readonly Movie2Context _movie2Context;


        public MoviesController(MovieContext dbContext, AuthDemoDbContext authDemoDbContext)
        {
            _dbContext = dbContext;
            _authDemoDbContext = authDemoDbContext; 
        }


        //GET: api/Movies
        /* [HttpGet]
         public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
         {
             if(_dbContext.Movies == null)
             {
                 return NotFound();
             }
             return await _dbContext.Movies.ToListAsync();

         }*/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            var movies = await _dbContext.Movies
                .FromSqlRaw("EXEC GetMovies")
                .ToListAsync();

            if (movies == null || movies.Count == 0)
            {
                return NotFound("No records available");
            }

            return movies;
        }


       /* [HttpGet("CombinedData")]
        public async Task<ActionResult<IEnumerable<CombinedRecord>>> GetCombinedData()
        {
            var moviesFromMovieContext = await _dbContext.Movies.ToListAsync();
            var employeesFromAuthDemoContext = await _authDemoDbContext.Employee.ToListAsync();

            var combinedData = new List<CombinedRecord>();

            foreach (var movie in moviesFromMovieContext)
            {
                combinedData.Add(new CombinedRecord { MovieData = movie });
            }

            foreach (var employee in employeesFromAuthDemoContext)
            {
                combinedData.Add(new CombinedRecord { EmployeeData = employee });
            }

            return combinedData;
        }


*/


        //GET: api/Movies/5
        /* [HttpGet("{id}")]
         public async Task<ActionResult<Movie>> GetMovie(int id)
         {
             if (_dbContext.Movies == null)
             {
                 return NotFound();
             }
             var movie = await _dbContext.Movies.FindAsync(id);
             if (movie == null)
             {
                 return NotFound();
             }
             return movie;
         }*/

        [HttpGet("{id}")]
       
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            if (_dbContext.Movies == null)
            {
                return NotFound();
            }

            var movies = _dbContext.Movies.FromSqlInterpolated($"EXEC sp_GetMovieById {id}").AsEnumerable();
            var movie = movies.FirstOrDefault();

            if (movie == null)
            {
                return NotFound();
            }
            return movie;
        }







        //[Authorize]
        //POST: api/Movies
        /*  [HttpPost]
          public async Task<ActionResult<Movie>> PostMovie(Movie movie)
          {
              _dbContext.Movies.Add(movie);
              await _dbContext.SaveChangesAsync();

              return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
          }*/
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            var titleParam = new SqlParameter("@Title", movie.Title);
            var genreParam = new SqlParameter("@Genre", movie.Genre);
            var releaseDateParam = new SqlParameter("@ReleaseDate", movie.ReleaseDate);

            await _dbContext.Database.ExecuteSqlRawAsync("EXEC InsertMovie @Title, @Genre, @ReleaseDate", titleParam, genreParam, releaseDateParam);

            // Since the stored procedure doesn't return a result set, you can create a response without querying again.
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
        /* [HttpPut("{id}")]
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
         }*/

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            var idParam = new SqlParameter("@Id", id);
            var titleParam = new SqlParameter("@Title", movie.Title);
            var genreParam = new SqlParameter("@Genre", movie.Genre);
            var releaseDateParam = new SqlParameter("@ReleaseDate", movie.ReleaseDate);

            // Assuming you have a stored procedure named 'UpdateMovie'
            await _dbContext.Database.ExecuteSqlRawAsync("EXEC UpdateMovie @Id, @Title, @Genre, @ReleaseDate", idParam, titleParam, genreParam, releaseDateParam);

            return NoContent();
        }


        //[Authorize]
        //DELETE: api/Movies/5
        /*  [HttpDelete("{id}")]
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
          }*/

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var idParam = new SqlParameter("@Id", id);

            // Assuming you have a stored procedure named 'DeleteMovie'
            await _dbContext.Database.ExecuteSqlRawAsync("EXEC DeleteMovie @Id", idParam);

            return NoContent();
        }


        private bool MovieExists(long id)
        {
            return (_dbContext.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
