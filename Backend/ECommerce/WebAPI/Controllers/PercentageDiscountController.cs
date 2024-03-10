using BusinessLogic.Interface;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;
using WebAPI.Models.Write.Discounts;
using WebAPI.Models.Read.Discounts;

namespace WebAPI.Controllers
{
    [ApiExceptionFilter]
    [ApiController]
    [Route("api/discounts/percentages")]
    public class PercentageDiscountController : ControllerBase
    {
        private IPercentageDiscountLogic PercentageDiscountService;

        public PercentageDiscountController(IPercentageDiscountLogic percentageDiscountService) : base()
        {
            this.PercentageDiscountService = percentageDiscountService;
        }
        [ProducesResponseType(typeof(PercentageDiscountModelRead), 200)]
        [ProducesResponseType(401)]
        [HttpGet]
        public IActionResult Get()
        {
            var discounts = this.PercentageDiscountService.Get();
            if (discounts.Count > 0)
            {
                return Ok(PercentageDiscountModelRead.ToModel(discounts));
            }
            return NotFound("No hay descuentos en el sistema.");
        }
        [ProducesResponseType(typeof(PercentageDiscountModelRead), 200)]
        [ProducesResponseType(401)]
        [HttpPut("{Id}")]
        [AuthenticationFilter("PUT/DISCOUNT")]
        public IActionResult Put(Guid id, [FromBody] PercentageDiscountUpdateModelWrite discountModel)
        {
            this.PercentageDiscountService.Update(id, discountModel.ToEntity());
            return Ok("Se actualizo el descuento correctamente.");

        }
        [ProducesResponseType(typeof(PercentageDiscountModelRead), 201)]
        [ProducesResponseType(401)]
        [HttpPost()]
        [AuthenticationFilter("POST/DISCOUNT")]
        public IActionResult Post([FromBody] PercentageDiscountModelWrite discountModel)
        {
            var discount = this.PercentageDiscountService.Create(discountModel.ToEntity());
            return CreatedAtRoute(null, PercentageDiscountModelRead.ToModel(discount));

        }
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpDelete("{Id}")]
        [AuthenticationFilter("DELETE/DISCOUNT")]
        public IActionResult Delete(Guid id)
        {
            var discount = this.PercentageDiscountService.Get(id);
            if (discount == null)
            {
                return NotFound("No existe el descuento con Id: " + id);
            }
            this.PercentageDiscountService.Remove(discount);
            return Ok("Se elimino el descuento " + discount.Name);
        }
    }
}
