using HotelBookingAPI.Service;
using HotelBookingAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using HotelBookingAPI.ViewModel.Validator;
using FluentValidation;


namespace HotelBookingAPI.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class DatPhongController : ControllerBase
    {
        private readonly IDatPhongService _datPhongService;
        private readonly IValidator<CreateDatPhongVM> _datPhongValidator;
        private readonly IValidator<UpdateDatPhongStatusVM> _datPhongStatusValidator;

        public DatPhongController(IDatPhongService datPhongService, IValidator<CreateDatPhongVM> createDatPhongValidator, IValidator<UpdateDatPhongStatusVM> datPhongStatusValidator)
        {
            _datPhongService = datPhongService;
            _datPhongValidator = createDatPhongValidator;
            _datPhongStatusValidator = datPhongStatusValidator;
        }

        [HttpPost]
        public IActionResult TaoDatPhong([FromBody] CreateDatPhongVM request)
        {
            ValidationResult result = _datPhongValidator.Validate(request);
            if (!result.IsValid)
            {
                var error = result.Errors.Select(x => new
                {
                    Property = x.PropertyName,
                    Error = x.ErrorMessage
                });
                return BadRequest(error);  
            }
            var datPhong = _datPhongService.CreateDatPhong(request, out string errorMessage);
            if (!datPhong)
            {
                return BadRequest(errorMessage);
            }
            return CreatedAtAction(nameof(TaoDatPhong), request);
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
            ValidationResult result = _datPhongStatusValidator.Validate(request);
            if (!result.IsValid)
            {
                var error = result.Errors.Select(x => new
                {
                    Property = x.PropertyName,
                    Error = x.ErrorMessage
                });
                return BadRequest(error);
            }
            var succes = _datPhongService.UpdateDatPhongStatus(id, request, out string errorMessage);
            if (!succes)
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
