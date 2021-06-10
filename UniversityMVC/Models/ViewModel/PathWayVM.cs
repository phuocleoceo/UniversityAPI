using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityMVC.Models.ViewModel
{
    public class PathWayVM
    {
        public IEnumerable<SelectListItem> UniversityList { get; set; }

        public PathWay PathWay { get; set; }
    }
}
