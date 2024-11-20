using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TI_NET_API.API.DTO;
using TI_NET_API.API.Mappers;
using TI_NET_API.BLL.Exceptions;
using TI_NET_API.BLL.Interfaces;
using TI_NET_API.DOMAIN.Models;

namespace TI_NET_API.API.Controllers
{
    [Route("api/[controller]")] // localhost/7035/api/Movies
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {

        private readonly IMovieService _service;

        public MoviesController(IMovieService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public ActionResult<IEnumerable<MovieListViewDTO>> GetAll()
        {
            try
            {
  
                IEnumerable<Movie> movies = _service.GetAll();
                return Ok(movies.Select(MovieMappers.ToListDTO));
                
            }catch (CustomSqlException ex)
            {
                return StatusCode(500, ex.Message);
            }catch (Exception ex)
            {
                return Problem();
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MovieViewDTO> GetById([FromRoute] int id)
        {
            Movie? movie = _service.GetById(id);

            if (movie is not null)
            {
                return Ok(movie.ToDTO());
            }

            return NotFound(new { message = $"l'Id {id} n'existe pas dans la BDD" });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<MovieViewDTO> Create([FromBody] MovieCreateFormDTO movieDTO)
        {
            try
            {
                if (movieDTO is null || !ModelState.IsValid)
                {
                    return BadRequest();
                }

                Movie? movieToAdd = _service.Create(movieDTO.ToMovie());

                return CreatedAtAction(nameof(GetById), new { id = movieToAdd.Id }, movieToAdd.ToDTO());
            }
            catch (CustomSqlException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult<MovieViewDTO> Update([FromRoute] int id, [FromBody] MovieUpdateFormDTO movieDTO)
        {
            if (movieDTO is null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            Movie? movie = _service.Update(id, movieDTO.ToMovie());

            if (movie is null)
            {
                return NotFound(new { message = $"L'Id : {id} n'existe pas dans la BDD" });
            }


            return Ok(movie.ToDTO());
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Moderator)}")]
        public ActionResult<MovieViewDTO> Patch([FromRoute] int id, [FromBody] MoviePatchFormDTO movieDTO)
        {
            if (movieDTO is null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            Movie? movie = _service.Patch(id, movieDTO.ToMovie());

            if (movie is null)
            {
                return NotFound(new { message = $"L'Id : {id} n'existe pas dans la BDD" });
            }

            return Ok(movie.ToDTO());

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete([FromRoute] int id)
        {
            return _service.Delete(id) ? NoContent() : NotFound(new { message = $"L'Id : {id} n'existe pas dans la BDD" });
        }
    }
}
