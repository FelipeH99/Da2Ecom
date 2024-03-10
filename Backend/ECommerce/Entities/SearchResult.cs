using Entities.Enums;

namespace Entities
{
    public class SearchResult
    {
        public string BrandName { get; set; }
        public string ProductName { get; set; }
        public ProductCategory Category { get; set; }
        public double Price { get; set; }
        public Guid ProductId { get; set; }
        public string Image { get; set; }


        public SearchResult()
        {
        }

        public SearchResult(Product product)
        {
            BrandName = product.Brand.Name;
            ProductName = product.Name;
            Price = product.Price;
            Category = product.ProductCategory;
            ProductId = product.Id;
            Image = product.ImageURL;
        }

        public override bool Equals(object obj)
        {
            bool equals = false;
            if (IsNotNull(obj) && IsSameTypeAsThis(obj))
            {
                SearchResult oneSearchResult = (SearchResult)obj;
                equals = this.ProductName.Equals(oneSearchResult.ProductName);
            }
            return equals;
        }

        public bool IsSameTypeAsThis(object obj)
        {
            return obj.GetType().Equals(this.GetType());
        }

        public bool IsNotNull(object obj)
        {
            return obj != null;
        }

    }
}
