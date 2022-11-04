using System.ComponentModel.DataAnnotations;

namespace Beca.CarBrandInfo.API.Models
{
    public class ModelForUpdateDto
    {
        /// <summary>
        /// This is the name of the Brand
        /// </summary>
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// This is not required. Update the description of this model.
        /// </summary>
        [MaxLength(255)]
        public string? Description { get; set; }

    }
}
