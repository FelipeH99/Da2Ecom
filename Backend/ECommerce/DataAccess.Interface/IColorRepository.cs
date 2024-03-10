using Entities;

namespace DataAccess.Interface
{
    public interface IColorRepository
    {
        ICollection<Color> Get();
        Color Get(Guid id);
        void Add(Color color);
        bool Exists(Color color);
        void Save();
        Color GetByName(string name);
    }
}
