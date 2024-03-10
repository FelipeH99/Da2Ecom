using Entities;
using Microsoft.EntityFrameworkCore;
using DataAccess.Interface;
using Exceptions;

namespace DataAccess.Contexts
{
    public class ColorDiscountRepository : IColorDiscountRepository
    {
        protected DbContext Context { get; set; }
        public ColorDiscountRepository(DbContext context)
        {
            this.Context = context;
        }
        public ICollection<ColorDiscount> Get()
        {
            return this.Context.Set<ColorDiscount>()
                .Include(cd => cd.Color)
                .ToList();
        }
        public ColorDiscount Get(Guid id) 
        {
            return this.Context.Set<ColorDiscount>()
                 .Include(cd => cd.Color)
                .FirstOrDefault(cd => cd.Id.Equals(id));
        }
        public void Add(ColorDiscount colorDiscount) 
        {
            this.Context.Set<ColorDiscount>().Add(colorDiscount);
        }
        public bool Exists(ColorDiscount colorDiscount) 
        {
            return this.Context.Set<ColorDiscount>().Any(cd => cd.Name.Equals(colorDiscount.Name) &&
            cd.Color.Equals(colorDiscount.Color));
        }
        public void Save() 
        {
            this.Context.SaveChanges();
        }
        public void Update(ColorDiscount oldColorDiscount, ColorDiscount newColorDiscount) 
        {
            UpdateAttributes(oldColorDiscount, newColorDiscount);
            this.Context.Entry(oldColorDiscount).State = EntityState.Modified;
            this.Context.SaveChanges();
        }
        private void UpdateAttributes(ColorDiscount oldColorDiscount, ColorDiscount newColorDiscount)
        {
            oldColorDiscount.Name = newColorDiscount.Name;
            oldColorDiscount.Color = newColorDiscount.Color;
            oldColorDiscount.MinProductsNeededForDiscount = newColorDiscount.MinProductsNeededForDiscount;
            oldColorDiscount.PercentageDiscount = newColorDiscount.PercentageDiscount;
            oldColorDiscount.ProductToBeDiscounted = newColorDiscount.ProductToBeDiscounted;
            oldColorDiscount.IsActive = newColorDiscount.IsActive;
        }
        public void Remove(ColorDiscount colorDiscount) 
        {
            if (this.Exists(colorDiscount))
            {
                this.Context.Set<ColorDiscount>().Remove(colorDiscount);
                this.Context.SaveChanges();
            }
            else
            {
                throw new IncorrectRequestException("No existe el ColorDiscount que desea eliminar.");
            }
        }
    }
}
