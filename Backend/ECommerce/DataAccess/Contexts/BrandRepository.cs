using DataAccess.Interface;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class BrandRepository : IBrandRepository
    {
        protected DbContext Context { get; set; }
        public BrandRepository(DbContext context)
        {
            this.Context = context;
        }
        public ICollection<Brand> Get()
        {
            return this.Context.Set<Brand>().ToList();
        }
        public Brand Get(Guid Id) 
        {
            return this.Context.Set<Brand>().FirstOrDefault(b => b.Id.Equals(Id));
        }
        public Brand GetByName(string name) 
        {
            return this.Context.Set<Brand>().FirstOrDefault(b => b.Name.Equals(name));
        }
        public void Add(Brand brand) 
        {
            this.Context.Set<Brand>().Add(brand);
        }
        public bool Exists(Brand brand) 
        {
            return this.Context.Set<Brand>().Any(b => b.Id.Equals(brand.Id));
        }
        public void Save() 
        {
            this.Context.SaveChanges();
        }
    }
}
