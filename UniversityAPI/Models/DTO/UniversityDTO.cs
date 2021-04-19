using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAPI.Models.DTO
{
    public class UniversityDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Abbreviation { get; set; }

        [Required]
        public string Address { get; set; }

        public DateTime Established { get; set; }
    }
}
