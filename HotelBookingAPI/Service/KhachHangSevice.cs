using HotelBookingAPI.ViewModel;
using HotelBookingAPI.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace HotelBookingAPI.Service
{
    public class KhachHangService : IKhachHangService
    {
        private static List<KhachHang> _khachHangs = new();
        private readonly ILogger<KhachHangService> _logger;
        private readonly IValidator<CreateKhachHangVM> _khachHangValidator;

        public KhachHangService(ILogger<KhachHangService> logger, IValidator<CreateKhachHangVM> khachHangValidator)
        {
            _logger = logger;
            _khachHangValidator = khachHangValidator;
        }

        public bool CreateKhachHang(CreateKhachHangVM request, out string errorMessage)
        {
            try
            {
                ValidationResult result = _khachHangValidator.Validate(request);
                if (!result.IsValid)
                {
                    errorMessage = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                    return false;
                }

                var khachHang = new KhachHang
                {
                    Id = _khachHangs.Any() ? _khachHangs.Max(k => k.Id) + 1 : 1,
                    Name = request.Name,
                    Email = request.Email,
                    Phone = request.Phone
                };

                _khachHangs.Add(khachHang);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi khi tạo khách hàng");
                errorMessage = "Đã xảy ra lỗi khi tạo khách hàng";
                return false;
            }
        }

        public List<KhachHangVM> GetAllKhachHang(out string errorMessage)
        {
            if (_khachHangs.Any())
            {
                var khachHangVMs = _khachHangs.Select(k => new KhachHangVM
                {
                    Id = k.Id,
                    Name = k.Name,
                    Email = k.Email,
                    Phone = k.Phone
                }).ToList();
                errorMessage = null;
                return khachHangVMs;
            }
            errorMessage = "Không có khách hàng nào trong danh sách";
            return null;
        }

        public KhachHangVM GetKhachHangById(int id, out string errorMessage)
        {
            var khachHang = _khachHangs.FirstOrDefault(k => k.Id == id);
            if (khachHang == null)
            {
                errorMessage = "Không tìm thấy khách hàng với ID này";
                return null;
            }

            var khachHangVM = new KhachHangVM
            {
                Id = khachHang.Id,
                Name = khachHang.Name,
                Email = khachHang.Email,
                Phone = khachHang.Phone
            };

            errorMessage = null;
            return khachHangVM;
        }

        public bool DeleteKhachHang(int id, out string errorMessage)
        {
            try
            {
                var khachHang = _khachHangs.FirstOrDefault(k => k.Id == id);
                if (khachHang == null)
                {
                    errorMessage = "Không tìm thấy khách hàng với ID này";
                    return false;
                }

                _khachHangs.Remove(khachHang);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi khi xóa khách hàng");
                errorMessage = "Đã xảy ra lỗi khi xóa khách hàng";
                return false;
            }
        }

        public List<DatPhongVM> GetDatPhongByKhachHangId(int khachHangId, out string errorMessage)
        {
            throw new NotImplementedException();
        }
        
    }
}
