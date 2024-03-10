using DataAccess.Interface;
using Entities;
using Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class PercentageDiscountRepository : IPercentageDiscountRepository
    {
        protected DbContext Context { get; set; }
        public PercentageDiscountRepository(DbContext context)
        {
            this.Context = context;
        }
        public ICollection<PercentageDiscount> Get()
        {
            return this.Context.Set<PercentageDiscount>().ToList();
        }
        public PercentageDiscount Get(Guid Id) 
        {
            return this.Context.Set<PercentageDiscount>().FirstOrDefault(pd => pd.Id.Equals(Id) && pd.IsActive.Equals(true));
        }
        public void Add(PercentageDiscount percentageDiscount) 
        {
            this.Context.Set<PercentageDiscount>().Add(percentageDiscount);
        }
        public bool Exists(PercentageDiscount percentageDiscount) 
        {
            return this.Context.Set<PercentageDiscount>().Any(pd => pd.Name.Equals(percentageDiscount.Name));
        }
        public void Save() 
        {
            this.Context.SaveChanges();
        }
        public void Update(PercentageDiscount oldPercentageDiscount, PercentageDiscount newPercentageDiscount)
        {
            UpdateAttributes(oldPercentageDiscount, newPercentageDiscount);
            this.Context.Entry(oldPercentageDiscount).State = EntityState.Modified;
            this.Context.SaveChanges();
        }
        private void UpdateAttributes(PercentageDiscount oldPercentageDiscount, PercentageDiscount newPercentageDiscount)
        {
            oldPercentageDiscount.Name = newPercentageDiscount.Name;
            oldPercentageDiscount.PercentageDiscounted = newPercentageDiscount.PercentageDiscounted;
            oldPercentageDiscount.MinProductsNeededForDiscount = newPercentageDiscount.MinProductsNeededForDiscount;
            oldPercentageDiscount.ProductToBeDiscounted = newPercentageDiscount.ProductToBeDiscounted;
            oldPercentageDiscount.IsActive = newPercentageDiscount.IsActive;
        }
        public void Remove(PercentageDiscount percentageDiscount)
        {
            if (this.Exists(percentageDiscount))
            {
                this.Context.Set<PercentageDiscount>().Remove(percentageDiscount);
                this.Context.SaveChanges();
            }
            else
            {
                throw new IncorrectRequestException("No existe el PercentageDiscount que desea eliminar.");
            }
        }
    }
}

