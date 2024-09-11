using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingConsole.Model.DatPhong;

namespace HotelBookingConsole.UI
{
    internal class DatPhongUI
    {
        private readonly DatPhongUIService _datPhongService = new();

        public async Task DisplayDatPhongMenu()
        {
            Console.Clear();
            Console.WriteLine("Quản lý Đặt phòng:");
            Console.WriteLine("1. Xem tất cả đặt phòng");
            Console.WriteLine("2. Thêm đặt phòng mới");
            Console.WriteLine("3. Sửa trạng thái đặt phòng");
            Console.WriteLine("4. Xóa đặt phòng");
            Console.WriteLine("5. Quay lại");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await _datPhongService.GetAll();
                    break;
                case "2":
                    Console.Write("Nhập ID khách hàng: ");
                    int customerId = int.Parse(Console.ReadLine());
                    Console.Write("Nhập ID phòng: ");
                    int roomId = int.Parse(Console.ReadLine());
                    Console.Write("Nhập ngày nhận phòng (yyyy-mm-dd): ");
                    DateTime checkInDate = DateTime.Parse(Console.ReadLine());
                    Console.Write("Nhập ngày trả phòng (yyyy-mm-dd): ");
                    DateTime checkOutDate = DateTime.Parse(Console.ReadLine());
                    DatPhongCreateVM newBooking = new() { CustomerId = customerId, RoomId = roomId, CheckInDate = checkInDate, CheckOutDate = checkOutDate };
                    await _datPhongService.Post(newBooking);
                    break;
                case "3":
                    Console.Write("Nhập ID đặt phòng: ");
                    int bookingId = int.Parse(Console.ReadLine());
                    Console.Write("Nhập trạng thái mới (đã xác nhận/hủy): ");
                    string newStatus = Console.ReadLine();
                    DatPhongUpdateVM updateBooking = new() { Status = newStatus };
                    await _datPhongService.Put(bookingId, updateBooking);
                    break;
                case "4":
                    Console.Write("Nhập ID đặt phòng: ");
                    int deleteBookingId = int.Parse(Console.ReadLine());
                    await _datPhongService.Delete(deleteBookingId);
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                    break;
            }

            Console.WriteLine("Nhấn phím bất kỳ để tiếp tục...");
            Console.ReadKey();
        }
    }
}
