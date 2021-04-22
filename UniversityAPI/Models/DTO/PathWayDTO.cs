using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static UniversityAPI.Models.PathWay;

namespace UniversityAPI.Models.DTO
{
    public class PathWayDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Distance { get; set; }

        public DifficultLevel Difficulty { get; set; }

        [Required]
        public int UniversityId { get; set; }
        [ForeignKey(nameof(UniversityId))]
        public UniversityDTO University { get; set; }
    }
}
