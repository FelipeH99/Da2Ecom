using Entities;

namespace DataAccess.Interface
{
    public interface IBrandDiscountRepository
    {
        ICollection<BrandDiscount> Get();
        BrandDiscount Get(Guid Id);
        void Add(BrandDiscount brandRepository);
        bool Exists(BrandDiscount brandRepository);
        void Save();
        void Update(BrandDiscount oldBrandDiscount, BrandDiscount newBrandDiscount);
        void Remove(BrandDiscount brandDiscount);
    }
}
