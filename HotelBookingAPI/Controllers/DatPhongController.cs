using HotelBookingAPI.Service;
using HotelBookingAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace HotelBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatPhongController : ControllerBase
    {
        private readonly IDatPhongService _datPhongService;

        public DatPhongController(IDatPhongService datPhongService)
        {
            _datPhongService = datPhongService;
        }

        [HttpPost]
        public IActionResult TaoDatPhong([FromBody] CreateDatPhongVM request)
        {
            var result = _datPhongService.CreateDatPhong(request, out string errorMessage);
            if (!result)
            {
                return BadRequest(errorMessage);
            }
            return CreatedAtAction(nameof(LayDatPhongTheoId), new { id = request.Id }, request);
        }

        [HttpGet]
        public IActionResult LayDanhSachDatPhong()
        {
            var datPhongs = _datPhongService.GetAllDatPhong(out string errorMessage);
            if (datPhongs == null)
            {
                return NotFound(errorMessage);
            }
            return Ok(datPhongs);
        }

        [HttpGet("{id}")]
        public IActionResult LayDatPhongTheoId(int id)
        {
            var datPhong = _datPhongService.GetDatPhongById(id, out string errorMessage);
            if (datPhong == null)
            {
                return NotFound(errorMessage);
            }
            return Ok(datPhong);
        }

        [HttpPut("{id}/status")]
        public IActionResult CapNhatTrangThaiDatPhong(int id, [FromBody] UpdateDatPhongStatusVM request)
        {
            var result = _datPhongService.UpdateDatPhongStatus(id, request, out string errorMessage);
            if (!result)
            {
                if (errorMessage.Contains("không tìm thấy"))
                {
                    return NotFound(errorMessage);
                }
                return BadRequest(errorMessage);
            }
            return Ok(request);
        }

        [HttpDelete("{id}")]
        public IActionResult XoaDatPhong(int id)
        {
            var result = _datPhongService.DeleteDatPhong(id, out string errorMessage);
            if (!result)
            {
                return NotFound(errorMessage);
            }
            return NoContent();
        }
    }
}
