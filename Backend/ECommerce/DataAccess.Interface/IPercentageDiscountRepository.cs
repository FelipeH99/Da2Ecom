using Entities;

namespace DataAccess.Interface
{
    public interface IPercentageDiscountRepository
    {
        ICollection<PercentageDiscount> Get();
        PercentageDiscount Get(Guid Id);
        void Add(PercentageDiscount percentageDiscount);
        bool Exists(PercentageDiscount percentageDiscount);
        void Save();
        void Update(PercentageDiscount oldPercentageDiscount, PercentageDiscount newPercentageDiscount);
        void Remove(PercentageDiscount percentageDiscount);

    }
}
