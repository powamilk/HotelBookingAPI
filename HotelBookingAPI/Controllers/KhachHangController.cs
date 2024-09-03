using HotelBookingAPI.Service;
using HotelBookingAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingAPI.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class KhachHangController : ControllerBase
    {
        private readonly IKhachHangService _khachHangService;

        public KhachHangController(IKhachHangService khachHangService)
        {
            _khachHangService = khachHangService;
        }

        [HttpPost]
        public IActionResult TaoKhachHang([FromBody] CreateKhachHangVM request)
        {
            var result = _khachHangService.CreateKhachHang(request, out string errorMessage);
            if (!result)
            {
                return BadRequest(errorMessage);
            }
            return CreatedAtAction(nameof(LayKhachHangTheoId), new { id = request.Id }, request);
        }

        [HttpGet]
        public IActionResult LayDanhSachKhachHang()
        {
            var khachHangs = _khachHangService.GetAllKhachHang(out string errorMessage);
            if (khachHangs == null)
            {
                return NotFound(errorMessage);
            }
            return Ok(khachHangs);
        }

        [HttpGet("{id}")]
        public IActionResult LayKhachHangTheoId(int id)
        {
            var khachHang = _khachHangService.GetKhachHangById(id, out string errorMessage);
            if (khachHang == null)
            {
                return NotFound(errorMessage);
            }
            return Ok(khachHang);
        }

        [HttpDelete("{id}")]
        public IActionResult XoaKhachHang(int id)
        {
            var result = _khachHangService.DeleteKhachHang(id, out string errorMessage);
            if (!result)
            {
                return NotFound(errorMessage);
            }
            return NoContent();
        }
    }
}
