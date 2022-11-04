using System.ComponentModel.DataAnnotations;

namespace Beca.CarBrandInfo.API.Models
{
    public class ModelForCreationDto
    {
        /// <summary>
        /// The Name of the new Model
        /// </summary>
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// This is not required. Include a description of this model.
        /// </summary>
        [MaxLength(255)]
        public string? Description { get; set; }
    }
}
