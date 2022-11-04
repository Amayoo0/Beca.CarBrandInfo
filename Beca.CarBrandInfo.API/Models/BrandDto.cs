using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Beca.CarBrandInfo.API.Models
{
    public class BrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ModelDto> Models { get; set; } = new List<ModelDto>();
    }
}
