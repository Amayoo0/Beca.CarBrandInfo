using Beca.CarBrandInfo.API.Entities;

namespace Beca.CarBrandInfo.API.Services
{
    public interface IBrandInfoRepository
    {
        Task<IEnumerable<Brand>> GetBrandsAsync();
        Task<(IEnumerable<Brand>, PaginationMetadata)> GetBrandsAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<Brand?> GetBrandAsync(int BrandId, bool includeModels);
        Task<bool> BrandExistsAsync(int BrandId);
        Task<IEnumerable<Model>> GetModelsForBrandAsync(int BrandId);
        Task<Model?> GetModelForBrandAsync(int BrandId,
            int modelId);
        Task AddModelForBrandAsync(int BrandId, Model model);
        void DeleteModel(Model model);
        Task<bool> BrandNameMatchesBrandId(string? BrandName, int BrandId);
        Task<bool> SaveChangesAsync();
    }
}
