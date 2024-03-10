using Reflection;
using Entities;

namespace BusinessLogic.Interface
{
    public interface IDiscountLogic
    {
        (string name,double amountDiscounted) CalculateOptimumDiscount(List<Product> products, IReflectionImplementation reflection);
        (string name, double amountDiscounted) CalculateDiscount(string productsIds,IReflectionImplementation reflection, IProductLogic productLogic);
    }
}
