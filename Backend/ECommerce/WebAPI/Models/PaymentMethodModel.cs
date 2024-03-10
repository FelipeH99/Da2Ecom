using Entities;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models.Read;

namespace WebAPI.Models
{
    public class PaymentMethodModel : ModelRead<PaymentMethod, PaymentMethodModel>
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public PaymentMethodModel() { }

        public override PaymentMethodModel SetModel(PaymentMethod entity)
        {
            this.Id = entity.Id.ToString();
            this.Name = entity.Name;
            return this;
        }
    }
}
