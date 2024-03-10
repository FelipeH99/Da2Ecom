using BusinessLogic.Interface;
using DataAccess.Interface;
using Entities;
using Exceptions;

namespace BusinessLogic
{
    public class PercentageDiscountLogic : IPercentageDiscountLogic
    {
        private IPercentageDiscountRepository PercentageDiscountRepository;
        public PercentageDiscountLogic(IPercentageDiscountRepository percentageDiscountrepository)
        {
            this.PercentageDiscountRepository = percentageDiscountrepository;
        }
        public ICollection<PercentageDiscount> Get()
        {
            return this.PercentageDiscountRepository.Get();
        }
        public PercentageDiscount Get(Guid id) 
        {
            return this.PercentageDiscountRepository.Get(id);
        }
        public PercentageDiscount Create(PercentageDiscount percentageDiscount) 
        {
            ValidatePercentageDiscount(percentageDiscount);
            ValidateRepeatedPercentageDiscount(percentageDiscount);
            SetActivated(percentageDiscount);
            this.PercentageDiscountRepository.Add(percentageDiscount);
            this.PercentageDiscountRepository.Save();
            return percentageDiscount;
        }
        public PercentageDiscount Update(Guid id, PercentageDiscount percentageDiscount)
        {
            PercentageDiscount percentageDiscountToChange = Get(id);
            if (percentageDiscountToChange == null) 
            { 
                throw new IncorrectRequestException("El descuento indicado no es correcto."); 
            }
            PercentageDiscountRepository.Update(percentageDiscountToChange, percentageDiscount);
            PercentageDiscountRepository.Save();
            return percentageDiscountToChange;
        }
        public void Remove(PercentageDiscount percentageDiscount)
        {
            PercentageDiscountRepository.Remove(percentageDiscount);
            PercentageDiscountRepository.Save();
        }

        private void ValidatePercentageDiscount(PercentageDiscount percentageDiscount)
        {
            ValidateNullPercentageDiscount(percentageDiscount);
            EmptyOrWhiteSpaceName(percentageDiscount.Name);
            ValidateConditionsForPromotion(percentageDiscount);

        }
        private void SetActivated(PercentageDiscount discount)
        {
            discount.IsActive = true;
        }
        private void ValidateConditionsForPromotion(PercentageDiscount percentageDiscount)
        {
            if (percentageDiscount.MinProductsNeededForDiscount <= 0)
            {
                throw new ArgumentException("Se debe indicar el minimo de productos para que la promocion sea aplicada.");
            }
            else if (percentageDiscount.PercentageDiscounted <= 0)
            {
                throw new ArgumentException("Se debe indicar el porcentage de descuento.");
            }
            else if (percentageDiscount.ProductToBeDiscounted == null)
            {
                throw new ArgumentException("Se debe indicar sobre que producto se debe aplicar el descuento.");
            }
        }
        private void ValidateRepeatedPercentageDiscount(PercentageDiscount percentageDiscount)
        {
            if (PercentageDiscountRepository.Exists(percentageDiscount))
            {
                throw new RepeatedObjectException("El descuento ya se enceuntra registrado.");
            }
        }
        private void ValidateNullPercentageDiscount(PercentageDiscount percentageDiscount)
        {
            if (percentageDiscount == null)
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
