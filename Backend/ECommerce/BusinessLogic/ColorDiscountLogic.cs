using BusinessLogic.Interface;
using DataAccess.Interface;
using Entities;
using Exceptions;

namespace BusinessLogic
{
    public class ColorDiscountLogic : IColorDiscountLogic
    {
        private IColorDiscountRepository ColorDiscountRepository;
        private IColorRepository ColorRepository;
        public ColorDiscountLogic(IColorDiscountRepository colorDiscountRepository, IColorRepository colorRepository)
        {
            this.ColorDiscountRepository = colorDiscountRepository;
            this.ColorRepository = colorRepository;
        }
        public ICollection<ColorDiscount> Get()
        {
            return this.ColorDiscountRepository.Get();
        }
        public ColorDiscount Get(Guid id) 
        {
            return this.ColorDiscountRepository.Get(id);
        }
        public ColorDiscount Create(ColorDiscount colorDiscount)
        {
            ValidateColorDiscount(colorDiscount);
            ValidateRepeatedColorDiscount(colorDiscount);
            SetActivated(colorDiscount);
            this.ColorDiscountRepository.Add(colorDiscount);
            this.ColorDiscountRepository.Save();
            return colorDiscount;
        }
        public ColorDiscount Update(Guid id, ColorDiscount colorDiscount)
        {
            ColorDiscount colorDiscountToChange = this.ColorDiscountRepository.Get(id);
            if (colorDiscountToChange == null) 
            {
                throw new IncorrectRequestException("El descuento indicado no es correcto.");
            }
            ColorDiscountRepository.Update(colorDiscountToChange, colorDiscount);
            ColorDiscountRepository.Save();
            return colorDiscountToChange;
        }
        public void Remove(ColorDiscount colorDiscount)
        {
            ColorDiscountRepository.Remove(colorDiscount);
            ColorDiscountRepository.Save();
        }
        private void SetActivated(ColorDiscount discount)
        {
            discount.IsActive = true;
        }
        private void ValidateColorDiscount(ColorDiscount colorDiscount)
        {
            ValidateNullColorDiscount(colorDiscount);
            EmptyOrWhiteSpaceName(colorDiscount.Name);
            ValidateConditionsForPromotion(colorDiscount);
            ValidateColor(colorDiscount.Color);

        }
        private void ValidateConditionsForPromotion(ColorDiscount colorDiscount)
        {
            if (colorDiscount.MinProductsNeededForDiscount <= 0)
            {
                throw new ArgumentException("Se debe indicar el minimo de productos para que la promocion sea aplicada.");
            }
            else if (colorDiscount.PercentageDiscount <= 0)
            {
                throw new ArgumentException("Se debe indicar el porcentage de descuento.");
            }
            else if (colorDiscount.ProductToBeDiscounted == null)
            {
                throw new ArgumentException("Se debe indicar sobre que producto se debe aplicar el descuento.");
            }
        }
        private void ValidateColor(Color oneColor)
        {
            if (oneColor == null)
            {
                throw new IncorrectColorForDiscount("El color para el descuento no puede ser vacio.");
            }
            else if (!this.ColorRepository.Exists(oneColor))
            {
                throw new IncorrectColorForDiscount("El color para el descuento no se encuentra registrada en el sistema.");
            }
        }
        private void ValidateRepeatedColorDiscount(ColorDiscount colorDiscount)
        {
            if (ColorDiscountRepository.Exists(colorDiscount))
            {
                throw new RepeatedObjectException("El descuento ya se enceuntra registrado.");
            }
        }
        private void ValidateNullColorDiscount(ColorDiscount colorDiscount)
        {
            if (colorDiscount == null)
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
