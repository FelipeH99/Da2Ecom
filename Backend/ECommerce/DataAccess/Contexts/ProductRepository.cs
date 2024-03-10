using DataAccess.Interface;
using Entities;
using Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class ProductRepository : IProductRepository
    {
        protected DbContext Context { get; set; }
        public ProductRepository(DbContext context)
        {
            this.Context = context;
        }
        public ICollection<Product> Get()
        {
            return this.Context.Set<Product>()
                .Include(p => p.Brand)
                .Include(p => p.Colors)
                .Where(p => p.Stock >= 1)
                .ToList();
        }
        public Product Get(Guid id) 
        {
            return this.Context.Set<Product>()
                .Include(p => p.Brand)
                .Include(p => p.Colors)
                .FirstOrDefault(p => p.Id == id && p.Stock >= 1);
        }
        public void Add(Product product) 
        {
            this.Context.Set<Product>().Add(product);
        }
        public void Save() 
        {
            this.Context.SaveChanges();
        }
        public bool Exists(Product product) 
        {
            return this.Context.Set<Product>().Any(p => p.Name.Equals(product.Name) &&
            p.Brand.Equals(product.Brand) && p.Colors.All(pc => product.Colors.Contains(pc)));
        }
        public void Update(Product oldProduct, Product newProduct) 
        {
            UpdateAttributes(oldProduct, newProduct);
            this.Context.Entry(oldProduct).State = EntityState.Modified;
            this.Context.SaveChanges();
        }
        private void UpdateAttributes(Product oldProduct, Product newProduct)
        {
            oldProduct.Name = newProduct.Name;
            oldProduct.Price = newProduct.Price;
            oldProduct.Description = newProduct.Description;
            oldProduct.Brand = newProduct.Brand;
            oldProduct.Colors = newProduct.Colors;
            oldProduct.ProductCategory = newProduct.ProductCategory;
            oldProduct.Stock = newProduct.Stock;
        }
        public void Remove(Product product)
        {
            if (this.Exists(product))
            {
                this.Context.Set<Product>().Remove(product);
                this.Context.SaveChanges();
            }
            else
            {
                throw new IncorrectRequestException("No existe el Producto que desea eliminar.");
            }
        }
        public ICollection<Product> GetByBrandId(Guid brandId) 
        {
            return this.Context.Set<Product>().Where(p => p.Brand.Id.Equals(brandId))
                .Include(p => p.Brand)
                .Include(p => p.Colors)
                 .ToList();
        }
    }
}
