using AutoMapper;
using Beca.CarBrandInfo.API.Controllers;
using Beca.CarBrandInfo.API.Entities;
using Beca.CarBrandInfo.API.Models;
using Beca.CarBrandInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;


namespace Beca.CarBrandInfo.API.Test
{
    public class ModelsControllerTests
    {
        private readonly ModelsController _modelsController;
        private HttpContext _httpContext;

        public ModelsControllerTests()
        {
            var loggerMock = new Mock<ILogger<ModelsController>>();
            var brandInfoRepositoryMock = new Mock<IBrandInfoRepository>();
            brandInfoRepositoryMock.Setup(m => m.GetBrandsAsync())
                .ReturnsAsync(new List<Brand>()
                {
                    new Brand("Malaga")
                    {
                        Id =1,
                    }
                });
            brandInfoRepositoryMock.Setup(m => m.BrandExistsAsync(1))
                .ReturnsAsync(true);
            brandInfoRepositoryMock.Setup(m => m.GetModelForBrandAsync(1, 1))
                .ReturnsAsync(new Model("Fiesta")
                {
                    Id = 1,
                    BrandId = 1,
                    Description = "The best car ever"
                });
            brandInfoRepositoryMock.Setup(m => m.GetModelsForBrandAsync(1))
                .ReturnsAsync(new List<Model>()
                {
                    new Model("Fiesta")
                    {
                        Id = 1,
                        BrandId = 1,
                        Description = "The best car ever"
                    },
                    new Model("Kuga")
                    {
                        Id = 2,
                        BrandId = 1,
                        Description = "A SUV"
                    }
                });
            brandInfoRepositoryMock.Setup(m => m.AddModelForBrandAsync(1, new Model("Kuga")
            {
                Id = 2,
                BrandId = 1,
                Description = "A SUV"
            }));
            brandInfoRepositoryMock.Setup(m => m.SaveChangesAsync())
                .ReturnsAsync(true);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Profiles.BrandProfile>();
                cfg.AddProfile<Profiles.ModelProfile>();
            });
            var mapper = new Mapper(mapperConfig);

            _modelsController = new ModelsController(loggerMock.Object,
                brandInfoRepositoryMock.Object, mapper);

            _httpContext = new DefaultHttpContext();
            _modelsController.ControllerContext = new ControllerContext()
            {
                HttpContext = _httpContext
            };
        }

        [Fact]
        public async Task GetModels_GetAction_MustReturnOkObjectResult()
        {
            // Arrange

            // Act
            var result = await _modelsController.GetModels(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Models.ModelDto>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetModel_GetAction_MustReturnOkObjectResult()
        {
            // Arrange

            // Act
            var result = await _modelsController.GetModel(1, 1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Models.ModelDto>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task CreateModel_GetAction_MustReturnOkObjectResult()
        {
            // Arrange
            var model = new ModelForCreationDto()
            {
                Name = "Kuga",
                Description = "A SUV"
            };

            // Act
            var result = await _modelsController.CreateModel(1, model);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Models.ModelDto>>(result);
            Assert.IsType<CreatedAtRouteResult>(actionResult.Result);
        }

        [Fact]
        public async Task UpdateModel_GetAction_MustReturnNoContent()
        {
            // Arrange
            var model = new ModelForUpdateDto()
            {
                Name = "Kuga",
                Description = "A SUV"
            };

            // Act
            var result = await _modelsController.UpdateModel(1, 1, model);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

    }
}
