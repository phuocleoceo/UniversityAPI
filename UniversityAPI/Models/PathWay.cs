using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Models
{
    public class PathWay
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Distance { get; set; }

        public enum DifficultLevel { Easy,Medium,Difficult,Expert}

        public DifficultLevel Difficulty { get; set; }

        [Required]
        public int UniversityId { get; set; }
        [ForeignKey(nameof(UniversityId))]
        public University University { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
