using BusinessLogic.Interface;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Reflection;
using WebAPI.Filters;
using WebAPI.Models;
using WebAPI.Models.Write;

namespace WebAPI.Controllers
{
    [ApiExceptionFilter]
    [ApiController]
    [Route("api/discounts")]
    public class DiscountController : ControllerBase
    {
        private IDiscountLogic DiscountService;
        public DiscountController(IDiscountLogic discountService) : base()
        {
            this.DiscountService = discountService;
        }
        [ProducesResponseType(200)]
        [HttpGet]
        public IActionResult Get([FromQuery] string productsIds,[FromServices] IReflectionImplementation reflection,
            [FromServices] IProductLogic productLogic)
        {
            if (productsIds.IsNullOrEmpty()) 
            {
                return new OkObjectResult(new
                {
                    mensaje = "No fue posible aplicar ningun descuento",
                    descuento = 0,
                });
            }
            var discount = this.DiscountService.CalculateDiscount(productsIds, reflection, productLogic);
            return new OkObjectResult(new
            {
                mensaje = discount.name,
                descuento = discount.amountDiscounted,
            });


        }
    }
}
