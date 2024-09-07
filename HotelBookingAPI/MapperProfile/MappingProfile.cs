using AutoMapper;
using HotelBookingAPI.Entities;
using HotelBookingAPI.ViewModel;

namespace HotelBookingAPI.MapperProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DatPhong, DatPhongVM>();
            CreateMap<KhachHang, KhachHangVM>();
            CreateMap<KhachSan, KhachSanVM>();
            CreateMap<Phong, PhongVM>();
        }
    }
}
