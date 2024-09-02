using FluentValidation;

namespace HotelBookingAPI.ViewModel.NewFolder
{
    public class CreatePhongValidation : AbstractValidator<CreatePhongVM>
    {
        public CreatePhongValidation() 
        {
            RuleFor(x => x.RoomType)
                .NotEmpty().WithMessage("Loại phòng không được để trông")
                .MaximumLength(100).WithMessage("Loại phòng không được quá 100 ký tự");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Giá phòng phải là số dương");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Trạng thái phòng không được để trống")
                .Must(x => x == "còn trống" || x == "đã đặt").WithMessage("Trạng thái phòng phải là 'đã đăt' hoặc 'còn trống' ");

            RuleFor(x => x.CheckInTime)
                .NotEmpty().WithMessage("Thơi gian nhận phòng không được để trống");

            RuleFor(x => x.CheckOutTime)
                .GreaterThan(x => x.CheckInTime).WithMessage("Thời gian trả phòng phải lớn hơn thời gian trả phòng");
        }
    }
}
