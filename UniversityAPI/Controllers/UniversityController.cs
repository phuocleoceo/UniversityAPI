using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAPI.Models;
using UniversityAPI.Models.DTO;
using UniversityAPI.Repository.IRepository;
using UniversityAPI.Utility;

namespace UniversityAPI.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityRepository _repo;
        private readonly IMapper _mapper;

        public UniversityController(IUniversityRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list all university
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200,Type =typeof(List<UniversityDTO>))]
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
        /// <summary>
        /// Get a university by its id
        /// </summary>
        /// <param name="id">The id of university that you want to get</param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetUniversity")]
        [ProducesResponseType(200, Type = typeof(UniversityDTO))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
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

        /// <summary>
        /// Create a new university
        /// </summary>
        /// <param name="universityDTO">New university information</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UniversityDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        /// <summary>
        /// Update a university
        /// </summary>
        /// <param name="id">the id of university that you want to update</param>
        /// <param name="universityDTO">new information for university</param>
        /// <returns></returns>
        [HttpPatch("{id:int}", Name = "UpdateUniversity")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateUniversity(int id, [FromBody] UniversityDTO universityDTO)
        {
            if (universityDTO == null || universityDTO.Id != id)
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

        /// <summary>
        /// Delete a university
        /// </summary>
        /// <param name="id">the id of university that you want to delete</param>
        /// <returns></returns>
        [HttpDelete("{id:int}", Name = "DeleteUniversity")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
