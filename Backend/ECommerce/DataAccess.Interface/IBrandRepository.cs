using Entities;

namespace DataAccess.Interface
{
    public interface IBrandRepository
    {
        ICollection<Brand> Get();
        Brand Get(Guid Id);
        Brand GetByName(string name);
        void Add(Brand brand);
        bool Exists(Brand brand);
        void Save();
    }
}
