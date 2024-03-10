using Entities;

namespace BusinessLogic.Interface
{
    public interface IColorDiscountLogic
    {
        ICollection<ColorDiscount> Get();
        ColorDiscount Get(Guid id);
        ColorDiscount Create(ColorDiscount colorDiscount);
        ColorDiscount Update(Guid id, ColorDiscount colorDiscount);
        void Remove(ColorDiscount colorDiscount);
    }
}
