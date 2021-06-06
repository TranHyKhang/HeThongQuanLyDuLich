using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongQuanLyDuLich.Models;
using HeThongQuanLyDuLich.Areas.Client.Models;
using HeThongQuanLyDuLich.Areas.Client.Code;

namespace HeThongQuanLyDuLich.Areas.Client.Controllers
{
    public class HomeController : Controller
    {
        HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();
        // GET: Client/Home
        public ActionResult Index()
        {
            ViewHomeModel homeModel = new ViewHomeModel()
            {
                Tours = db.Tours.ToList(),
                LoaiTours = db.LoaiTours.ToList()
            };
            return View(homeModel);
        }

        public ActionResult DetailTour(int id)
        {
            var tour = db.Tours.Where(s => s.IDTour == id).FirstOrDefault();
            

            ViewDetailModel detailModel = new ViewDetailModel()
            {
                Tour = tour,
                ChiTietHanhTrinh = new List<List<string>>()
            };

            foreach (var hanhTrinh in tour.HanhTrinhs)
            {
                string[] arr = hanhTrinh.moTaHanhTrinh.Split(' ');

                List<string> listString = new List<string>();
                for(int i = 0; i < 1; i++)
                {
                    int start = 0;
                    int end = 0;
                    string temp = "";
                    for(int j = 0; j < arr.Length; j++)
                    {
                        if(arr[j] == "Trưa:" || arr[j] == "Chiều:")
                        {
                            end = (j - start);
                            temp = String.Join(" ", arr.SubArray(start, end));
                            start = j;
                            listString.Add(temp);
                        } 
                        if(j + 1 == arr.Length)
                        {
                            end = (j - start) + 1;
                            temp = String.Join(" ", arr.SubArray(start, end));
                            start = j;
                            listString.Add(temp);
                        }
                        
                    }
                }
                detailModel.ChiTietHanhTrinh.Add(listString);
            }

           
            
            return View(detailModel);
        }


        public ActionResult DanhSachTour(int id)
        {
            LoaiTour loaiTour = db.LoaiTours.Where(s => s.IDLoaiTour == id).FirstOrDefault();

            List<Tour> dsTour = db.Tours.Where(s => s.IDLoaiTour == id).ToList();

            string[] dsGioiThieuLoaiTour = new string[3];
            dsGioiThieuLoaiTour[0] = "mang nhiều dấu ấn lịch sử lâu đời từ thời Vua Hùng dựng nước cho đến các triều đại phong kiến giữ nước. Du lịch Miền Bắc là hành trình hấp dẫn dành cho những ai yêu thích lịch sử, mong muốn tìm hiểu và trải nghiệm những điều mới mẻ mà chỉ Miền Bắc mới có. Đến với Tour du lịch Miền Bắc, tour du lịch hè miền bắc du khách sẽ được tham quan những di tích lịch sử lâu đời, được hóa thân vào các vai Vua chúa, quan đại thần, cung phi, hoàng hậu,.... Quý khách có thể đặt tour Miền Bắc, tour Hè miền bắc, trực tuyến ngay tại website Travel để nhận được nhiều ưu đãi hấp dẫn. Hoặc tham khảo thêm các thông tin khuyến mãi Tour du lịch hè Miền Bắc 2021 ngay bên dưới:";
            dsGioiThieuLoaiTour[1] = "hay còn gọi là Trung Bộ, trải qua tiến trình lịch sử, Miền Trung được xem như trạm trung chuyển, là cầu nối giữa hai miền Nam - Bắc. Với địa hình nhỏ hẹp, phía Tây là dãy núi Trường Sơn, phía Đông giáp biển. Tour Miền Trung, Tour Hè Miền Trung đang trở thành xu hướng du lịch nghỉ dưỡng số một tại Việt Nam bởi nơi đây có nhiều cảnh quan và bãi biển đẹp. Đến với Tour du lịch Miền Trung, Tour Du lịch hè miền trung du khách sẽ được thả hồn vào sóng nước, được tham quan hệ sinh thái động thực vật đa dạng phong phú và khám phá những bãi biển hoang sơ. Để có chuyến đi thêm phần ý nghĩa và trọn vẹn, khách hàng liên hệ đăng ký tour trực tuyến tại Website Travel để nhận được giá ưu đãi tốt nhất nhé! ";
            dsGioiThieuLoaiTour[2] = "là vùng đất mang nhiều hy vọng của những người dân xa quê, là vùng đất trù phú giàu có được thiên nhiên ưu ái ban tặng. Miền Nam chính vì được thiên nhiên ưu ái nên đây cũng là vùng đất được phát triển về du lịch sinh thái. Đến với tour du lịch Miền Nam du khách sẽ được nhiều trải nghiệm du lịch sông nước với chợ Nổi, tham quan vườn Quốc gia Cát Tiên, vườn Quốc gia Tràm Chim,... cùng nhau ngắm nhìn hoàng hôn tại biển Vũng Tàu,... Ngoài ra, đến với Tour Miền Nam du khách còn được trải nghiệm làm kẹo Dừa, và hái trái miệt vườn tại Bến Tre. Để săn được tour du lịch giá rẻ, du khách hãy đăng ký tour trực tiếp tại Website Travel nhé! ";

            string gioiThieu = dsGioiThieuLoaiTour[id - 1];


            ViewDanhSachTourModel danhSachTourModel = new ViewDanhSachTourModel()
            {
                danhSachTour = dsTour,
                loaiTour = loaiTour,
                gioiThieuLoaiTour = gioiThieu
            };
            return View(danhSachTourModel);
        }
    }
}