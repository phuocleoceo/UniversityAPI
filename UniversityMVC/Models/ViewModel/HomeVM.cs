using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityMVC.Models.ViewModel
{
    public class HomeVM
    {
        public IEnumerable<University> UniversityList { get; set; }

        public IEnumerable<PathWay> PathWayList { get; set; }
    }
}
