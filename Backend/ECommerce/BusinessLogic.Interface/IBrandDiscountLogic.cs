using Entities;

namespace BusinessLogic.Interface
{
    public interface IBrandDiscountLogic
    {
        ICollection<BrandDiscount> Get();
        BrandDiscount Get(Guid id);
        BrandDiscount Create(BrandDiscount brandDiscount);
        BrandDiscount Update(Guid id, BrandDiscount newBrandDiscount);
        void Remove(BrandDiscount brandDiscount);
    }
}
