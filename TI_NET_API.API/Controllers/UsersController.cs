using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TI_NET_API.API.DTO;
using TI_NET_API.API.Mappers;
using TI_NET_API.API.Services;
using TI_NET_API.BLL.Interfaces;
using TI_NET_API.DOMAIN.Models;

namespace TI_NET_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AuthService _authService;
        private readonly MailHelperService _mailService;

        public UsersController(IUserService service, AuthService authService, MailHelperService mailService)
        {
            _userService = service;
            _authService = authService;
            _mailService = mailService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UserListViewDTO>> GetAll()
        {
            IEnumerable<User> users = _userService.GetAll();

            if (users is not null)
            {
                return Ok(users.Select(UserMappers.ToListDTO));
                // return Ok(users.Select(u => u.ToListDTO()));
            }

            return NotFound();
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize]
        public ActionResult<UserViewDTO> GetById([FromRoute] int id)
        {
            User? user = _userService.GetById(id);

            if (user is not null)
            {
                return Ok(user.ToDTO());
            }

            return NotFound(new { message = $"l'Id {id} n'existe pas dans la BDD" });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserViewDTO> Create([FromBody] UserCreateFormDTO userDTO)
        {
            // Check des données
            if (userDTO is null || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Données invalides" });
            }

            // Création du compte
            User? userToAdd = _userService.Create(userDTO.ToUser());
            if(userToAdd is null)
            {
                return BadRequest(new { message = "Erreurs lors de la création du compte" });
            }

            // Envoyer une mail
            _mailService.SendWelcome(userToAdd);

            // Reponse de la requete
            return CreatedAtAction(nameof(GetById), new { id = userToAdd.Id }, userToAdd.ToDTO());
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize]
        public ActionResult<UserViewDTO> Update([FromRoute] int id, [FromBody] UserUpdateFormDTO userDTO)
        {
            // Check les claims (via le JWT) pour savoir si l'utilisateur est Admin ou si c'est l'utilisateur ciblé
            if(HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.Role).Value != Role.Admin.ToString()
                && HttpContext.User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value != id.ToString())
            {
                    return Forbid();
            }

            if (userDTO is null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            User? user = _userService.Update(id, userDTO.ToUser());

            if (user is null)
            {
                return NotFound(new { message = $"L'Id : {id} n'existe pas dans la BDD" });
            }

            return Ok(user.ToDTO());
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin")]
        public ActionResult<UserViewDTO> Patch([FromRoute] int id, [FromBody] UserPatchFormDTO userDTO)
        {
            if (userDTO is null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            User? user = _userService.Patch(id, userDTO.ToUser());

            if (user is null)
            {
                return NotFound(new { message = $"L'Id : {id} n'existe pas dans la BDD" });
            }

            return Ok(user.ToDTO());

        }

        [HttpDelete("{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = nameof(Role.Admin))]
        public ActionResult Delete([FromRoute] int id)
        {
            return _userService.Delete(id) ? NoContent() : NotFound(new { message = $"L'Id : {id} n'existe pas dans la BDD" });
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<string> Login([FromBody] UserLoginDTO user)
        {
            User? response = _userService.Login(user.Email, user.Password);

            if (response is not null)
            {
                string token = _authService.GenerateToken(response);
                return Ok(token);
            }

            return BadRequest(new { message = "Connexion impossible !" });
        }

    }
}
