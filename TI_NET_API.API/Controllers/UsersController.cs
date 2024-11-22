using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TI_NET_API.API.DTO;
using TI_NET_API.API.Mappers;
using TI_NET_API.API.Services;
using TI_NET_API.BLL.Exceptions;
using TI_NET_API.BLL.Interfaces;
using TI_NET_API.DOMAIN.Models;

namespace TI_NET_API.API.Controllers
{
    [ApiVersion(1, Deprecated = true)]
    [ApiVersion(2)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly AuthService _authService;

        public UsersController(IUserService service, AuthService authService)
        {
            _service = service;
            _authService = authService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<UserListViewDTO>> GetAll()
        {
            try
            {
                IEnumerable<User> users = _service.GetAll();
                return Ok(users.Select(UserMappers.ToListDTO));
            }
            catch (CustomSqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Une erreur interne est survenue lors de la récupération des utilisateurs");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public ActionResult<UserViewDTO> GetById([FromRoute] int id)
        {
            try
            {
                User? user = _service.GetById(id);

                if (user is not null)
                {
                    return Ok(user.ToDTO());
                }

                return NotFound(new { message = $"L'utilisateur avec l'Id {id} n'existe pas" });
            }
            catch (CustomSqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Une erreur interne est survenue lors de la récupération de l'utilisateur");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public ActionResult<UserViewDTO> Create([FromBody] UserCreateFormDTO userDTO)
        {
            try
            {
                if (userDTO is null || !ModelState.IsValid)
                {
                    return BadRequest(new { message = "Les données de l'utilisateur sont invalides" });
                }

                User? userToAdd = _service.Create(userDTO.ToUser());

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = userToAdd.Id },
                    userToAdd.ToDTO()
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
                    "Une erreur interne est survenue lors de la création de l'utilisateur");
            }
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public ActionResult<UserViewDTO> Update([FromRoute] int id, [FromBody] UserUpdateFormDTO userDTO)
        {
            try
            {
                if (userDTO is null || !ModelState.IsValid)
                {
                    return BadRequest(new { message = "Les données de l'utilisateur sont invalides" });
                }

                User? updatedUser = _service.Update(id, userDTO.ToUser());

                if (updatedUser is null)
                {
                    return NotFound(new { message = $"L'utilisateur avec l'Id {id} n'existe pas" });
                }

                return Ok(updatedUser.ToDTO());
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
                    "Une erreur interne est survenue lors de la mise à jour de l'utilisateur");
            }
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public ActionResult<UserViewDTO> Patch([FromRoute] int id, [FromBody] UserPatchFormDTO userDTO)
        {
            try
            {
                if (userDTO is null || !ModelState.IsValid)
                {
                    return BadRequest(new { message = "Les données de l'utilisateur sont invalides" });
                }

                User? patchedUser = _service.Patch(id, userDTO.ToUser());

                if (patchedUser is null)
                {
                    return NotFound(new { message = $"L'utilisateur avec l'Id {id} n'existe pas" });
                }

                return Ok(patchedUser.ToDTO());
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
                    "Une erreur interne est survenue lors de la mise à jour partielle de l'utilisateur");
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete([FromRoute] int id)
        {
            try
            {
                bool isDeleted = _service.Delete(id);

                if (!isDeleted)
                {
                    return NotFound(new { message = $"L'utilisateur avec l'Id {id} n'existe pas" });
                }

                return Ok(new { message = $"L'utilisateur avec l'Id {id} a été supprimé" });
            }
            catch (CustomSqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Une erreur interne est survenue lors de la suppression de l'utilisateur");
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public ActionResult<string> Login([FromBody] UserLoginDTO loginDTO)
        {
            try
            {
                if (loginDTO is null || !ModelState.IsValid)
                {
                    return BadRequest(new { message = "Les données de connexion sont invalides" });
                }

                User? user = _service.Login(loginDTO.Email, loginDTO.Password);

                if (user is null)
                {
                    return Unauthorized(new { message = "Email ou mot de passe incorrect" });
                }

                string token = _authService.GenerateToken(user);
                return Ok(token );
            }
            catch (CustomSqlException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Une erreur interne est survenue lors de la connexion");
            }
        }
    }
}
