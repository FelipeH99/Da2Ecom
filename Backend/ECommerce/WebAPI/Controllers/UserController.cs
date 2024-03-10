using BusinessLogic.Interface;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiExceptionFilter]
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private IUserLogic UserService;

        public UserController(IUserLogic userService) : base()
        {
            this.UserService = userService;
        }

        [ProducesResponseType(typeof(UserModel), 200)]
        [ProducesResponseType(401)]
        [HttpGet]
        public IActionResult Get()
        {
            var users = this.UserService.Get();
            if (users.Count > 0)
            {
                return Ok(UserModel.ToModel(users));
            }
            return NotFound("No hay usuarios en el sistema.");
        }
        [ProducesResponseType(typeof(UserModel), 200)]
        [ProducesResponseType(401)]
        [AuthenticationFilter("GET/USERS{ID}")]
        [HttpGet("{Id}", Name = "GetUser")]
        public IActionResult Get(Guid id)
        {
            var user = this.UserService.Get(id);
            if (user == null)
            {
                return NotFound("No existe el usuario con Id: " + id);
            }
            return Ok(UserModel.ToModel(user));
        }
        [ProducesResponseType(typeof(UserModel), 200)]
        [ProducesResponseType(401)]
        [HttpPut("{Id}")]
        public IActionResult Put(Guid id, [FromBody] UserModel userModel, [FromServices] IRoleLogic roleService)
        {

            if (userModel == null)
            {
                return NotFound("No existe el usuario.");
            }

            userModel.Id = id;
            var userToModify = this.UserService.Get(id);
            if (userModel.RolesId == null) 
            {
                userModel.RolesId = userToModify.Roles.Select(role => role.Id).ToList();
            }
            this.UserService.Update(id, userModel.ToEntity(), roleService);
            return Ok("Se cambio el usuario correctamente.");
        }
        [ProducesResponseType(typeof(UserModel), 201)]
        [ProducesResponseType(401)]
        [HttpPost()]
        public IActionResult Post([FromBody] UserModel userModel, [FromServices] IRoleLogic roleService)
        {
            var user = this.UserService.Create(userModel.ToEntity(),roleService);
            return CreatedAtRoute("GetUser", new { id = user.Id }, UserModel.ToModel(user));
        }
        [ProducesResponseType(typeof(UserModel), 200)]
        [ProducesResponseType(401)]
        [AuthenticationFilter("DELETE/USERS")]
        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid id)
        {
            var user = this.UserService.Get(id);
            if (user == null)
            {
                return NotFound("No existe el usuario con Id: " + id);
            }
            this.UserService.Remove(user);
            return Ok("Se elimino el usuario con el email: " + user.Email);
        }
    }
}
