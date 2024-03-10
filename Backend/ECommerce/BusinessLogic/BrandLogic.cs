using BusinessLogic.Interface;
using DataAccess.Interface;
using Entities;

namespace BusinessLogic
{
    public class BrandLogic : IBrandLogic
    {
        private IBrandRepository BrandRepository;

        public BrandLogic(IBrandRepository brandRepository)
        {
            this.BrandRepository = brandRepository;
        }

        public ICollection<Brand> Get()
        {
            return BrandRepository.Get();
        }
        public Brand Get(Guid id)
        {
            return BrandRepository.Get(id);
        }
        public Brand GetByName(string name) 
        {
            return this.BrandRepository.GetByName(name);
        }
    }
}
