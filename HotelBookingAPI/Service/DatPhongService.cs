using FluentValidation;
using HotelBookingAPI.Entities;
using HotelBookingAPI.ViewModel;
using FluentValidation.Results;
using AutoMapper;

namespace HotelBookingAPI.Service
{
    public class DatPhongService : IDatPhongService
    {
        private static List<DatPhong> _datPhongs = new();
        private readonly ILogger<DatPhongService> _logger;
        private readonly IMapper _mapper;

        public DatPhongService(ILogger<DatPhongService> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public bool CreateDatPhong(CreateDatPhongVM request, out string errorMessage)
        {
            try
            {
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
                var datPhongsVM = _mapper.Map<List<DatPhongVM>>(_datPhongs);
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

            var datPhongVM = _mapper.Map<DatPhongVM>(datPhong);
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
