using HotelBookingAPI.ViewModel;
using HotelBookingAPI.Entities;
using FluentValidation;
using FluentValidation.Results;
using AutoMapper;

namespace HotelBookingAPI.Service
{
    public class KhachHangService : IKhachHangService
    {
        private static List<KhachHang> _khachHangs = new();
        private readonly ILogger<KhachHangService> _logger;
        private readonly IMapper _mapper;

        public KhachHangService(ILogger<KhachHangService> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public bool CreateKhachHang(CreateKhachHangVM request, out string errorMessage)
        {
            try
            {

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
                var khachHangVMs = _mapper.Map<List<KhachHangVM>>(_khachHangs);
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

            var khachHangVM = _mapper.Map<KhachHangVM>(khachHang);
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
