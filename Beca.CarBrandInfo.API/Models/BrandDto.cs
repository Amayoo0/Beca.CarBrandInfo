using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Beca.CarBrandInfo.API.Models
{
    public class BrandDto
    {
        /// <summary>
        /// The Id is required to identify the Brand
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The name of the Brand
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Collection of Models associated with a Brand
        /// </summary>
        public ICollection<ModelDto> Models { get; set; } = new List<ModelDto>();
    }
}
