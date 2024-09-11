using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingConsole.Model.KhachHang;

namespace HotelBookingConsole.UI
{
    internal class KhachHangUI
    {
        private readonly KhachHangUIService _khachHangService = new();

        public async Task DisplayKhachHangMenu()
        {
            Console.Clear();
            Console.WriteLine("Quản lý Khách hàng:");
            Console.WriteLine("1. Xem tất cả khách hàng");
            Console.WriteLine("2. Xem khách hàng theo ID");
            Console.WriteLine("3. Thêm khách hàng mới");
            Console.WriteLine("4. Sửa thông tin khách hàng");
            Console.WriteLine("5. Xóa khách hàng");
            Console.WriteLine("6. Quay lại");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await _khachHangService.GetAll();
                    break;
                case "2":
                    Console.Write("Nhập ID khách hàng: ");
                    int id = int.Parse(Console.ReadLine());
                    await _khachHangService.GetById(id);
                    break;
                case "3":
                    Console.Write("Nhập tên khách hàng: ");
                    string name = Console.ReadLine();
                    Console.Write("Nhập email: ");
                    string email = Console.ReadLine();
                    Console.Write("Nhập số điện thoại: ");
                    string phone = Console.ReadLine();
                    KhachHangCreateVM newCustomer = new() { Name = name, Email = email, Phone = phone };
                    await _khachHangService.Post(newCustomer);
                    break;
                case "4":
                    Console.Write("Nhập ID khách hàng: ");
                    int updateId = int.Parse(Console.ReadLine());
                    Console.Write("Nhập tên mới: ");
                    string newName = Console.ReadLine();
                    Console.Write("Nhập email mới: ");
                    string newEmail = Console.ReadLine();
                    Console.Write("Nhập số điện thoại mới: ");
                    string newPhone = Console.ReadLine();
                    KhachHangUpdateVM updateCustomer = new() { Name = newName, Email = newEmail, Phone = newPhone };
                    await _khachHangService.Put(updateId, updateCustomer);
                    break;
                case "5":
                    Console.Write("Nhập ID khách hàng: ");
                    int deleteId = int.Parse(Console.ReadLine());
                    await _khachHangService.Delete(deleteId);
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
