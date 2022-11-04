using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Beca.CarBrandInfo.API.Entities
{
    public class Model
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        [ForeignKey("BrandId")]
        public Brand? Brand { get; set; }
        public int BrandId { get; set; }

        public Model(string name)
        {
            Name = name;
        }
    }
}
