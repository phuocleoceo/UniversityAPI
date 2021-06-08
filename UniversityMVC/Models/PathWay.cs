using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityMVC.Models
{
    public class PathWay
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Distance { get; set; }

        public enum DifficultLevel { Easy, Medium, Difficult, Expert }
        public DifficultLevel Difficulty { get; set; }

        [Required]
        public int UniversityId { get; set; }

        public University University { get; set; }
    }
}
