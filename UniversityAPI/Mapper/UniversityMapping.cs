using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Models;
using UniversityAPI.Models.DTO;

namespace UniversityAPI.Mapper
{
    public class UniversityMapping : Profile
    {
        public UniversityMapping()
        {
            CreateMap<University, UniversityDTO>().ReverseMap();
            //ReverseMap cho phep TwoWay Binding giua 2 cai tren chu khong chi 1 chieu
        }
    }
}
