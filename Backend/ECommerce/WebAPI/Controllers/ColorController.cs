using BusinessLogic.Interface;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiExceptionFilter]
    [ApiController]
    [Route("api/colors")]
    public class ColorController : ControllerBase
    {
        private IColorLogic ColorService;
        public ColorController(IColorLogic colorService) : base()
        {
            this.ColorService = colorService;
        }
        [ProducesResponseType(typeof(ColorModel), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            var colors = this.ColorService.Get();
            return Ok(ColorModel.ToModel(colors));

        }
        [ProducesResponseType(typeof(ColorModel), 200)]
        [ProducesResponseType(401)]
        [HttpGet("{Id}")]
        public IActionResult Get(Guid id)
        {
            var color = this.ColorService.Get(id);
            if (color == null)
            {
                return NotFound("No existe el color con Id: " + id);
            }
            return Ok(ColorModel.ToModel(color));
        }
    }
}
