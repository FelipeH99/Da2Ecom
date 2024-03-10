using BusinessLogic.Interface;
using Entities;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;
using WebAPI.Models;
using WebAPI.Models.Read.Discounts;
using WebAPI.Models.Write.Discounts;

namespace WebAPI.Controllers
{
    [ApiExceptionFilter]
    [ApiController]
    [Route("api/discounts/brands")]
    public class BrandDiscountController : Controller
    {
        private IBrandDiscountLogic BrandDiscountService;
        private IBrandLogic BrandService;

        public BrandDiscountController(IBrandDiscountLogic brandDiscountService, IBrandLogic brandService) : base()
        {
            this.BrandDiscountService = brandDiscountService;
            this.BrandService = brandService;
        }
        [ProducesResponseType(typeof(BrandDiscountModelRead), 200)]
        [ProducesResponseType(401)]
        [HttpGet]
        public IActionResult Get()
        {
            var brandDiscounts = this.BrandDiscountService.Get();
            if (brandDiscounts.Count > 0)
            {
                return Ok(BrandDiscountModelRead.ToModel(brandDiscounts));
            }
            return NotFound("No hay descuentos en el sistema.");
        }
        [ProducesResponseType(typeof(BrandDiscountModelRead), 200)]
        [ProducesResponseType(401)]
        [HttpPut("{Id}")]
        [AuthenticationFilter("PUT/DISCOUNT")]
        public IActionResult Put(Guid id, [FromBody] BrandDiscountUpdateModelWrite discountModel)
        {
            BrandDiscount brandDiscount = discountModel.ToEntity();
            Brand brand = this.BrandService.Get(brandDiscount.Brand.Id);
            if (brand == null)
            {
                return NotFound("No existe la marca asociada al descuento con el Id: " + discountModel.BrandId);
            }
            else
            {
                brandDiscount.Brand = brand;
                this.BrandDiscountService.Update(id, brandDiscount);
                return Ok("Se actualizo el descuento correctamente.");
            }
        }
        [ProducesResponseType(typeof(BrandDiscountModelRead), 201)]
        [ProducesResponseType(401)]
        [HttpPost()]
        [AuthenticationFilter("POST/DISCOUNT")]
        public IActionResult Post([FromBody] BrandDiscountModelWrite discountModel)
        {
            Brand brand = BrandService.Get(discountModel.BrandId);
            if (brand == null)
            {
                return NotFound("No existe la marca asociada al descuento con el Id: " + discountModel.BrandId);
            }
            else
            {
                var discount = discountModel.ToEntity();
                discount.Brand = brand;
                var created = this.BrandDiscountService.Create(discount);
                return CreatedAtRoute(null, BrandDiscountModelRead.ToModel(created));
            }
        }
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [HttpDelete("{Id}")]
        [AuthenticationFilter("DELETE/DISCOUNT")]
        public IActionResult Delete(Guid id)
        {
            var discount = this.BrandDiscountService.Get(id);
            if (discount == null)
            {
                return NotFound("No existe el descuento con Id: " + id);
            }
            this.BrandDiscountService.Remove(discount);
            return Ok("Se elimino el descuento " + discount.Name);
        }
    }
}
