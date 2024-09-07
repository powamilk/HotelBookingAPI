using FluentValidation;
using HotelBookingAPI.Service;

namespace HotelBookingAPI.ViewModel.Validator
{
    public class CreateDatPhongValidator : AbstractValidator<CreateDatPhongVM>
    {
        private readonly IKhachHangService _khachHangService;
        private readonly IKhachSanService _khachSanService;

        public CreateDatPhongValidator(IKhachHangService khachHangService, IKhachSanService khachSanService) 
        {
            _khachHangService = khachHangService;
            _khachSanService = khachSanService;

            RuleFor(x => x.CustomerId)
                .Must(CustomerIdExists).WithMessage("Khách hàng với ID này không tồn tại.");

            RuleFor(x => x.RoomId)
                .Must(RoomIdExists).WithMessage("Phòng với ID này không tồn tại.");

            RuleFor(x => x.CheckInDate)
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Ngày nhận phòng phải lớn hơn hoặc bằng ngày hiện tại.");

            RuleFor(x => x.CheckOutDate)
                .GreaterThan(x => x.CheckInDate).WithMessage("Ngày trả phòng phải lớn hơn ngày nhận phòng.");

            RuleFor(x => x.BookingDate)
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Ngày đặt phòng phải lớn hơn hoặc bằng ngày hiện tại.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Trạng thái đặt phòng không được để trống")
                .Must(x => x == "đã xác nhận" || x == "hủy").WithMessage("Trạng thái phải là 'đã xác nhận' hoặc 'hủy'.");
        }

        private bool CustomerIdExists(int customerId)
        {
            return _khachHangService.GetKhachHangById(customerId, out _) != null;
        }

        private bool RoomIdExists(int roomId)
        {
            return _khachSanService.GetPhongByKhachSanId(roomId, out _) != null;
        }
    }
}
