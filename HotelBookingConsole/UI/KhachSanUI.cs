using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingConsole.Model.KhachSan;

namespace HotelBookingConsole.UI
{
    internal class KhachSanUI
    {
        private readonly KhachSanUIService _khachSanService = new();

        public async Task DisplayKhachSanMenu()
        {
            Console.Clear();
            Console.WriteLine("Quản lý Khách sạn:");
            Console.WriteLine("1. Xem tất cả khách sạn");
            Console.WriteLine("2. Xem khách sạn theo ID");
            Console.WriteLine("3. Thêm khách sạn mới");
            Console.WriteLine("4. Sửa thông tin khách sạn");
            Console.WriteLine("5. Xóa khách sạn");
            Console.WriteLine("6. Quay lại");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await _khachSanService.GetAll();
                    break;
                case "2":
                    Console.Write("Nhập ID khách sạn: ");
                    int id = int.Parse(Console.ReadLine());
                    await _khachSanService.GetById(id);
                    break;
                case "3":
                    Console.Write("Nhập tên khách sạn: ");
                    string name = Console.ReadLine();
                    Console.Write("Nhập vị trí khách sạn: ");
                    string location = Console.ReadLine();
                    Console.Write("Nhập đánh giá khách sạn (1-5): ");
                    float rating = float.Parse(Console.ReadLine());
                    Console.WriteLine("Nhập mô tả khách sạn");
                    string description = Console.ReadLine();
                    Console.Write("Nhập tổng số phòng: ");
                    int totalRoom = int.Parse(Console.ReadLine());
                    KhachSanCreateVM newHotel = new() { Name = name, Location = location, Rating = rating,Description = description, TotalRoom = totalRoom };
                    await _khachSanService.Post(newHotel);
                    break;
                case "4":
                    Console.Write("Nhập ID khách sạn cần sửa: ");
                    int updateId = int.Parse(Console.ReadLine());
                    Console.Write("Nhập tên mới: ");
                    string newName = Console.ReadLine();
                    Console.Write("Nhập vị trí mới: ");
                    string newLocation = Console.ReadLine();
                    Console.Write("Nhập đánh giá mới (1-5): ");
                    float newRating = float.Parse(Console.ReadLine());
                    Console.WriteLine("Nhập mô tả khách sạn");
                    string newDescription = Console.ReadLine();
                    Console.Write("Nhập tổng số phòng mới: ");
                    int newTotalRoom = int.Parse(Console.ReadLine());
                    KhachSanUpdateVM updateHotel = new() { Name = newName, Location = newLocation, Rating = newRating, Description = newDescription, TotalRoom = newTotalRoom};
                    await _khachSanService.Put(updateId, updateHotel);
                    break;
                case "5":
                    Console.Write("Nhập ID khách sạn cần xóa: ");
                    int deleteId = int.Parse(Console.ReadLine());
                    await _khachSanService.Delete(deleteId);
                    break;
                case "6":
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
