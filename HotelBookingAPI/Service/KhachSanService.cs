using FluentValidation;
using FluentValidation.Results;
using HotelBookingAPI.Entities;
using HotelBookingAPI.ViewModel;

namespace HotelBookingAPI.Service
{
    public class KhachSanService : IKhachSanService
    {
        private static List<KhachSan> _khachSans = new();
        private static List<Phong> _phongs = new();
        private readonly ILogger<KhachSanService> _logger;
        private readonly IValidator<CreatePhongVM> _phongValidator;
        private readonly IValidator<CreateKhachSanVM> _khachSanValidator;

        public KhachSanService(ILogger<KhachSanService> logger, IValidator<CreatePhongVM> phongValidator, IValidator<CreateKhachSanVM> khachSanValidator)
        {
            _logger = logger;
            _khachSanValidator = khachSanValidator;
            _phongValidator = phongValidator;
        }
        public bool CreateKhachSan(CreateKhachSanVM request, out string errorMessage)
        {
            try
            {
                ValidationResult result = _khachSanValidator.Validate(request);
                if(!result.IsValid)
                {
                    errorMessage = string.Join(",", result.Errors.Select(e => e.ErrorMessage));
                    return false;
                }
                var khachSan = new KhachSan
                {
                    Id = _khachSans.Any() ? _khachSans.Max(h => h.Id) + 1 : 1,
                    Name = request.Name,
                    Location = request.Location,
                    Rating = request.Rating,
                    Description = request.Description,
                    TotalRoom = request.TotalRoom,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
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
                ValidationResult result = _phongValidator.Validate(request);
                if (!result.IsValid)
                {
                    errorMessage = string.Join(",", result.Errors.Select(e => e.ErrorMessage));
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
            if(_khachSans.Any())
            {
                var khachSanVM =_khachSans.Select(h => new KhachSanVM
                {
                    Id = h.Id,
                    Name = h.Name,
                    Location = h.Location,
                    Rating = h.Rating,
                    Description = h.Description,
                    TotalRoom = h.TotalRoom,
                    CreateAt = h.CreateAt,
                    UpdateAt = h.UpdateAt,
                }
                ).ToList();
                errorMessage = null;
                return khachSanVM;
            }
            errorMessage = "Không có khách sạn nao trong danh sách";
            return null;
        }

        public KhachSanVM GetKhachSanById(int id, out string errorMessage)
        {
            var khachSan = _khachSans.FirstOrDefault(h => h.Id == id);
            if(khachSan == null)
            {
                errorMessage = "Không tìm thấy khách sạn nào với ID này";
                return null;
            }
            var khachSanVM = new KhachSanVM
            {
                Id = khachSan.Id,
                Name = khachSan.Name,
                Location = khachSan.Location,
                Rating = khachSan.Rating,
                Description = khachSan.Description,
                TotalRoom = khachSan.TotalRoom,
                CreateAt = khachSan.CreateAt,
                UpdateAt = khachSan.UpdateAt,
            };
            errorMessage = null;
            return khachSanVM;
        }

        public List<PhongVM> GetPhongByKhachSanId(int khachSanId, out string errorMessage)
        {
            var khachSan = _khachSans.FirstOrDefault(h => h.Id == khachSanId);
            if(khachSan == null)
            {
                errorMessage = "Không tìm thấy khách sạn với Id này";
                return null;
            }    
            var rooms = _phongs.Where(r => r.HotelId == khachSanId).ToList();
            if (!rooms.Any())
            {
                errorMessage = "Không có phòng nào trong khách sạn này.";
                return null;
            }
            var phongsVM = rooms.Select(e => new PhongVM
            {
                Id = e.Id,
                RoomType = e.RoomType,
                Price = e.Price,
                Status = e.Status,
                CheckInTime = e.CheckInTime,
                CheckOutTime = e.CheckOutTime,
            }).ToList();
            errorMessage = null;
            return phongsVM;
        }

        public bool UpdateKhachSan(int id, KhachSanUpdateVM request, out string errorMessage)
        {
            throw new NotImplementedException();
        }
    }
}
