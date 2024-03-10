using Microsoft.AspNetCore.Mvc;
using Entities;
using BusinessLogic.Interface;
using WebAPI.Models.Read;
using WebAPI.Filters;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiExceptionFilter]
    [ApiController]
    [Route("api/brands")]
    public class BrandController : ControllerBase
    {
        private IBrandLogic BrandService;
        public BrandController(IBrandLogic brandService) : base()
        {
            this.BrandService = brandService;
        }
        [ProducesResponseType(typeof(BrandModel), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            var brands = this.BrandService.Get();
            return Ok(BrandModel.ToModel(brands));

        }
        [ProducesResponseType(typeof(BrandModel), 200)]
        [ProducesResponseType(401)]
        [HttpGet("{Id}")]
        public IActionResult Get(Guid id)
        {
            var brand = this.BrandService.Get(id);
            if (brand == null)
            {
                return NotFound("No existe la marca con Id: " + id);
            }
            return Ok(BrandModel.ToModel(brand));
        }
    }
}
