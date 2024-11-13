using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TI_NET_API.API.Context;
using TI_NET_API.API.DTO;
using TI_NET_API.API.Mappers;
using TI_NET_API.API.Models;

namespace TI_NET_API.API.Controllers
{
    [Route("api/[controller]")] // localhost/7035/api/Movies
    [ApiController]
    public class MoviesController : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Movie>> GetAll()
        {
            IEnumerable<Movie> movies = FakeDB.Movies;

            if (movies is not null)
            {
                return Ok(movies);
            }

            return NotFound();
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Movie> GetById([FromRoute] int id) 
        { 
            Movie? movie = FakeDB.Movies.SingleOrDefault(x => x.Id == id);

            if(movie is not null)
            {
                return Ok(movie);
            }

            return NotFound(new { message = " l'id n'existe pas" });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Movie> Create([FromBody] MovieCreateFormDTO movieDTO)
        {
            if (movieDTO is null || !ModelState.IsValid) 
            {
                return BadRequest();
            }

            Movie movieToAdd = movieDTO.ToMovie();

            FakeDB.Movies.Add(movieToAdd);

            return CreatedAtAction(nameof(GetById), new { id = movieToAdd.Id }, movieToAdd);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Movie> Update([FromRoute] int id, [FromBody] MovieUpdateFormDTO movieDTO)
        {
            if(movieDTO is null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            Movie? movie = FakeDB.Movies.SingleOrDefault(x => x.Id == id);

            if (movie is null)
            {
                return NotFound(new {message = $"L'Id : {id} n'existe pas dans la BDD"});
            }

            movie.Title = movieDTO.Title;
            movie.Synopsis = movieDTO.Synopsis;
            movie.Director = movieDTO.Director;
            movie.Release = movieDTO.Release;

            return Ok(movie);


        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Movie> Patch([FromRoute] int id, [FromBody] MoviePatchFormDTO movieDTO)
        {
            if (movieDTO is null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            Movie? movie = FakeDB.Movies.SingleOrDefault(x => x.Id == id);

            if (movie is null)
            {
                return NotFound(new { message = $"L'Id : {id} n'existe pas dans la BDD" });
            }

            movie.Release = movieDTO.Release;

            return Ok(movie);

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete([FromRoute] int id) 
        { 
            Movie? movie = FakeDB.Movies.SingleOrDefault(x => x.Id == id);

            if (movie is null)
            {
                return NotFound();
            }

            FakeDB.Movies.Remove(movie);

            return NoContent();

        }



    }
}
