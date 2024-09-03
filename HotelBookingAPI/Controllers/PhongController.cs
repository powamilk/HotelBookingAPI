using HotelBookingAPI.Service;
using HotelBookingAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Controllers
{
    [ApiController]
    [Route("api/hotels")]
    public class PhongController : ControllerBase
    {
        private readonly IKhachSanService _khachSanService;

        public PhongController(IKhachSanService khachSanService)
        {
            _khachSanService = khachSanService;
        }

        [HttpPost("{hotelId}/rooms")]
        public IActionResult TaoPhong(int khachSanId, [FromBody] CreatePhongVM request)
        {
            var result = _khachSanService.CreatePhong(khachSanId, request, out string errorMessage);
            if (!result)
            {
                return BadRequest(errorMessage);
            }
            return CreatedAtAction(nameof(LayPhongTheoKhachSanId), new { khachSanId = khachSanId }, request);
        }

        [HttpGet("{hotelId}/rooms")]
        public IActionResult LayPhongTheoKhachSanId(int khachSanId)
        {
            var phongs = _khachSanService.GetPhongByKhachSanId(khachSanId, out string errorMessage);
            if (phongs == null)
            {
                return NotFound(errorMessage);
            }
            return Ok(phongs);
        }
    }
}
