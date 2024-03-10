using BusinessLogic.Interface;
using DataAccess.Interface;
using Entities;
using Exceptions;

namespace BusinessLogic
{
    public class BrandDiscountLogic : IBrandDiscountLogic
    {
        private IBrandDiscountRepository BrandDiscountRepository;
        private IBrandRepository BrandRepository;
        public BrandDiscountLogic(IBrandDiscountRepository brandDiscountRepository, IBrandRepository brandRepository)
        {
            this.BrandDiscountRepository = brandDiscountRepository;
            this.BrandRepository = brandRepository;
        }
        public ICollection<BrandDiscount> Get()
        {
            return this.BrandDiscountRepository.Get();
        }
        public BrandDiscount Get(Guid id) 
        {
            return this.BrandDiscountRepository.Get(id);
        }
        public BrandDiscount Create(BrandDiscount brandDiscount)
        {
            ValidateBrandDiscount(brandDiscount);
            ValidateRepeatedBrandDiscount(brandDiscount);
            SetActivated(brandDiscount);
            this.BrandDiscountRepository.Add(brandDiscount);
            this.BrandDiscountRepository.Save();
            return brandDiscount;
        }
        public BrandDiscount Update(Guid id, BrandDiscount brandDiscount)
        {
            BrandDiscount brandDiscountToChange = this.BrandDiscountRepository.Get(id);
            if (brandDiscountToChange == null)
            {
                throw new IncorrectRequestException("El descuento indicado no es correcto.");
            }
            BrandDiscountRepository.Update(brandDiscountToChange, brandDiscount);
            BrandDiscountRepository.Save();
            return brandDiscount;
        }
        public void Remove(BrandDiscount brandDiscount)
        {
            BrandDiscountRepository.Remove(brandDiscount);
            BrandDiscountRepository.Save();
        }
        private void ValidateBrandDiscount(BrandDiscount brandDiscount)
        {
            ValidateNullBrandDiscount(brandDiscount);
            EmptyOrWhiteSpaceName(brandDiscount.Name);
            ValidateConditionsForPromotion(brandDiscount);
            ValidateBrand(brandDiscount.Brand);
        }
        private void SetActivated(BrandDiscount discount)
        {
            discount.IsActive = true;
        }
        private void ValidateConditionsForPromotion(BrandDiscount brandDiscount) 
        {
            if (brandDiscount.MinProductsForPromotion <= 0)
            {
                throw new ArgumentException("Se debe indicar el minimo de productos para que la promocion sea aplicada.");
            } else if (brandDiscount.NumberOfProductsForFree <= 0)
            {
                throw new ArgumentException("Se debe indicar cuantos productos deben ser descontados.");
            } else if (brandDiscount.ProductToBeDiscounted == null)
            {
                throw new ArgumentException("Se debe indicar sobre que producto se debe aplicar el descuento.");
            }
        }
        private void ValidateBrand(Brand oneBrand)
        {
            if (oneBrand == null)
            {
                throw new IncorrectBrandForDiscount("La Marca del descuento no puede ser vacia.");
            }
            else if (!this.BrandRepository.Exists(oneBrand))
            {
                throw new IncorrectBrandForDiscount("La Marca del descuento no se encuentra registrada en el sistema.");
            }
        }
        private void ValidateRepeatedBrandDiscount(BrandDiscount brandDiscount)
        {
            if (BrandDiscountRepository.Exists(brandDiscount))
            {
                throw new RepeatedObjectException("El descuento ya se enceuntra registrado.");
            }
        }
        private void ValidateNullBrandDiscount(BrandDiscount brandDiscount)
        {
            if (brandDiscount == null)
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
