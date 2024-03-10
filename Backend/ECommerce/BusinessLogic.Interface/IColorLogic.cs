using Entities;
namespace BusinessLogic.Interface
{
    public interface IColorLogic
    {
        ICollection<Color> Get();
        Color Get(Guid id);
        Color GetByName(string name);
    }
}
