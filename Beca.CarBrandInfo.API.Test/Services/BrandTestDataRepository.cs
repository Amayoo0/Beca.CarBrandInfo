using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beca.CarBrandInfo.API.Entities;
using Beca.CarBrandInfo.API.Services;

namespace Beca.CarBrandInfo.API.Test.Services
{
    public class BrandTestDataRepository : IBrandInfoRepository
    {
        private List<Brand> _brands;
        private List<Model> _models;

        public BrandTestDataRepository()
        {
            // mimic expensive creation process
            // Thread.Sleep(3000);

            _brands = new List<Brand>
            {
                new Brand("Ford")
                {
                    Id = 1,
                    Models = new List<Model>
                    {
                        new Model("Fiesta")
                        {
                            Id = 1,
                            Description = "A super car"
                        }
                    }
                }
            };
        }


        public Task AddModelForBrandAsync(int BrandId, Model model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> BrandExistsAsync(int BrandId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> BrandNameMatchesBrandId(string? BrandName, int BrandId)
        {
            throw new NotImplementedException();
        }

        public void DeleteModel(Model model)
        {
            throw new NotImplementedException();
        }

        public Brand? GetBrand(int id)
        {
            return _brands.FirstOrDefault(c => c.Id == id);
        }
        //Not used
        public List<Brand> GetBrands(int[] BrandsId)
        {
            List<Brand> brandToReturn = new();
            foreach (var id in BrandsId)
            {
                var brand = GetBrand(id);
                if(brand != null)
                {
                    brandToReturn.Add(brand);
                }
            }
            return brandToReturn;
        }

        public Task<Brand?> GetBrandAsync(int BrandId, bool includeModels)
        {
            return Task.FromResult(GetBrand(BrandId));
        }

        public Task<IEnumerable<Brand>> GetBrandsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<(IEnumerable<Brand>, PaginationMetadata)> GetBrandsAsync(string? name, string? searchQuery, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Model?> GetModelForBrandAsync(int BrandId, int modelId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Model>> GetModelsForBrandAsync(int BrandId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
