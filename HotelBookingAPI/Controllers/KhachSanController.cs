using HotelBookingAPI.Service;
using HotelBookingAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachSanController : ControllerBase
    {
        private readonly IKhachSanService _khachSanService;

        public KhachSanController(IKhachSanService khachSanService)
        {
            _khachSanService = khachSanService;
        }

        [HttpPost]
        public IActionResult CreateKhachSan([FromBody] CreateKhachSanVM request)
        {
            var result = _khachSanService.CreateKhachSan(request, out string errorMessage);
            if(!result)
            {
                return BadRequest(errorMessage);
            }
            return CreatedAtAction(nameof(CreateKhachSan), request);
        }

        [HttpGet]
        public IActionResult GetAllKhachSan()
        {
            var khachSans = _khachSanService.GetAllKhachSan(out string errorMessage);
            if(khachSans == null)
            {
                return NotFound(errorMessage);
            }   
            return Ok(khachSans);
        }

        [HttpGet("{id}")]
        public IActionResult GetKhachSanById(int id)
        {
            var khachSan = _khachSanService.GetKhachSanById(id, out string errorMessage);
            if(khachSan == null)
            {
                return NotFound(errorMessage);
            } 
            return Ok(khachSan);
        }

        [HttpPut]
        public IActionResult UpdateKhachSan(int id , [FromBody] KhachSanUpdateVM request)
        {
            var result = _khachSanService.UpdateKhachSan(id, request, out string errorMessage);
            if (!result)
            {
                return BadRequest(errorMessage);
            }
            return Ok(request);             
        }

        [HttpDelete]
        public IActionResult DeleteKhachSan(int khachSanId)
        {
            var result = _khachSanService.DeleteKhachSan(khachSanId, out string errorMessage);
            if (!result)
            {
                return NotFound(errorMessage);
            }
            return NoContent();
        }

        
    }
}
