using HotelBookingConsole.UI;
using System.Text;

class Program
{
    static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Chào mừng đến hệ thống quản lý khách sạn!");
            Console.WriteLine("1. Quản lý Khách sạn");
            Console.WriteLine("2. Quản lý Phòng");
            Console.WriteLine("3. Quản lý Khách hàng");
            Console.WriteLine("4. Quản lý Đặt phòng");
            Console.WriteLine("5. Thoát");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var khachSanUI = new KhachSanUI();
                    await khachSanUI.DisplayKhachSanMenu();
                    break;
                case "2":
                    var phongUI = new PhongUI();
                    await phongUI.DisplayPhongMenu();
                    break;
                case "3":
                    var khachHangUI = new KhachHangUI();
                    await khachHangUI.DisplayKhachHangMenu();
                    break;
                case "4":
                    var datPhongUI = new DatPhongUI();
                    await datPhongUI.DisplayDatPhongMenu();
                    break;
                case "5":
                    Console.WriteLine("Thoát chương trình. Tạm biệt!");
                    return;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng thử lại.");
                    break;
            }

            Console.ReadKey();
        }
    }
}
