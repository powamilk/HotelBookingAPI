using FluentValidation;
using HotelBookingAPI.Service;
using HotelBookingAPI.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;

namespace HotelBookingAPI.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class KhachHangController : ControllerBase
    {
        private readonly IKhachHangService _khachHangService;
        private readonly IValidator<CreateKhachHangVM> _khachHangValidator;

        public KhachHangController(IKhachHangService khachHangService, IValidator<CreateKhachHangVM> khachHangValidator)
        {
            _khachHangService = khachHangService;
            _khachHangValidator = khachHangValidator;
        }

        [HttpPost]
        public IActionResult TaoKhachHang([FromBody] CreateKhachHangVM request)
        {
            ValidationResult result = _khachHangValidator.Validate(request);
            if(!result.IsValid)
            {
                var error = result.Errors.Select(x => new
                {
                    PropertyName = x.PropertyName,
                    ErrorMessage = x.ErrorMessage
                });
                return BadRequest(error);
            }    
            var khachHang = _khachHangService.CreateKhachHang(request, out string errorMessage);
            if (!khachHang)
            {
                return BadRequest(errorMessage);
            }
            return CreatedAtAction(nameof(TaoKhachHang), request);
        }

    }
}
