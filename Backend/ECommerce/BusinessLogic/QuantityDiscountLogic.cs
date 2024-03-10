using BusinessLogic.Interface;
using DataAccess.Interface;
using Entities;
using Exceptions;
namespace BusinessLogic
{
    public class QuantityDiscountLogic : IQuantityDiscountLogic
    {
        private IQuantityDiscountRepository QuantityDiscountRepository;
        public QuantityDiscountLogic(IQuantityDiscountRepository quantityDiscountRepository)
        {
            this.QuantityDiscountRepository = quantityDiscountRepository;
        }
        public ICollection<QuantityDiscount> Get()
        {
            return this.QuantityDiscountRepository.Get();
        }
        public QuantityDiscount Get(Guid id) 
        {
            return this.QuantityDiscountRepository.Get(id);
        }
        public QuantityDiscount Create(QuantityDiscount quantityDiscount)
        {
            ValidateQuantityDiscount(quantityDiscount);
            ValidateRepeatedQuantityDiscount(quantityDiscount);
            SetActivated(quantityDiscount);
            this.QuantityDiscountRepository.Add(quantityDiscount);
            this.QuantityDiscountRepository.Save();
            return quantityDiscount;
        }
        public QuantityDiscount Update(Guid id, QuantityDiscount quantityDiscount)
        {
            QuantityDiscount quantityDiscountToChange = this.QuantityDiscountRepository.Get(id);
            if (quantityDiscountToChange == null)
            {
                throw new IncorrectRequestException("El descuento indicado no es correcto.");
            }
            QuantityDiscountRepository.Update(quantityDiscountToChange, quantityDiscount);
            QuantityDiscountRepository.Save();
            return quantityDiscountToChange;
        }
        public void Remove(QuantityDiscount quantityDiscount)
        {
            QuantityDiscountRepository.Remove(quantityDiscount);
            QuantityDiscountRepository.Save();
        }
        private void SetActivated(QuantityDiscount discount) 
        {
            discount.IsActive = true;
        }
        private void ValidateQuantityDiscount(QuantityDiscount quantityDiscount)
        {
            ValidateNullQuantityDiscount(quantityDiscount);
            EmptyOrWhiteSpaceName(quantityDiscount.Name);
            ValidateConditionsForPromotion(quantityDiscount);

        }
        private void ValidateConditionsForPromotion(QuantityDiscount quantityDiscount)
        {
            if (quantityDiscount.MinProductsNeededForDiscount <= 0)
            {
                throw new ArgumentException("Se debe indicar el minimo de productos para que la promocion sea aplicada.");
            }
            else if (quantityDiscount.NumberOfProductsToBeFree <= 0)
            {
                throw new ArgumentException("Se debe indicar cuantos productos deben ser descontados.");
            }
            else if (quantityDiscount.ProductToBeDiscounted == null)
            {
                throw new ArgumentException("Se debe indicar sobre que producto se debe aplicar el descuento.");
            }
        }
        private void ValidateRepeatedQuantityDiscount(QuantityDiscount quantityDiscount)
        {
            if (QuantityDiscountRepository.Exists(quantityDiscount))
            {
                throw new RepeatedObjectException("El descuento ya se enceuntra registrado.");
            }
        }
        private void ValidateNullQuantityDiscount(QuantityDiscount quantityDiscount)
        {
            if (quantityDiscount == null)
            {
                throw new InvalidDiscountException("El Descuento es nulo.");
            }
        }
        private void EmptyOrWhiteSpaceName(String name)
        {
            if (VerifyEmptyOrWhiteSpaceString(name))
            {
                throw new IncorrectNameException("Nombre del descuento no puede ser vacīo.");
            }
        }
        private bool VerifyEmptyOrWhiteSpaceString(String str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}
