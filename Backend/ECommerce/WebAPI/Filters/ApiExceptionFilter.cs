using Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Authentication;
using Exceptions;
namespace WebAPI.Filters
{
    public class ApiExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            try
            {
                throw context.Exception;
            }
            catch (AuthenticationException ex)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = ex.Message
                };
            }
            catch (IncorrectPasswordException ex)
            {
                context.Result = new ContentResult()
                {

                    StatusCode = 400,
                    Content = ex.Message
                };
            }
            catch (FormatException)
            {
                string message = "El email no cumple con el siguiente formato: prueba@prueba.com";
                context.Result = new ContentResult()
                {
                    StatusCode = 400,
                    Content = message
                };
            }
            catch (IncorrectEmailException ex)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 400,
                    Content = ex.Message
                };
            }
            catch (IncorrectNameException ex)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 400,
                    Content = ex.Message
                };
            }
            catch (IncorrectRequestException ex)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 400,
                    Content = ex.Message
                };
            }
            catch (RepeatedObjectException ex)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 400,
                    Content = ex.Message
                };
            }
            catch (ArgumentException ex)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 400,
                    Content = ex.Message
                };
            }
            catch (Exception)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 500,
                    Content = "Problemas del servidor."
                };
            }
        }
    }
}
