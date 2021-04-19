using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Models;
using UniversityAPI.Models.DTO;
using UniversityAPI.Repository.IRepository;

namespace UniversityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private IUniversityRepository _repo;
        private readonly IMapper _mapper;

        public UniversityController(IUniversityRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUniversitys()
        {
            var list = _repo.GetAll();
            var listDTO = new List<UniversityDTO>();
            foreach (var uni in list)
            {
                listDTO.Add(_mapper.Map<UniversityDTO>(uni));
            }
            return Ok(listDTO);
        }

        //{} => tham so , ? => khong bat buoc
        [HttpGet("{id:int}", Name = "GetUniversity")]
        public IActionResult GetUniversity(int id)
        {
            var uni = _repo.GetById(id);
            if (uni == null)
            {
                return NotFound();
            }
            var uniDTO = _mapper.Map<UniversityDTO>(uni);
            return Ok(uniDTO);
        }

        [HttpPost]
        public IActionResult CreateUniversity([FromBody] UniversityDTO universityDTO)
        {
            if (universityDTO == null)
            {
                return BadRequest(ModelState);
            }
            if (_repo.UniversityExists(universityDTO.Name))
            {
                ModelState.AddModelError("Error !", "University Exists");
                return StatusCode(404, ModelState);
            }
            var uni = _mapper.Map<University>(universityDTO);
            if (!_repo.CreateUniversity(uni))
            {
                ModelState.AddModelError("Error !", $"Something went wrong when creating the record {uni.Name}");
                return StatusCode(500, ModelState);
            }
            //return Ok();
            //Tu dong goi HttpGet voi Id de hien thi thong tin ngay lap tuc
            return CreatedAtRoute("GetUniversity", new { id = uni.Id }, uni);
        }

        [HttpPatch("{id:int}", Name = "UpdateUniversity")]
        public IActionResult UpdateUniversity(int id, [FromBody] UniversityDTO universityDTO)
        {
            if (universityDTO == null|| universityDTO.Id!=id)
            {
                return BadRequest(ModelState);
            }
            var uni = _mapper.Map<University>(universityDTO);
            if (!_repo.UpdateUniversity(uni))
            {
                ModelState.AddModelError("Error !", $"Something went wrong when updating the record {uni.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "DeleteUniversity")]
        public IActionResult DeleteUniversity(int id)
        {
            if (!_repo.UniversityExists(id))
            {
                return NotFound();
            }
            var uni = _repo.GetById(id);
            if (!_repo.DeleteUniversity(uni))
            {
                ModelState.AddModelError("Error !", $"Something went wrong when deleting the record {uni.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
