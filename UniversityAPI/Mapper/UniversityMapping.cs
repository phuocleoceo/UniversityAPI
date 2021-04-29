using AutoMapper;
using UniversityAPI.Models;
using UniversityAPI.Models.DTO;

namespace UniversityAPI.Mapper
{
    public class UniversityMapping : Profile
    {
        public UniversityMapping()
        {
            CreateMap<University, UniversityDTO>().ReverseMap();
            CreateMap<PathWay, PathWayDTO>().ReverseMap();
            CreateMap<PathWay, PathwayUpsertDTO>().ReverseMap();
            //ReverseMap cho phep TwoWay Binding giua 2 cai tren chu khong chi 1 chieu
        }
    }
}
