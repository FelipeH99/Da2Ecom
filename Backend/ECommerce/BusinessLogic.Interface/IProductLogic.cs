using Entities;
namespace BusinessLogic.Interface
{
    public interface IProductLogic
    {
        ICollection<Product> Get();
        Product Get(Guid id);
        Product Create(Product oneProduct);
        Product Update(Guid id, Product oneProduct);
        void Remove(Product oneProduct);
        IEnumerable<SearchResult> CreateSearchResult(ProductInformation productInformation);
        SearchResult ConvertProductToSearchResult(Product oneProduct);
        IEnumerable<Product> GetByBrand(Guid brandId);
        IEnumerable<Product> GetProducts(string ids);
    }
}
