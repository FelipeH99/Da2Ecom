using BusinessLogic.Interface;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiExceptionFilter]
    [ApiController]
    [Route("api/roles")]
    public class RoleController : ControllerBase
    {
        private IRoleLogic RoleService;
        private IUserLogic UserService;

        public RoleController(IRoleLogic roleService, IUserLogic userService) : base()
        {
            this.RoleService = roleService;
            this.UserService = userService;
        }
        [ProducesResponseType(200)]
        [HttpGet]
        public IActionResult Get()
        {
            var roles = this.RoleService.Get();
            if (roles.Count > 0)
            {
                return Ok(roles);
            }
            return NotFound("No hay roles registrados en el sistema.");
        }
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpGet("{Id}")]
        public IActionResult GetPermissionsByUser(Guid id)
        {
            var user = this.UserService.Get(id);
            var permissions = this.RoleService.GetPermissionsByRole(user);
            if (permissions.Count > 0)
            {
                return Ok(permissions);
            }
            return NotFound("El usuario no posee ningun permiso en el sistema.");
        }
    }
}
