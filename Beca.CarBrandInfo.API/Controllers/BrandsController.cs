using AutoMapper;
using Beca.CarBrandInfo.API.Models;
using Beca.CarBrandInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Beca.CarBrandInfo.API.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandInfoRepository _brandInfoRepository;
        private readonly IMapper _mapper;
        const int maxBrandsPageSize = 20;

        /// <summary>
        /// Get all Brands with pagination
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandWithoutModelsDto>>> GetBrands(
            string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxBrandsPageSize)
            {
                pageSize = maxBrandsPageSize;
            }

            var (brandEntities, paginationMetadata) = await _brandInfoRepository
                .GetBrandsAsync(name, searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<BrandWithoutModelsDto>>(brandEntities));
        }

        /// <summary>
        /// Get one specific Brand.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeModels"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrand(int id, bool includeModels = false)
        {
            var brand = await _brandInfoRepository.GetBrandsAsync(id, includeModels);
            if(brand == null)
            {
                return NotFound();
            }

            if (includeModels)
            {
                return Ok(_mapper.Map<BrandDto>(brand));
            }

            return Ok(_mapper.Map<BrandWithoutModelsDto>(brand));
        }

    }
}
