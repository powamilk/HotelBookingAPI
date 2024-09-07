using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using HotelBookingAPI.Entities;
using HotelBookingAPI.ViewModel;
using HotelBookingAPI.ViewModel.Validator;

namespace HotelBookingAPI.Service
{
    public class KhachSanService : IKhachSanService
    {
        private static List<KhachSan> _khachSans = new();
        private static List<Phong> _phongs = new();
        private readonly ILogger<KhachSanService> _logger;
        private readonly IMapper _mapper;
        public KhachSanService(ILogger<KhachSanService> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public bool CheckKhachSanExist(int id)
        {
            return _khachSans.Any(h => h.Id == id);
        }

        public bool CreateKhachSan(CreateKhachSanVM request, out string errorMessage)
        {
            try
            {
                var khachSan = new KhachSan
                {
                    Id = _khachSans.Any() ? _khachSans.Max(h => h.Id) + 1 : 1,
                    Name = request.Name,
                    Location = request.Location,
                    Rating = request.Rating,
                    Description = request.Description,
                    TotalRoom = request.TotalRoom,
                    CreateAt = DateTime.Now,  
                };
                _khachSans.Add(khachSan);
                errorMessage = null;
                return true;
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "Da cos loi xay ra khi tao khach san");
                errorMessage = "Da cos loi xay ra khi tao khach san";
                return false;
            }
        }

        public bool CreatePhong(int khachSanId, CreatePhongVM request, out string errorMessage)
        {
            try
            {
                var khachSan = _khachSans.FirstOrDefault(h => h.Id == khachSanId);
                if (khachSan == null)
                {
                    errorMessage = "Khong tim thay khach san voi id nay";
                    return false;
                }
                var phong = new Phong
                {
                    Id = _phongs.Any() ? _phongs.Max(r => r.Id) + 1 : 1,
                    HotelId = khachSanId,
                    RoomType = request.RoomType,
                    Price = request.Price,
                    Status = request.Status,
                    CheckInTime = request.CheckInTime,
                    CheckOutTime = request.CheckOutTime,
                    KhachSan = khachSan,
                };
                _phongs.Add(phong);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "da cos loi xay ra");
                errorMessage = "da cos loi xay ra";
                return false;
            }
        }

        public bool DeleteKhachSan(int id, out string errorMessage)
        {
            try
            {
                var khachSan = _khachSans.FirstOrDefault(h => h.Id == id);
                if (khachSan == null)
                {
                    errorMessage = "Không tìm thấy khách sạn với id này";
                    return false;
                }
                _khachSans.Remove(khachSan);
                errorMessage = null;
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi khi xóa khách sạn");
                errorMessage = "Đã xảy ra lỗi khi xóa khách sạn";
                return false;
            }
        }

        public List<KhachSanVM> GetAllKhachSan(out string errorMessage)
        {
            if (_khachSans.Any())
            {
                var khachSanVM = _mapper.Map<List<KhachSanVM>>(_khachSans);
                errorMessage = null;
                return khachSanVM;
            }
            errorMessage = "Không có khách sạn nào trong danh sách";
            return null;
        }

        public KhachSanVM GetKhachSanById(int id, out string errorMessage)
        {
            var khachSan = _khachSans.FirstOrDefault(h => h.Id == id);
            if (khachSan == null)
            {
                errorMessage = "Không tìm thấy khách sạn với ID này";
                return null;
            }

            var khachSanVM = _mapper.Map<KhachSanVM>(khachSan); 
            errorMessage = null;
            return khachSanVM;
        }

        public List<PhongVM> GetPhongByKhachSanId(int khachSanId, out string errorMessage)
        {
            var rooms = _phongs.Where(r => r.HotelId == khachSanId).ToList();
            if (!rooms.Any())
            {
                errorMessage = "Không có phòng nào trong khách sạn này.";
                return null;
            }

            var phongsVM = _mapper.Map<List<PhongVM>>(rooms); 
            errorMessage = null;
            return phongsVM;
        }

        public bool UpdateKhachSan(int id, KhachSanUpdateVM request, out string errorMessage)
        {
            var khachSan = _khachSans.FirstOrDefault(h => h.Id == id);
            if(khachSan == null)
            {
                errorMessage = "Không tìm thấy khách sạn với Id này";
                return false;
            } 
            khachSan.Name = request.Name;
            khachSan.Location = request.Location;
            khachSan.Rating = request.Rating;
            khachSan.Description = request.Description;
            khachSan.TotalRoom = request.TotalRoom;
            khachSan.UpdateAt = DateTime.Now;

            errorMessage = null;
            return true;
        }
    }
}
