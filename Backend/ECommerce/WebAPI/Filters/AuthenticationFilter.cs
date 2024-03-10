using BusinessLogic.Interface;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Entities;

namespace WebAPI.Filters
{
    public class AuthenticationFilter :  Attribute, IActionFilter
    {
        private readonly string action;
        private IAdminTokenLogic adminTokenService;
        private IRoleLogic roleService;
        public AuthenticationFilter(string action)
        {
            this.action = action;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string token = context.HttpContext.Request.Headers["Auth"];
            if (token == null)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Se requiere que se ingrese un Token.",
                };
                return;
            }
            this.adminTokenService = GetAdminTokens(context);
            this.roleService = GetRoleService(context);
            Guid tokenGuid = Guid.Parse(token);
            if (!this.adminTokenService.IsLogged(tokenGuid))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = "Se ha ingresado un Token inválido.",
                };
                return;
            }
            AdminToken at = adminTokenService.GetAdminTokenById(tokenGuid,roleService);
            if (!HasPermission(action, at.User.Roles))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "No posee los privilegios necesarios para la accion seleccionada.",
                };
                return;
            }
        }
        private static bool HasPermission(string action, List<Role> roles) 
        {
            var hasPermission = false;
            foreach (Role role in roles) 
            {
                foreach (Permission permission in role.Permissions) 
                {
                    if (permission.Name.Equals(action, StringComparison.OrdinalIgnoreCase)) 
                    {
                        return true;
                    }
                }
            }
            return hasPermission;
        }

        private static IAdminTokenLogic GetAdminTokens(ActionExecutingContext context)
        {
            return (IAdminTokenLogic)context.HttpContext.RequestServices.GetService(typeof(IAdminTokenLogic));
        }
        private static IRoleLogic GetRoleService(ActionExecutingContext context)
        {
            return (IRoleLogic)context.HttpContext.RequestServices.GetService(typeof(IRoleLogic));
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
