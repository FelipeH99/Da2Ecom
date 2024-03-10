using BusinessLogic.Interface;
using Entities;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;
using WebAPI.Models.Read.Discounts;
using WebAPI.Models.Write.Discounts;

namespace WebAPI.Controllers
{ 
    [ApiExceptionFilter]
    [ApiController]
    [Route("api/discounts/colors")]
    public class ColorDiscountController : ControllerBase
    {
        private IColorDiscountLogic ColorDiscountService;
        private IColorLogic ColorLogic;

        public ColorDiscountController(IColorDiscountLogic colorDiscountService, IColorLogic colorLogic) : base()
        {
            this.ColorDiscountService = colorDiscountService;
            this.ColorLogic = colorLogic;
        }
        [ProducesResponseType(typeof(ColorDiscountModelRead), 200)]
        [ProducesResponseType(401)]
        [HttpGet]
        public IActionResult Get()
        {
            var discounts = this.ColorDiscountService.Get();
            if (discounts.Count > 0)
            {
                return Ok(ColorDiscountModelRead.ToModel(discounts));
            }
            return NotFound("No hay descuentos en el sistema.");
        }
        [ProducesResponseType(typeof(ColorDiscountModelRead), 200)]
        [ProducesResponseType(401)]
        [HttpPut("{Id}")]
        [AuthenticationFilter("PUT/DISCOUNT")]
        public IActionResult Put(Guid id, [FromBody] ColorDiscountUpdateModelWrite discountModel)
        {
            ColorDiscount colorDiscount = discountModel.ToEntity();
            Color color = this.ColorLogic.Get(discountModel.ColorId);
            if (color == null)
            {
                return NotFound("No existe el color asociado al descuento con el Id: " + discountModel.ColorId);
            }
            else 
            {
                colorDiscount.Color = color;
                this.ColorDiscountService.Update(id, colorDiscount);
                return Ok("Se actualizo el descuento correctamente.");
            }
        }
        [ProducesResponseType(typeof(ColorDiscountModelRead), 201)]
        [ProducesResponseType(401)]
        [HttpPost()]
        [AuthenticationFilter("POST/DISCOUNT")]
        public IActionResult Post([FromBody] ColorDiscountModelWrite discountModel)
        {
            Color color = ColorLogic.Get(discountModel.ColorId);
            if (color == null)
            {
                return NotFound("No existe el color asociado al descuento con el Id: " + discountModel.ColorId);
            }
            else 
            {
                ColorDiscount colorDiscount = discountModel.ToEntity();
                colorDiscount.Color = color;
                var discount = this.ColorDiscountService.Create(colorDiscount);
                return CreatedAtRoute(null, ColorDiscountModelRead.ToModel(discount));
            }
        }
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpDelete("{Id}")]
        [AuthenticationFilter("DELETE/DISCOUNT")]
        public IActionResult Delete(Guid id)
        {
            var discount = this.ColorDiscountService.Get(id);
            if (discount == null)
            {
                return NotFound("No existe el descuento con Id: " + id);
            }
            Color color = ColorLogic.Get(discount.Color.Id);
            this.ColorDiscountService.Remove(discount);
            return Ok("Se elimino el descuento " + discount.Name);
        }
    }
}
