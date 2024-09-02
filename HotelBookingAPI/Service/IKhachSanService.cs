using HotelBookingAPI.ViewModel;

namespace HotelBookingAPI.Service
{
    public interface IKhachSanService
    {
        bool CreateKhachSan(CreateKhachSanVM request, out string errorMessage);
        List<KhachSanVM> GetAllKhachSan(out string errorMessage);
        KhachSanVM GetKhachSanById(int id, out string errorMessage);
        bool UpdateKhachSan(int id, KhachSanUpdateVM request ,out string errorMessage);
        bool DeleteKhachSan(int id, out string errorMessage );
        bool CreatePhong(int khachSanId, CreatePhongVM request, out string errorMessage);
        List<PhongVM> GetPhongByKhachSanId(int khachSanId, out string errorMessage);
    }
}
