using Beca.CarBrandInfo.API.Models;

namespace Beca.CarBrandInfo.API
{
    public class BrandDataStore
    {
        public List<BrandDto> Brands { get; set; }
        public static BrandDataStore Current { get; } = new BrandDataStore();

        public BrandDataStore()
        {
            Brands = new List<BrandDto>()
            {
                new BrandDto()
                {
                    Id = 1,
                    Name = "Ford",
                    Models = new List<ModelDto>()
                    {
                        new ModelDto()
                        {
                            Id = 1,
                            Name = "Fiesta",
                            Description = "Since 1976 to 2022. Ford has stopped production of this model."
                        }
                    }
                },
                new BrandDto()
                {
                    Id = 2,
                    Name = "Peugeot",
                    Models = new List<ModelDto>()
                    {
                        new ModelDto()
                        {
                            Id = 2,
                            Name = "405",
                            Description = "Since 1987 to 1997. This model was replace in 2004 by Peugeot 407."
                        }
                    }
                }
            };

        }
    }
}
