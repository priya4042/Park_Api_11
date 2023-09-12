using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkAPI_11.Models;
using ParkAPI_11.Models.Dtos;
using ParkAPI_11.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAPI_11.Controllers
{
    [Route("api/Trail")]
    [ApiController]
    public class TrailController : ControllerBase
    {
        private readonly ITrailRepository _trailRepository;
        private readonly IMapper _mapper;
        public TrailController(ITrailRepository trailRepository, IMapper mapper)
        {
            _trailRepository = trailRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetTrails()
        {
            return Ok(_trailRepository.GetTrails().ToList().Select(_mapper.Map<Trail, TrailDTO>));
        }

        [HttpGet("{trailId:int}", Name = "GetTrail")]

        public IActionResult GetTrail(int trailId)
        {
            var trail = _trailRepository.GetTrail(trailId);
            if (trail == null)
                return NotFound();
            return Ok(_mapper.Map<TrailDTO>(trail));
        }
        [HttpPost]
        public IActionResult CreateTrail([FromBody] TrailDTO trailDTO)
        {
            if (trailDTO == null)
                return BadRequest(ModelState);
            if(_trailRepository.TrailExists(trailDTO.Name))
            {
                ModelState.AddModelError("", "Trail Already In Db : {trailDTO.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var trail = _mapper.Map<Trail>(trailDTO);
            if(!_trailRepository.CreateTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrong while Save data{trail.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateTrail([FromBody] TrailDTO trailDTO)
        {
            if (trailDTO == null)
                return NotFound();
            var trail = _mapper.Map<Trail>(trailDTO);
            if(!_trailRepository.UpdateTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrong while Update Trail:{trail.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }

        [HttpDelete ("{trailId:int}")]

        public IActionResult Delete(int trailId)
        {
            if (!_trailRepository.TrailExists(trailId))
                return NotFound();
            var trail = _trailRepository.GetTrail(trailId);
            if(! _trailRepository.DeleteTrail(trail))
            {
                ModelState.AddModelError("", $"Something went wrong while Delete data{trail.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return Ok();
        }
    }
}
