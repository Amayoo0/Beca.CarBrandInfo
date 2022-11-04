using AutoMapper;
using Beca.CarBrandInfo.API.Models;
using Beca.CarBrandInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Beca.CarBrandInfo.API.Controllers
{
    [Route("api/brands/{brandId}/models")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly ILogger<ModelsController> _logger;
        private readonly IBrandInfoRepository _brandInfoRepository;
        private readonly IMapper _mapper;

        public ModelsController(ILogger<ModelsController> logger,
            IBrandInfoRepository brandInfoRepository,
            IMapper mapper)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _brandInfoRepository = brandInfoRepository ??
                throw new ArgumentNullException(nameof(brandInfoRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModelDto>>> GetModels(
            int brandId)
        {

            if (!await _brandInfoRepository.BrandExistsAsync(brandId))
            {
                _logger.LogInformation(
                    $"Brand with id {brandId} wasn't found when accessing models.");
                return NotFound();
            }

            var modelsForBrand = await _brandInfoRepository.GetModelsForBrandAsync(brandId);

            return Ok(_mapper.Map<IEnumerable<ModelDto>>(modelsForBrand));
        }

        [HttpGet("{modelid}", Name = "GetModel")]
        public async Task<ActionResult<ModelDto>> GetModel(
            int brandId, int modelId)
        {
            if (!await _brandInfoRepository.BrandExistsAsync(brandId))
            {
                return NotFound();
            }

            var model = await _brandInfoRepository
                .GetModelForBrandAsync(brandId, modelId);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ModelDto>(model));
        }

        [HttpPost]
        public async Task<ActionResult<ModelDto>> CreateModel(
           int brandId,
           ModelForCreationDto model)
        {
            if (!await _brandInfoRepository.BrandExistsAsync(brandId))
            {
                return NotFound();
            }

            var finalModel = _mapper.Map<Entities.Model>(model);

            await _brandInfoRepository.AddModelForBrandAsync(
                brandId, finalModel);

            await _brandInfoRepository.SaveChangesAsync();

            var createdModelToReturn =
                _mapper.Map<Models.ModelDto>(finalModel);

            return CreatedAtRoute("GetModel",
                 new
                 {
                     brandId = brandId,
                     modelId = createdModelToReturn.Id
                 },
                 createdModelToReturn);
        }

        [HttpPut("{modelid}")]
        public async Task<ActionResult> UpdateModel(int brandId, int modelId,
            ModelForUpdateDto model)
        {
            if (!await _brandInfoRepository.BrandExistsAsync(brandId))
            {
                return NotFound();
            }

            var modelEntity = await _brandInfoRepository
                .GetModelForBrandAsync(brandId, modelId);
            if (modelEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(model, modelEntity);

            await _brandInfoRepository.SaveChangesAsync();

            return NoContent();
        }


        [HttpPatch("{modelid}")]
        public async Task<ActionResult> PartiallyUpdateModel(
            int brandId, int modelId,
            JsonPatchDocument<ModelForUpdateDto> patchDocument)
        {
            if (!await _brandInfoRepository.BrandExistsAsync(brandId))
            {
                return NotFound();
            }

            var modelEntity = await _brandInfoRepository
                .GetModelForBrandAsync(brandId, modelId);
            if (modelEntity == null)
            {
                return NotFound();
            }

            var modelToPatch = _mapper.Map<ModelForUpdateDto>(
                modelEntity);

            patchDocument.ApplyTo(modelToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(modelToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(modelToPatch, modelEntity);
            await _brandInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{modelid}")]
        public async Task<ActionResult> DeleteModel(
            int brandId, int modelId)
        {
            if (!await _brandInfoRepository.BrandExistsAsync(brandId))
            {
                return NotFound();
            }

            var modelEntity = await _brandInfoRepository
                .GetModelForBrandAsync(brandId, modelId);
            if (modelEntity == null)
            {
                return NotFound();
            }

            _brandInfoRepository.DeleteModel(modelEntity);
            await _brandInfoRepository.SaveChangesAsync();


            return NoContent();
        }

    }
}
