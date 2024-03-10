using DataAccess.Interface;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class ColorRepository : IColorRepository
    {
        protected DbContext Context { get; set; }
        public ColorRepository(DbContext context) 
        {
            this.Context = context;
        }
        public ICollection<Color> Get()
        {
            return this.Context.Set<Color>().ToList();
        }
        public Color Get(Guid id) 
        {
            return this.Context.Set<Color>().FirstOrDefault(c => c.Id.Equals(id));
        }
        public Color GetByName(string name) 
        {
            return this.Context.Set<Color>().FirstOrDefault(c => c.Name.Equals(name));
        }     
        public void Add(Color oneColor) 
        {
            this.Context.Set<Color>().Add(oneColor);
        }
        public bool Exists(Color color) 
        {
            return this.Context.Set<Color>().Any(c => c.Name.Equals(color.Name));
        }
        public void Save() 
        {
            this.Context.SaveChanges();
        }
    }
}
