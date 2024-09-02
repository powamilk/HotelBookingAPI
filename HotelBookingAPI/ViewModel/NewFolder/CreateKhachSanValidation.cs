using FluentValidation;
namespace HotelBookingAPI.ViewModel.NewFolder
{
    public class CreateKhachSanValidation : AbstractValidator<CreateKhachSanVM>
    {
        public CreateKhachSanValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên Khách Sạn Không được bỏ trống")
                .MaximumLength(255).WithMessage("Tên Khách Sạn không được quá 255 kí tự");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Địa chỉ không được để trống")
                .MaximumLength(255).WithMessage("Địa chỉ Khách sạn không đc quá 255 kí tự");

            RuleFor(x => x.Rating)
               .InclusiveBetween(1, 5).WithMessage("Đánh giá phải từ 1 đến 5 ");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Mô tả không được để trống");

            RuleFor(x => x.TotalRoom)
                .GreaterThan(0).WithMessage("Tổng số phòng phải là số nguyên dương");
        }
    }
}
