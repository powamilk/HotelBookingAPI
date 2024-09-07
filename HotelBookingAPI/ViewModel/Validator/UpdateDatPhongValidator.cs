using FluentValidation;

namespace HotelBookingAPI.ViewModel.Validator
{
    public class UpdateDatPhongValidator : AbstractValidator<UpdateDatPhongStatusVM>
    {
        public UpdateDatPhongValidator()
        {
            RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Trạng thái đặt phòng không được để trống")
            .Must(x => x == "đã xác nhận" || x == "hủy").WithMessage("Trạng thái phải là 'đã xác nhận' hoặc 'hủy'.");
        }
    }
}
