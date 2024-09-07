using FluentValidation;
using HotelBookingAPI.Service;
using HotelBookingAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;

namespace HotelBookingAPI.Controllers
{
    [ApiController]
    [Route("api/hotels")]
    public class PhongController : ControllerBase
    {
        private readonly IKhachSanService _khachSanService;
        private readonly IValidator<CreatePhongVM> _phongValidator;

        public PhongController(IValidator<CreatePhongVM> phongValidator, IKhachSanService khachSanService)
        {
            _khachSanService = khachSanService;
            _phongValidator = phongValidator;
        }

        [HttpPost("{hotelId}/rooms")]
        public IActionResult TaoPhong(int hotelId, [FromBody] CreatePhongVM request)
        {
            ValidationResult result = _phongValidator.Validate(request);
            if(!result.IsValid)
            {
                var error = result.Errors.Select(x => new
                {
                    PropertyName = x.PropertyName,
                    ErrorMessage = x.ErrorMessage,
                });
                return BadRequest(error);
            }    
            var success = _khachSanService.CreatePhong(hotelId, request, out string errorMessage);
            if (!success)
            {
                return BadRequest(errorMessage);
            }
            return Created($"/api/hotel/{hotelId}/rooms", request);
        }

        [HttpGet("{hotelId}/rooms")]
        public IActionResult LayPhongTheoKhachSanId(int hotelId)
        {
            var phongs = _khachSanService.GetPhongByKhachSanId(hotelId, out string errorMessage);
            if (phongs == null)
            {
                return NotFound(errorMessage);
            }
            return Ok(phongs);
        }
    }
}
