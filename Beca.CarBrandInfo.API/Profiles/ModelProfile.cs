using AutoMapper;

namespace Beca.CarBrandInfo.API.Profiles
{
    public class ModelProfile : Profile
    {

        public ModelProfile()
        {
            CreateMap<Entities.Model, Models.ModelDto>();
            CreateMap<Models.ModelForCreationDto, Entities.Model>();
            CreateMap<Models.ModelForUpdateDto, Entities.Model>();
            CreateMap<Entities.Model, Models.ModelForUpdateDto>();
        }

    }
}
