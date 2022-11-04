using Beca.CarBrandInfo.API.DbContexts;
using Beca.CarBrandInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Beca.CarBrandInfo.API.Services
{
    public class BrandInfoRepository: IBrandInfoRepository
    {
        private readonly BrandInfoContext _context;

        public BrandInfoRepository(BrandInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Brand>> GetBrandsAsync()
        {
            return await _context.Brands.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<bool> Brand(string? brandName, int brandId)
        {
            return await _context.Brands.AnyAsync(c => c.Id == brandId && c.Name == brandName);
        }

        public async Task<(IEnumerable<Brand>, PaginationMetadata)> GetBrandsAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize)
        {
            // collection to start from
            var collection = _context.Brands as IQueryable<Brand>;

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.Name == name);
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(c => c.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }



        public async Task<Brand?> GetBrandAsync(int brandId, bool includeModels)
        {
            if (includeModels)
            {
                return await _context.Brands.Include(c => c.Models)
                    .Where(c => c.Id == brandId).FirstOrDefaultAsync();
            }

            return await _context.Brands
                  .Where(c => c.Id == brandId).FirstOrDefaultAsync();
        }

        public async Task<bool> BrandExistsAsync(int brandId)
        {
            return await _context.Brands.AnyAsync(c => c.Id == brandId);
        }

        public async Task<Model?> GetModelForBrandAsync(
            int brandId,
            int modelId)
        {
            return await _context.Models
               .Where(p => p.BrandId == brandId && p.Id == modelId)
               .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Model>> GetModelsForBrandAsync(int brandId)
        {
            return await _context.Models
                           .Where(p => p.BrandId == brandId).ToListAsync();
        }

        public async Task AddModelForBrandAsync(int brandId,
            Model model)
        {
            var brand = await GetBrandAsync(brandId, false);
            if (brand != null)
            {
                brand.Models.Add(model);
            }
        }

        public void DeleteModel(Model model)
        {
            _context.Models.Remove(model);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public Task<Brand?> GetBrandsAsync(int BrandId, bool includeModels)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Model>> GetModelForBrandAsync(int BrandId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> BrandNameMatchesBrandId(string? BrandName, int BrandId)
        {
            throw new NotImplementedException();
        }
    }
}
