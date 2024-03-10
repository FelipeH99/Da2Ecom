using Entities;

namespace Reflection
{
    public interface IReflectionImplementation
    {
        List<(string name, double amountDiscounted)> LoadDll(List<Product> products);
    }
}
