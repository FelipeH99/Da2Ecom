using BusinessLogic.Interface;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiExceptionFilter]
    [ApiController]
    [Route("api/payments")]
    public class PaymentMethodController : ControllerBase
    {
        private IPaymentMethodLogic PaymentMethodService;
        public PaymentMethodController(IPaymentMethodLogic paymentMethodService) : base()
        {
            this.PaymentMethodService = paymentMethodService;
        }
        [ProducesResponseType(typeof(PaymentMethodModel), 200)]
        [HttpGet]
        public IActionResult Get()
        {
            var payments = this.PaymentMethodService.Get();
            return Ok(PaymentMethodModel.ToModel(payments));

        }
    }
}
