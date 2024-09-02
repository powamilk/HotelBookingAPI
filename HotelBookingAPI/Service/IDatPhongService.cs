using HotelBookingAPI.ViewModel;

namespace HotelBookingAPI.Service
{
    public interface IDatPhongService
    {
        bool CreateDatPhong(CreateDatPhongVM request, out string errorMessage);
        List<DatPhongVM> GetAllDatPhong(out string errorMessage);
        DatPhongVM GetDatPhongById(int id, out string errorMessage);
        bool UpdateDatPhongStatus(int datPhongId, UpdateDatPhongStatusVM request, out string errorMessage);
        bool DeleteDatPhong(int id, out string errorMessage);
    }
}
