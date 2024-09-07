using FluentValidation;
using HotelBookingAPI.Service;

namespace HotelBookingAPI.ViewModel.Validator
{
    public class CreatePhongValidator : AbstractValidator<CreatePhongVM>
    {
        private readonly IKhachSanService _khachSanService;
        public CreatePhongValidator(IKhachSanService khachSanService)
        {
            _khachSanService = khachSanService;
            RuleFor(x => x.HotelId)
                .Must(HotelIdExists).WithMessage("Khách sạn với ID này không tồn tại.");
            RuleFor(x => x.RoomType)
                .NotEmpty().WithMessage("Loại phòng không được để trống")
                .MaximumLength(100).WithMessage("Loại phòng không được quá 100 ký tự");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Giá phòng phải là số dương");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Trạng thái phòng không được để trống")
                .Must(x => x == "còn trống" || x == "đã đặt").WithMessage("Trạng thái phòng phải là 'còn trống' hoặc 'đã đặt'");

            RuleFor(x => x.CheckInTime)
                .Must(BeAValidTime).WithMessage("Thời gian nhận phòng phải hợp lệ (00:00 - 23:59)");

            RuleFor(x => x.CheckOutTime)
                .GreaterThan(x => x.CheckInTime).WithMessage("Thời gian trả phòng phải lớn hơn thời gian nhận phòng.");
        }

        private bool BeAValidTime(TimeSpan time)
        {
            return time >= TimeSpan.Zero && time < TimeSpan.FromHours(24);
        }
        private bool HotelIdExists(int khachSanId)
        {
            return _khachSanService.CheckKhachSanExist(khachSanId);
        }
    }
}
