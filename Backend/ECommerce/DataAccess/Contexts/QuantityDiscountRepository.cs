using DataAccess.Interface;
using Entities;
using Microsoft.EntityFrameworkCore;
using Exceptions;

namespace DataAccess.Contexts
{
    public class QuantityDiscountRepository : IQuantityDiscountRepository
    {
        protected DbContext Context { get; set; }
        public QuantityDiscountRepository(DbContext context)
        {
            this.Context = context;
        }
        public ICollection<QuantityDiscount> Get()
        {
            return this.Context.Set<QuantityDiscount>().ToList();
        }
        public QuantityDiscount Get(Guid Id)
        {
            return this.Context.Set<QuantityDiscount>().FirstOrDefault(qd => qd.Id.Equals(Id));
        }
        public void Add(QuantityDiscount quantityDiscount) 
        {
            this.Context.Set<QuantityDiscount>().Add(quantityDiscount);    
        }
        public void Update(QuantityDiscount oldQuantityDiscount, QuantityDiscount newQuantityDiscount) 
        {
            UpdateAttributes(oldQuantityDiscount,newQuantityDiscount);
            this.Context.Entry(oldQuantityDiscount).State = EntityState.Modified;
            this.Context.SaveChanges();
        }
        private void UpdateAttributes(QuantityDiscount oldQuantityDiscount, QuantityDiscount newQuantityDiscount) 
        {
            oldQuantityDiscount.Name = newQuantityDiscount.Name;
            oldQuantityDiscount.NumberOfProductsToBeFree = newQuantityDiscount.NumberOfProductsToBeFree;
            oldQuantityDiscount.MinProductsNeededForDiscount = newQuantityDiscount.MinProductsNeededForDiscount;
            oldQuantityDiscount.ProductToBeDiscounted = newQuantityDiscount.ProductToBeDiscounted;
            oldQuantityDiscount.ProductCategory = newQuantityDiscount.ProductCategory;
            oldQuantityDiscount.IsActive = newQuantityDiscount.IsActive;
        }
        public bool Exists(QuantityDiscount quantitiyDiscount) 
        {
            return this.Context.Set<QuantityDiscount>().Any(qd => qd.Name.Equals(quantitiyDiscount.Name) 
            && qd.ProductCategory.Equals(quantitiyDiscount.ProductCategory));
        }
        public void Save()
        {
            this.Context.SaveChanges();
        }
        public void Remove(QuantityDiscount quantityDiscount) 
        {
            if (this.Exists(quantityDiscount))
            {
                this.Context.Set<QuantityDiscount>().Remove(quantityDiscount);
                this.Context.SaveChanges();
            }
            else
            {
                throw new IncorrectRequestException("No existe el QuantityDiscount que desea eliminar.");
            }
        }
    }
}
