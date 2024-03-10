using Entities;
using DataAccess.Interface;
using BusinessLogic.Interface;
using Reflection;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogic
{
    public class DiscountLogic : IDiscountLogic
    {
        private IPercentageDiscountRepository PercentageDiscountRepository;
        private IQuantityDiscountRepository QuantityDiscountRepository;
        private IBrandDiscountRepository BrandDiscountRepository;
        private IColorDiscountRepository ColorDiscountRepository;

        public DiscountLogic(IPercentageDiscountRepository percentageDiscountRepository, IQuantityDiscountRepository quantityDiscountRepository,
            IBrandDiscountRepository brandDiscountRepository, IColorDiscountRepository colorDiscountRepository) 
        {
            this.PercentageDiscountRepository = percentageDiscountRepository;
            this.QuantityDiscountRepository = quantityDiscountRepository;
            this.BrandDiscountRepository = brandDiscountRepository;
            this.ColorDiscountRepository = colorDiscountRepository;
        }

        public (string name, double amountDiscounted) CalculateDiscount(string productsIds, IReflectionImplementation reflection, IProductLogic productLogic)
        {
            var products = new List<Product>();
            var splitedIds = productsIds.Split('*');
            foreach (String str in splitedIds) 
            {
                var product = productLogic.Get(Guid.Parse(str));
                if (product != null) 
                {
                    products.Add(product);
                }
            }
            return this.CalculateOptimumDiscount(products, reflection);
        }

        public (string name, double amountDiscounted) CalculateOptimumDiscount(List<Product> products
            , IReflectionImplementation reflection)
        {
            List<QuantityDiscount> quantityDiscounts = this.QuantityDiscountRepository.Get().Where(qd => qd.IsActive.Equals(true)).ToList();
            List<(string name, double amountDiscounted)> quantityDiscountTuples = quantityDiscounts.Select(qd => (qd.Name, qd.CalculateDiscount(products))).ToList();

            List<PercentageDiscount> percentageDiscounts = this.PercentageDiscountRepository.Get().Where(pd => pd.IsActive.Equals(true)).ToList();
            List<(string name, double amountDiscounted)> percentageDiscountTuples = percentageDiscounts.Select(pd => (pd.Name, pd.CalculateDiscount(products))).ToList();

            List<ColorDiscount> colorDiscounts = this.ColorDiscountRepository.Get().Where(cd => cd.IsActive.Equals(true)).ToList();
            List<(string name, double amountDiscounted)> colorDiscountTuples = colorDiscounts.Select(cd => (cd.Name, cd.CalculateDiscount(products))).ToList();

            List<BrandDiscount> brandDiscounts = this.BrandDiscountRepository.Get().Where(bd => bd.IsActive.Equals(true)).ToList();
            List<(string name, double amountDiscounted)> brandDiscountTuples = brandDiscounts.Select(bd => (bd.Name, bd.CalculateDiscount(products))).ToList();

            (string name, double discount) maximumBrandDiscountAmount = brandDiscountTuples.OrderByDescending(bd => bd.amountDiscounted).FirstOrDefault();
            (string name, double discount) maxmimumQuantityDiscountAmount = quantityDiscountTuples.OrderByDescending(qd => qd.amountDiscounted).FirstOrDefault();
            (string name, double discount) maximumColorDiscountAmount = colorDiscountTuples.OrderByDescending(cd => cd.amountDiscounted).FirstOrDefault();
            (string name, double discount) maximumPercentageDiscountAmount = percentageDiscountTuples.OrderByDescending(pd => pd.amountDiscounted).FirstOrDefault();

            List<(string name,double amountDiscounted)> finalDiscounts = new List<(string name, double discount)>
            {
                maximumBrandDiscountAmount,
                maxmimumQuantityDiscountAmount,
                maximumColorDiscountAmount,
                maximumPercentageDiscountAmount
            };

            var maxDataBaseDiscount = finalDiscounts.OrderByDescending(d => d.amountDiscounted).FirstOrDefault();

            var loadedDiscounts = reflection.LoadDll(products);
            if (!loadedDiscounts.IsNullOrEmpty())
            {
                var discountImported = loadedDiscounts.OrderByDescending(d => d.amountDiscounted).FirstOrDefault();
                return discountImported.amountDiscounted > maxDataBaseDiscount.amountDiscounted ? discountImported : maxDataBaseDiscount;
            }
            else 
            {
                return maxDataBaseDiscount;

            }
        }
    }
}
