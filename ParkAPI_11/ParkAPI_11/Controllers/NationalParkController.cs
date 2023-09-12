using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/nationalPark")]
    [ApiController]
   
    public class NationalParkController : ControllerBase
    {
        private readonly INationalParkRepository _nationalParkrepository;

        private readonly IMapper _mapper;
        public NationalParkController(INationalParkRepository nationalParkRepository,IMapper mapper)
        {
            _nationalParkrepository = nationalParkRepository;
            _mapper = mapper;
                
        }
        [HttpGet]
        public IActionResult GetNationalParks()
        {
            var nationalParkListDto = _nationalParkrepository.GetNationalParks().ToList().Select(_mapper.Map<NationalPark, NationalParkDTO>);
            return Ok(nationalParkListDto);//200
        }
        [HttpGet("{nationalParkId:int}",Name = "GetNationalPark")]

        public IActionResult GetNationalPark(int nationalParkId)
        {
            var nationalpark = _nationalParkrepository.GetNationalPark(nationalParkId);
            if (nationalpark == null)
                return NotFound();
            var nationalParkDto = _mapper.Map<NationalParkDTO>(nationalpark);
            return Ok(nationalParkDto);//200
        }
        [HttpPost]
        public IActionResult CreateNationalpark([FromBody] NationalParkDTO nationalParkDTO)
        {
            if (nationalParkDTO == null)
                return BadRequest(ModelState);//400 status code
            if(_nationalParkrepository.NationalParkExists(nationalParkDTO.Name))
            {
                ModelState.AddModelError("", "National Park Already In Db");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var nationalPark = _mapper.Map<NationalParkDTO, NationalPark>(nationalParkDTO);
            if(!_nationalParkrepository.CreateNationalPark(nationalPark))
            {
                ModelState.AddModelError("", $"Something went wrong while Save data{nationalPark.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError,ModelState);
            }
            //return Ok();
            return CreatedAtRoute("GetNationalPark", new { nationalParkId = nationalPark.Id }, nationalPark);//201
        }
        [HttpPut]
        public IActionResult UpdateNationalPark([FromBody] NationalParkDTO nationalParkDTO)
        {
            if (nationalParkDTO == null)
                return BadRequest(ModelState);//400
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var nationalPark = _mapper.Map<NationalPark>(nationalParkDTO);
            if(_nationalParkrepository.UpdateNationalPark(nationalPark))
            {
                ModelState.AddModelError("", $"Something went wrong while Update data{nationalPark.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            //return Ok();
            return NoContent();//204
        }
        [HttpDelete("{nationalParkId:int}")]

        public IActionResult DeleteNationalpark(int nationalParkId)
        {
            if (! _nationalParkrepository.NationalParkExists(nationalParkId))
                return NotFound();
            var nationalpark= _nationalParkrepository.GetNationalPark(nationalParkId);
            if (!_nationalParkrepository.DeleteNationalPark(nationalpark))
            {
                ModelState.AddModelError("", $"Something went wrong while Delete data{nationalpark.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return Ok();
        }
    }
}
