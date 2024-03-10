using Entities;
namespace BusinessLogic.Interface
{
    public interface IPercentageDiscountLogic
    {
        ICollection<PercentageDiscount> Get();
        PercentageDiscount Get(Guid id);
        PercentageDiscount Create(PercentageDiscount percentageDiscount);
        PercentageDiscount Update(Guid id, PercentageDiscount percentageDiscount);
        void Remove(PercentageDiscount percentageDiscount); 
    }
}
