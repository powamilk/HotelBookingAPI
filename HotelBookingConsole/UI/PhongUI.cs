using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingConsole.Model.Phong;

namespace HotelBookingConsole.UI
{
    internal class PhongUI
    {
        private readonly PhongUIService _phongService = new();

        public async Task DisplayPhongMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Quản lý Phòng:");
                Console.WriteLine("1. Hiển thị danh sách phòng theo khách sạn");
                Console.WriteLine("2. Tạo phòng mới");
                Console.WriteLine("3. Cập nhật trạng thái phòng");
                Console.WriteLine("4. Xóa phòng");
                Console.WriteLine("5. Quay lại menu chính");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Nhập ID khách sạn: ");
                        int hotelId = int.Parse(Console.ReadLine());
                        await _phongService.GetAllRoomsByHotel(hotelId);
                        break;

                    case "2":
                        Console.Write("Nhập ID khách sạn: ");
                        int hotelIdForCreate = int.Parse(Console.ReadLine());
                        Console.Write("Nhập loại phòng: ");
                        string roomType = Console.ReadLine();
                        Console.Write("Nhập giá phòng: ");
                        float price = float.Parse(Console.ReadLine());
                        Console.Write("Nhập trạng thái phòng (còn trống/đã đặt): ");
                        string status = Console.ReadLine();
                        Console.Write("Nhập thời gian check-in (HH:mm): ");
                        TimeSpan checkInTime = TimeSpan.Parse(Console.ReadLine());
                        Console.Write("Nhập thời gian check-out (HH:mm): ");
                        TimeSpan checkOutTime = TimeSpan.Parse(Console.ReadLine());

                        PhongCreateVM newRoom = new()
                        {
                            RoomType = roomType,
                            Price = price,
                            Status = status,
                            CheckInTime = checkInTime,
                            CheckOutTime = checkOutTime
                        };

                        await _phongService.CreateRoom(hotelIdForCreate, newRoom);
                        break;

                    case "3":
                        Console.Write("Nhập ID khách sạn: ");
                        int hotelIdForUpdate = int.Parse(Console.ReadLine());
                        Console.Write("Nhập ID phòng: ");
                        int roomIdForUpdate = int.Parse(Console.ReadLine());
                        Console.Write("Nhập trạng thái mới (còn trống/đã đặt): ");
                        string newStatus = Console.ReadLine();

                        await _phongService.UpdateRoomStatus(hotelIdForUpdate, roomIdForUpdate, newStatus);
                        break;

                    case "4":
                        Console.Write("Nhập ID khách sạn: ");
                        int hotelIdForDelete = int.Parse(Console.ReadLine());
                        Console.Write("Nhập ID phòng: ");
                        int roomIdForDelete = int.Parse(Console.ReadLine());
                        await _phongService.DeleteRoom(hotelIdForDelete, roomIdForDelete);
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng thử lại.");
                        break;
                }

                Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
                Console.ReadKey();
            }
        }
    }
}
