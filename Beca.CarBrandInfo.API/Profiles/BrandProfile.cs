using AutoMapper;

namespace Beca.CarBrandInfo.API.Profiles
{
    public class BrandProfile: Profile
    {
        public BrandProfile()
        {
            CreateMap<Entities.Brand, Models.BrandWithoutModelsDto>();
            CreateMap<Entities.Brand, Models.BrandDto>();
        }
    }
}
