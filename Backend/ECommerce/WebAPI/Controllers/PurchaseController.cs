using BusinessLogic.Interface;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;
using WebAPI.Models.Read;
using WebAPI.Models.Write;
using Reflection;

namespace WebAPI.Controllers
{
    [ApiExceptionFilter]
    [ApiController]
    [Route("api/purchases")]
    public class PurchaseController : ControllerBase
    {
        private IPurchaseLogic PurchaseService;
        public PurchaseController(IPurchaseLogic purchaseService) : base()
        {
            this.PurchaseService = purchaseService;
        }

        [ProducesResponseType(typeof(PurchaseModelRead), 200)]
        [ProducesResponseType(401)]
        [AuthenticationFilter("GET/PURCHASE")]
        [HttpGet]
        public IActionResult Get()
        {
            var purchases = this.PurchaseService.Get();
            if (purchases.Count > 0)
            {
                return Ok(PurchaseModelRead.ToModel(purchases));
            }
            return NotFound("No hay compras en el sistema.");
        }
        [ProducesResponseType(typeof(PurchaseModelRead), 200)]
        [ProducesResponseType(401)]
        [AuthenticationFilter("GET/PURCHASE{ID}")]
        [HttpGet("{Id}", Name = "GetPurchasesByUser")]
        public IActionResult Get(Guid id)
        {
            var purchases = this.PurchaseService.GetByUserId(id);
            if (purchases.Count > 0)
            {
                return Ok(PurchaseModelRead.ToModel(purchases));
            }
            return NotFound("No existe la compra para el usuario con Id: " + id);
        }
        [ProducesResponseType(typeof(PurchaseModelCreatedRead), 201)]
        [ProducesResponseType(401)]
        [AuthenticationFilter("POST/PURCHASE")]
        [HttpPost()]
        public IActionResult Post([FromBody] PurchaseModelWrite purchaseModel, [FromServices] IProductLogic productService,
            [FromServices] IPaymentMethodLogic paymentMethodService, [FromServices] IReflectionImplementation reflection,
            [FromServices] IDiscountLogic discountLogic)
        {
            if (purchaseModel.ProductIds.Count <= 0)
            {
                return Ok("Para realizar una compra es necesario que haya seleccionado al menos 1 producto.");
            }
            else 
            {
                var purchase = this.PurchaseService.Create(purchaseModel.ToEntity(), productService, paymentMethodService
                    ,reflection, discountLogic);
                return CreatedAtRoute(null, PurchaseModelCreatedRead.ToModel(purchase));
            }
            

        }
    }
}
