using DataAccess.Interface;
using Exceptions;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class BrandDiscountRepository : IBrandDiscountRepository
    {

        protected DbContext Context { get; set; }
        public BrandDiscountRepository(DbContext context)
        {
            this.Context = context;
        }
        public ICollection<BrandDiscount> Get()
        {
            return this.Context.Set<BrandDiscount>()
                .Include(bd => bd.Brand)
                .ToList();
        }
        public BrandDiscount Get(Guid Id) 
        {
            return this.Context.Set<BrandDiscount>()
                .Include(bd => bd.Brand)
                .FirstOrDefault(bd => bd.Id.Equals(Id));
        }
        public void Add(BrandDiscount brandDiscount) 
        {
            this.Context.Set<BrandDiscount>().Add(brandDiscount);
        }
        public bool Exists(BrandDiscount brandDiscount) 
        {
            return this.Context.Set<BrandDiscount>().Any(bd => bd.Name.Equals(brandDiscount.Name) &&
            bd.Brand.Equals(brandDiscount.Brand));
        }
        public void Save() 
        {
            this.Context.SaveChanges();
        }
        public void Update(BrandDiscount oldBrandDiscount, BrandDiscount newBrandDiscount)
        {
            UpdateAttributes(oldBrandDiscount, newBrandDiscount);
            this.Context.Entry(oldBrandDiscount).State = EntityState.Modified;
            this.Context.SaveChanges();
        }
        private void UpdateAttributes(BrandDiscount oldBrandDiscount, BrandDiscount newBrandDiscount)
        {
            oldBrandDiscount.Name = newBrandDiscount.Name;
            oldBrandDiscount.NumberOfProductsForFree = newBrandDiscount.NumberOfProductsForFree;
            oldBrandDiscount.MinProductsForPromotion = newBrandDiscount.MinProductsForPromotion;
            oldBrandDiscount.ProductToBeDiscounted = newBrandDiscount.ProductToBeDiscounted;
            oldBrandDiscount.Brand = newBrandDiscount.Brand;
            oldBrandDiscount.IsActive = newBrandDiscount.IsActive;
        }
        public void Remove(BrandDiscount brandDiscount)
        {
            if (this.Exists(brandDiscount))
            {
                this.Context.Set<BrandDiscount>().Remove(brandDiscount);
                this.Context.SaveChanges();
            }
            else
            {
                throw new IncorrectRequestException("No existe el BrandDiscount que desea eliminar.");
            }
        }
    }
}

