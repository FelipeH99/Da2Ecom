using Entities;

namespace DataAccess.Interface
{
    public interface IColorDiscountRepository
    {
        ICollection<ColorDiscount> Get();
        ColorDiscount Get(Guid Id);
        void Add(ColorDiscount colorDiscount);
        bool Exists(ColorDiscount colorDiscount);
        void Save();
        void Update(ColorDiscount oldColorDiscount,ColorDiscount newColorDiscount);
        void Remove(ColorDiscount colorDiscount);
    }
}
