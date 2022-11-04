using System.ComponentModel.DataAnnotations;

namespace Beca.CarBrandInfo.API.Models
{
    public class ModelForCreationDto
    {
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }
    }
}
