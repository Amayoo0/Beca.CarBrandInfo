using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Beca.CarBrandInfo.API.Controllers;
using Beca.CarBrandInfo.API.Entities;
using Beca.CarBrandInfo.API.Models;
using Beca.CarBrandInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Moq;

namespace Beca.CarBrandInfo.API.Test
{
    public class BrandsControllerTests
    {
        private readonly BrandsController _brandsController;
        
        public BrandsControllerTests()
        {
            var brandInfoRepositoryMock = new Mock<IBrandInfoRepository>();
            brandInfoRepositoryMock
                .Setup(m => m.GetBrandsAsync())
                .ReturnsAsync(new List<Brand>
                {
                    new Brand("Malaga")
                    {
                        Id =1,
                    }
                });
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Profiles.BrandProfile>();
                cfg.AddProfile<Profiles.ModelProfile>();
            });
            var mapper = new Mapper(mapperConfig);

            _brandsController = new BrandsController(
                brandInfoRepositoryMock.Object, mapper); 
        }


        [Fact]
        public async Task GetBrands_GetAction_MustReturnOkObjectResult() //Name of the action_what we're going to do_what we going to test
        {
            // Arrange

            // Act
            var result = await _brandsController.GetBrands();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Models.BrandDto>>>(result);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetBrands_GetAction_MustReturnIEnumerableOfBrands()
        {
            // Arrange

            // Act
            var result = await _brandsController.GetBrands();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Models.BrandDto>>>(result);
            Assert.IsAssignableFrom<IEnumerable<Models.BrandDto>>(
                ((OkObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public async Task GetBrands_GetAction_MustReturnNumberOfBrands()
        {
            // Arrange

            // Act
            var result = await _brandsController.GetBrands();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Models.BrandDto>>>(result);
            Assert.Equal(1,
                ((IEnumerable<Models.BrandDto>)
                ((OkObjectResult)actionResult.Result).Value).Count());

        }

    }
}
