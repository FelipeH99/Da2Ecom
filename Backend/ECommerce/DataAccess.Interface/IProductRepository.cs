using Entities;

namespace DataAccess.Interface
{
    public interface IProductRepository
    {
        ICollection<Product> Get();
        Product Get(Guid id);
        void Add(Product product);
        void Save();
        bool Exists(Product product);
        void Update(Product oldProduct, Product newProduct);
        void Remove(Product product);
        ICollection<Product> GetByBrandId(Guid brandId);
    }
}
