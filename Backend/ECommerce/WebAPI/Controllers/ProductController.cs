using Microsoft.AspNetCore.Mvc;
using Entities;
using BusinessLogic.Interface;
using WebAPI.Models.Read;
using WebAPI.Filters;
using WebAPI.Models;
using WebAPI.Models.Write;
using Microsoft.AspNetCore.Cors;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Controllers
{
    [ApiExceptionFilter]
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private IProductLogic ProductService;
        public ProductController(IProductLogic productService) : base()
        {
            this.ProductService = productService;
        }
        [ProducesResponseType(typeof(ProductModelRead), 200)]
        [HttpGet]
        public IActionResult Get([FromQuery] ProductInformation productInfo)
        {
            if (productInfo.productsIds.IsNullOrEmpty())
            {
                var mySearchResult = ProductService.CreateSearchResult(productInfo);
                return Ok(ProductModelRead.ToModel(mySearchResult));

            }
            else 
            {
                var products = this.ProductService.GetProducts(productInfo.productsIds);
                if (products == null)
                {
                    return NotFound("No hay productos que mostrar");
                }
                return Ok(ProductModel.ToModel(products));
            }


        }
        [ProducesResponseType(typeof(ProductModelRead), 200)]
        [ProducesResponseType(401)]
        [HttpGet("{Id}")]
        public IActionResult Get(Guid id)
        {
            var product = this.ProductService.Get(id);
            if (product == null)
            {
                return NotFound("No existe el producto con Id: " + id);
            }
            return Ok(ProductModel.ToModel(product));
        }
        [ProducesResponseType(typeof(ProductModel), 200)]
        [ProducesResponseType(401)]
        [AuthenticationFilter("PUT/PRODUCT")]
        [HttpPut("{Id}")]
        public IActionResult Put(Guid id, [FromBody] ProductModelWrite productModel)
        {
            this.ProductService.Update(id, productModel.ToEntity());
            return Ok("Se actualizo el producto correctamente.");

        }
        [ProducesResponseType(typeof(ProductModelRead), 201)]
        [ProducesResponseType(401)]
        [AuthenticationFilter("POST/PRODUCT")]
        [HttpPost()]
        public IActionResult Post([FromBody] ProductModelWrite productModel)
        {
            var product = this.ProductService.Create(productModel.ToEntity());
            return CreatedAtRoute(null, ProductModelCreatedRead.ToModel(product));
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [AuthenticationFilter("DELETE/PRODUCT")]
        [HttpDelete("{Id}")]
        public IActionResult Delete(Guid id)
        {
            var product = this.ProductService.Get(id);
            if (product == null)
            {
                return NotFound("No existe el producto con Id: " + id);
            }
            this.ProductService.Remove(product);
            return Ok("Se elimino el producto " + product.Name);
        }
    }
}
