using BusinessLogic.Interface;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;
using WebAPI.Models.Read.Discounts;
using WebAPI.Models.Write.Discounts;

namespace WebAPI.Controllers
{
    [ApiExceptionFilter]
    [ApiController]
    [Route("api/discounts/quantities")]
    public class QuantityDiscountController : ControllerBase
    {
        private IQuantityDiscountLogic QuantityDiscountService;

        public QuantityDiscountController(IQuantityDiscountLogic quantityDiscountService) : base()
        {
            this.QuantityDiscountService = quantityDiscountService;
        }
        [ProducesResponseType(typeof(QuantityDiscountModelRead), 200)]
        [ProducesResponseType(401)]
        [HttpGet]
        public IActionResult Get()
        {
            var discounts = this.QuantityDiscountService.Get();
            if (discounts.Count > 0)
            {
                return Ok(QuantityDiscountModelRead.ToModel(discounts));
            }
            return NotFound("No hay descuentos en el sistema.");
        }
        [ProducesResponseType(typeof(QuantityDiscountModelRead), 200)]
        [ProducesResponseType(401)]
        [AuthenticationFilter("PUT/DISCOUNT")]
        [HttpPut("{Id}")]
        public IActionResult Put(Guid id, [FromBody] QuantityDiscountUpdateModelWrite discountModel)
        {
            this.QuantityDiscountService.Update(id, discountModel.ToEntity());
            return Ok("Se actualizo el descuento correctamente.");

        }
        [ProducesResponseType(typeof(QuantityDiscountModelRead), 201)]
        [ProducesResponseType(401)]
        [AuthenticationFilter("POST/DISCOUNT")]
        [HttpPost()]
        public IActionResult Post([FromBody] QuantityDiscountModelWrite discountModel)
        {
            if (string.IsNullOrEmpty(discountModel.Category))
            {
                return Ok("Indique una categoria correcta para poder realizar el alta.");
            }
            else 
            {
                var discount = this.QuantityDiscountService.Create(discountModel.ToEntity());
                return CreatedAtRoute(null, QuantityDiscountModelRead.ToModel(discount));
            }


        }
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [AuthenticationFilter("DELETE/DISCOUNT")]
        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid id)
        {
            var discount = this.QuantityDiscountService.Get(id);
            if (discount == null)
            {
                return NotFound("No existe el descuento con Id: " + id);
            }
            this.QuantityDiscountService.Remove(discount);
            return Ok("Se elimino el descuento " + discount.Name);
        }
    }
}
