using System.ComponentModel.DataAnnotations;
using static UniversityAPI.Models.PathWay;

namespace UniversityAPI.Models.DTO
{
    public class PathwayCreateDTO
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public double Distance { get; set; }

        public DifficultLevel Difficulty { get; set; }

        [Required]
        public int UniversityId { get; set; }
    }
}
