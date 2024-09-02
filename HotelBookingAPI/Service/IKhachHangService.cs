using HotelBookingAPI.Entities;
using HotelBookingAPI.ViewModel;

namespace HotelBookingAPI.Service
{
    public interface IKhachHangService
    {
        bool CreateKhachHang(CreateKhachHangVM request, out string errorMessage);
        List<KhachHangVM> GetAllKhachHang(out string errorMessage);
        KhachHangVM GetKhachHangById(int id, out string errorMessage);
        List<DatPhongVM> GetDatPhongByKhachHangId(int khachHangId, out string errorMessage);
        bool DeleteKhachHang(int id, out string errorMessage);
    }
}
