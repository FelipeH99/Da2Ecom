using BusinessLogic.Interface;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;
using WebAPI.Models;
using Entities;

namespace WebAPI.Controllers
{
    [ApiExceptionFilter]
    [Route("api/sessions")]
    [ApiController]
    public class AdminTokenController : ControllerBase
    {
        private IAdminTokenLogic adminTokenService;

        public AdminTokenController(IAdminTokenLogic adminTokenService) : base()
        {
            this.adminTokenService = adminTokenService;
        }

        [ProducesResponseType(typeof(ConnectedUserModel), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [HttpGet("{Id}")]
        public IActionResult Get(Guid Id)
        {
            var user = this.adminTokenService.GetUser(Id);
            if (user != null)
            {
                return Ok(ConnectedUserModel.ToModel(user));
            }
            return NotFound("Error al buscar el usuario asociado a la sesion.");
        }
        [ProducesResponseType(typeof(UserModel), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [HttpPost()]
        public IActionResult LogIn([FromBody] AdminTokenModel adminTokenModel, [FromServices] IUserLogic userLogic)
        {
            AdminToken adminToken = this.adminTokenService.Login(adminTokenModel.email, adminTokenModel.password,userLogic);
            return new OkObjectResult(new
            {
                mensaje = "Se ha logeado correctamente.",
                token = adminToken.Id
            });
        }
        [ProducesResponseType(typeof(UserModel), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [HttpDelete("{Id}")]
        public IActionResult LogOut(Guid id)
        {
            if (adminTokenService.IsLogged(id))
            {
                this.adminTokenService.Logout(id);
            }
            else 
            {
                return NotFound("No hay usuarios logueados en el sistema con ese correo electronico.");
            }
            return new OkObjectResult(new
            {
                mensaje = "Se ha deslogueado correctamente.",
            });
        }
    }
}
