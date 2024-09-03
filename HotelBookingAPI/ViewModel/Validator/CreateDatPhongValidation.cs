using FluentValidation;

namespace HotelBookingAPI.ViewModel.NewFolder
{
    public class CreateDatPhongValidation : AbstractValidator<CreateDatPhongVM>
    {
        public CreateDatPhongValidation() // To do: Validator,CustomerId,RoomId
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("Id Khách Hàng không hợp lệ");

            RuleFor(x => x.RoomId)
                .GreaterThan(0).WithMessage("Id phòng không hợp lệ");

            RuleFor(x => x.CheckInDate)
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Ngày nhận phòng phải lớn hơn hoặc bằng ngày hiện tại");

            RuleFor(x => x.CheckOutDate)
                .GreaterThan(x => x.CheckInDate).WithMessage("Ngày trả phòng phải lớn hơn ngày nhận phòng");

            RuleFor(x => x.BookingDate)
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Ngày đặt phòng phải lớn hơn hoặc bằng ngày hiện tại.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Trạng thái không được để trống")
                .Must(x => x == "đã xác nhận" || x == "hủy").WithMessage("Trạng thái phải là đã xác nhận hoặc đã hủy");
        }
    }
}
