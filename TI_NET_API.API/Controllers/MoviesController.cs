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
    [Route("api/[controller]")]
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
            }
            catch (CustomSqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Une erreur interne est survenue lors de la récupération des films");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MovieViewDTO> GetById([FromRoute] int id)
        {
            try
            {
                Movie? movie = _service.GetById(id);

                if (movie is not null)
                {
                    return Ok(movie.ToDTO());
                }

                return NotFound(new { message = $"Le film avec l'Id {id} n'existe pas" });
            }
            catch (CustomSqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Une erreur interne est survenue lors de la récupération du film");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MovieViewDTO> Create([FromBody] MovieCreateFormDTO movieDTO)
        {
            try
            {
                if (movieDTO is null || !ModelState.IsValid)
                {
                    return BadRequest(new { message = "Les données du film sont invalides" });
                }

                Movie? movieToAdd = _service.Create(movieDTO.ToMovie());

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = movieToAdd.Id },
                    movieToAdd.ToDTO()
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (CustomSqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Une erreur interne est survenue lors de la création du film");
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult<MovieViewDTO> Update([FromRoute] int id, [FromBody] MovieUpdateFormDTO movieDTO)
        {
            try
            {
                if (movieDTO is null || !ModelState.IsValid)
                {
                    return BadRequest(new { message = "Les données du film sont invalides" });
                }

                Movie? movie = _service.Update(id, movieDTO.ToMovie());

                if (movie is null)
                {
                    return NotFound(new { message = $"Le film avec l'Id {id} n'existe pas" });
                }

                return Ok(movie.ToDTO());
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (CustomSqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Une erreur interne est survenue lors de la mise à jour du film");
            }
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult<MovieViewDTO> Patch([FromRoute] int id, [FromBody] MoviePatchFormDTO movieDTO)
        {
            try
            {
                if (movieDTO is null || !ModelState.IsValid)
                {
                    return BadRequest(new { message = "Les données du film sont invalides" });
                }

                Movie? movie = _service.Patch(id, movieDTO.ToMovie());

                if (movie is null)
                {
                    return NotFound(new { message = $"Le film avec l'Id {id} n'existe pas" });
                }

                return Ok(movie.ToDTO());
            }
            catch (CustomSqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Une erreur interne est survenue lors de la mise à jour partielle du film");
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete([FromRoute] int id)
        {
            try
            {
                bool deleted = _service.Delete(id);
                return deleted ? NoContent() : NotFound(new { message = $"Le film avec l'Id {id} n'existe pas" });
            }
            catch (CustomSqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Une erreur interne est survenue lors de la suppression du film");
            }
        }
    }
}
