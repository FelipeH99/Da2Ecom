using Entities;

namespace BusinessLogic.Interface
{
    public interface IBrandLogic
    {
        ICollection<Brand> Get();
        Brand Get(Guid id);
        Brand GetByName(string name);
    }
}
