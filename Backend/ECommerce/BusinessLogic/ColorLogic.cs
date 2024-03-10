using BusinessLogic.Interface;
using DataAccess.Interface;
using Entities;

namespace BusinessLogic
{
    public class ColorLogic : IColorLogic
    {
        private IColorRepository ColorRepository;

        public ColorLogic(IColorRepository colorRepository)
        {
            this.ColorRepository = colorRepository;
        }

        public ICollection<Color> Get()
        {
            return ColorRepository.Get();
        }
        public Color Get(Guid id)
        {
            return ColorRepository.Get(id);
        }
        public Color GetByName(string name)
        {
            return ColorRepository.GetByName(name);
        }
    }
}
