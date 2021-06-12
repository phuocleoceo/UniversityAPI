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
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class PathWayController : ControllerBase
    {
        private readonly IPathWayRepository _repo;
        private readonly IMapper _mapper;

        public PathWayController(IPathWayRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list all pathway
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<PathWayDTO>))]
        public IActionResult GetPathWays()
        {
            var list = _repo.GetAll();
            var listDTO = new List<PathWayDTO>();
            foreach (var pw in list)
            {
                listDTO.Add(_mapper.Map<PathWayDTO>(pw));
            }
            return Ok(listDTO);
        }

        //{} => tham so , ? => khong bat buoc
        /// <summary>
        /// Get a pathway by its id
        /// </summary>
        /// <param name="id">The id of pathway that you want to get</param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetPathWay")]
        [ProducesResponseType(200, Type = typeof(PathWayDTO))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetPathWay(int id)
        {
            var pw = _repo.GetById(id);
            if (pw == null)
            {
                return NotFound();
            }
            var pwDTO = _mapper.Map<PathWayDTO>(pw);
            return Ok(pwDTO);
        }

        /// <summary>
        /// Get pathway around a university
        /// </summary>
        /// <param name="university_id">The id of university</param>
        /// <returns></returns>
        [HttpGet("[action]/{university_id:int}", Name = "GetPathWayAroundUniversity")]
        [ProducesResponseType(200, Type = typeof(List<PathWayDTO>))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetPathWayAroundUniversity(int university_id)
        {
            var pwList = _repo.GetPathWayAroundUniversity(university_id);
            if (pwList == null)
            {
                return NotFound();
            }
            var pwDTO = new List<PathWayDTO>();
            foreach (var pw in pwList)
            {
                pwDTO.Add(_mapper.Map<PathWayDTO>(pw));
            }
            return Ok(pwDTO);
        }

        /// <summary>
        /// Create a new pathway
        /// </summary>
        /// <param name="pathwayCreateDTO">New pathway information</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PathWayDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreatePathWay([FromBody] PathwayCreateDTO pathwayCreateDTO)
        {
            if (pathwayCreateDTO == null)
            {
                return BadRequest(ModelState);
            }
            if (_repo.PathWayExists(pathwayCreateDTO.Name))
            {
                ModelState.AddModelError("Error !", "PathWay Exists");
                return StatusCode(404, ModelState);
            }
            var pw = _mapper.Map<PathWay>(pathwayCreateDTO);
            if (!_repo.CreatePathWay(pw))
            {
                ModelState.AddModelError("Error !", $"Something went wrong when creating the record {pw.Name}");
                return StatusCode(500, ModelState);
            }
            //return Ok();
            //Tu dong goi HttpGet voi Id de hien thi thong tin ngay lap tuc
            return CreatedAtRoute("GetPathWay", new { id = pw.Id }, pw);
        }

        /// <summary>
        /// Update a pathway
        /// </summary>
        /// <param name="id">the id of pathway that you want to update</param>
        /// <param name="pathwayUpsertDTO">new information for pathway</param>
        /// <returns></returns>
        [HttpPatch("{id:int}", Name = "UpdatePathWay")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePathWay(int id, [FromBody] PathwayUpdateDTO pathwayUpsertDTO)
        {
            if (pathwayUpsertDTO == null || pathwayUpsertDTO.Id != id)
            {
                return BadRequest(ModelState);
            }
            var pw = _mapper.Map<PathWay>(pathwayUpsertDTO);
            if (!_repo.UpdatePathWay(pw))
            {
                ModelState.AddModelError("Error !", $"Something went wrong when updating the record {pw.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        /// <summary>
        /// Delete a pathway
        /// </summary>
        /// <param name="id">the id of pathway that you want to delete</param>
        /// <returns></returns>
        [HttpDelete("{id:int}", Name = "DeletePathWay")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePathWay(int id)
        {
            if (!_repo.PathWayExists(id))
            {
                return NotFound();
            }
            var pw = _repo.GetById(id);
            if (!_repo.DeletePathWay(pw))
            {
                ModelState.AddModelError("Error !", $"Something went wrong when deleting the record {pw.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
