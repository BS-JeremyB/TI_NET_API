﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public UsersController(IUserService service, AuthService authService)
        {
            _userService = service;
            _authService = authService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            IEnumerable<User> users = _userService.GetAll();

            if (users is not null)
            {
                return Ok(users);
            }

            return NotFound();
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<User> GetById([FromRoute] int id)
        {
            User? user = _userService.GetById(id);

            if (user is not null)
            {
                return Ok(user);
            }

            return NotFound(new { message = $"l'Id {id} n'existe pas dans la BDD" });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> Create([FromBody] UserCreateFormDTO userDTO)
        {
            if (userDTO is null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            User? userToAdd = _userService.Create(userDTO.ToUser());



            return CreatedAtAction(nameof(GetById), new { id = userToAdd.Id }, userToAdd);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> Update([FromRoute] int id, [FromBody] UserUpdateFormDTO userDTO)
        {
            if (userDTO is null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            User? user = _userService.Update(id, userDTO.ToUser());

            if (user is null)
            {
                return NotFound(new { message = $"L'Id : {id} n'existe pas dans la BDD" });
            }


            return Ok(user);


        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<User> Patch([FromRoute] int id, [FromBody] UserPatchFormDTO userDTO)
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

            return Ok(user);

        }

        [HttpDelete("{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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