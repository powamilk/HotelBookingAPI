using FluentValidation;

namespace HotelBookingAPI.ViewModel.NewFolder
{
    public class CreateKhachHangValidation : AbstractValidator<KhachHangVM>
    {
        public CreateKhachHangValidation() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên Khách hàng không được để trống")
                .MaximumLength(255).WithMessage("Tên Khách Hàng không được quá 255 ký tự");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Địa chỉ Email không được để trống")
                .EmailAddress().WithMessage("Địa chỉ email không hợp lệ");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Số điện thoại không được bỏ trống")
                .Matches(@"^\d{10,15}$").WithMessage("Số điện thoại phải là số và có độ dai từ 10 đến 15 ký tự");
        }
    }
}
