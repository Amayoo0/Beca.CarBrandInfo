using Beca.CarBrandInfo.API.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Beca.CarBrandInfo.API.Models
{
    public class ModelDto
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Model Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// A simply description of the Model
        /// </summary>
        public string? Description { get; set; }
    }
}
