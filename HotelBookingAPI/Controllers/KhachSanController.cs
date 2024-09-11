using FluentValidation;
using HotelBookingAPI.Service;
using HotelBookingAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;

namespace HotelBookingAPI.Controllers
{
    [Route("api/hotels")]
    [ApiController]
    public class KhachSanController : ControllerBase
    {
        private readonly IKhachSanService _khachSanService;
        private readonly IValidator<CreateKhachSanVM> _khachSanValidator;
        private readonly IValidator<KhachSanUpdateVM> _khachSanUpdateValidator;
      

        public KhachSanController(IKhachSanService khachSanService, IValidator<CreateKhachSanVM> khachSanValidator, IValidator<KhachSanUpdateVM> khachSanUpdateValidator)
        {
            _khachSanService = khachSanService;
            _khachSanUpdateValidator = khachSanUpdateValidator;
            _khachSanValidator = khachSanValidator;
        }

        [HttpPost]
        public IActionResult CreateKhachSan([FromBody] CreateKhachSanVM request)
        {
            ValidationResult result = _khachSanValidator.Validate(request);
            if(!result.IsValid)
            {
                var error = result.Errors.Select(x => new
                {
                    ProperyName = x.PropertyName,
                    ErrorMessage = x.ErrorMessage,
                });
                return BadRequest(error);
            }    
            var succes = _khachSanService.CreateKhachSan(request, out string errorMessage);
            if (!succes)
            {
                return BadRequest(errorMessage);
            }
            return CreatedAtAction(nameof(CreateKhachSan), request);
        }

        [HttpGet]
        public IActionResult GetAllKhachSan()
        {
            var khachSans = _khachSanService.GetAllKhachSan(out string errorMessage);
            if (khachSans == null)
            {
                return NotFound(errorMessage);
            }
            return Ok(khachSans);
        }

        [HttpGet("{id}")]
        public IActionResult GetKhachSanById(int id)
        {
            var khachSan = _khachSanService.GetKhachSanById(id, out string errorMessage);
            if (khachSan == null)
            {
                return NotFound(errorMessage);
            }
            return Ok(khachSan);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateKhachSan(int id, [FromBody] KhachSanUpdateVM request)
        {
            ValidationResult result = _khachSanUpdateValidator.Validate(request);
            if (!result.IsValid)
            {
                var error = result.Errors.Select(x => new
                {
                    ProperyName = x.PropertyName,
                    ErrorMessage = x.ErrorMessage,
                });
                return BadRequest(error);
            }
            var succces = _khachSanService.UpdateKhachSan(id, request, out string errorMessage);
            if (!succces)
            {
                return BadRequest(errorMessage);
            }
            return Ok(request);
        }

        [HttpDelete("{khachSanId}")]
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
