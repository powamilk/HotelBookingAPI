using FluentValidation;
using HotelBookingAPI.Entities;
using HotelBookingAPI.ViewModel;
using FluentValidation.Results;

namespace HotelBookingAPI.Service
{
    public class DatPhongService : IDatPhongService
    {
        private static List<DatPhong> _datPhongs = new();
        private readonly ILogger<DatPhongService> _logger;
        private readonly IValidator<CreateDatPhongVM> _datPhongValidator;

        public DatPhongService(ILogger<DatPhongService> logger, IValidator<CreateDatPhongVM> datPhongValidator)
        {
            _logger = logger;
            _datPhongValidator = datPhongValidator;
        }

        public bool CreateDatPhong(CreateDatPhongVM request, out string errorMessage)
        {
            try
            {
                ValidationResult result = _datPhongValidator.Validate(request);
                if (!result.IsValid)
                {
                    errorMessage = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                    return false;
                }

                var datPhong = new DatPhong
                {
                    Id = _datPhongs.Any() ? _datPhongs.Max(d => d.Id) + 1 : 1,
                    CustomerId = request.CustomerId,
                    RoomId = request.RoomId,
                    CheckInDate = request.CheckInDate,
                    CheckOutDate = request.CheckOutDate,
                    BookingDate = request.BookingDate,
                    Status = request.Status,
                    CreateAt = DateTime.Now
                };

                _datPhongs.Add(datPhong);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi khi tạo đặt phòng");
                errorMessage = "Đã xảy ra lỗi khi tạo đặt phòng";
                return false;
            }
        }

        public List<DatPhongVM> GetAllDatPhong(out string errorMessage)
        {
            if (_datPhongs.Any())
            {
                var datPhongsVM = _datPhongs.Select(d => new DatPhongVM
                {
                    Id = d.Id,
                    CustomerId = d.CustomerId,
                    RoomId = d.RoomId,
                    CheckInDate = d.CheckInDate,
                    CheckOutDate = d.CheckOutDate,
                    BookingDate = d.BookingDate,
                    Status = d.Status,
                    CreateAt = d.CreateAt
                }).ToList();
                errorMessage = null;
                return datPhongsVM;
            }
            errorMessage = "Không có đặt phòng nào trong danh sách";
            return null;
        }

        public DatPhongVM GetDatPhongById(int id, out string errorMessage)
        {
            var datPhong = _datPhongs.FirstOrDefault(d => d.Id == id);
            if (datPhong == null)
            {
                errorMessage = "Không tìm thấy đặt phòng với ID này";
                return null;
            }

            var datPhongVM = new DatPhongVM
            {
                Id = datPhong.Id,
                CustomerId = datPhong.CustomerId,
                RoomId = datPhong.RoomId,
                CheckInDate = datPhong.CheckInDate,
                CheckOutDate = datPhong.CheckOutDate,
                BookingDate = datPhong.BookingDate,
                Status = datPhong.Status,
                CreateAt = datPhong.CreateAt
            };

            errorMessage = null;
            return datPhongVM;
        }

        public bool UpdateDatPhongStatus(int id, UpdateDatPhongStatusVM request, out string errorMessage)
        {
            try
            {
                var datPhong = _datPhongs.FirstOrDefault(d => d.Id == id);
                if (datPhong == null)
                {
                    errorMessage = "Không tìm thấy đặt phòng với ID này";
                    return false;
                }

                datPhong.Status = request.Status;

                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi khi cập nhật trạng thái đặt phòng");
                errorMessage = "Đã xảy ra lỗi khi cập nhật trạng thái đặt phòng";
                return false;
            }
        }

        public bool DeleteDatPhong(int id, out string errorMessage)
        {
            try
            {
                var datPhong = _datPhongs.FirstOrDefault(d => d.Id == id);
                if (datPhong == null)
                {
                    errorMessage = "Không tìm thấy đặt phòng với ID này";
                    return false;
                }

                _datPhongs.Remove(datPhong);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi khi xóa đặt phòng");
                errorMessage = "Đã xảy ra lỗi khi xóa đặt phòng";
                return false;
            }
        }
    }
}
