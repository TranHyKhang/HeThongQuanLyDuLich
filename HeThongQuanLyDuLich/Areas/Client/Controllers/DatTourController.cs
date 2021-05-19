using HeThongQuanLyDuLich.Areas.Client.Models;
using HeThongQuanLyDuLich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyDuLich.Areas.Client.Controllers
{
    public class DatTourController : Controller
    {
        HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();

        //GET: Client/DatTour
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Where(s => s.IDTour == id).FirstOrDefault();
            if (tour == null)
            {
                return HttpNotFound();
            }
            ThongTinDatVe a = new ThongTinDatVe()
            {
                Tour = tour
            };
            return View(a);
        }

        [HttpPost]
        public ActionResult PostVeDatTour(ThongTinDatVe thongTinDatVe)
        {
            KhachHang kh = new KhachHang()
            {
                IDKhachHang = RandomIDKhachHang(),
                hoTenKhachHang = thongTinDatVe.TenKH,
                sdtKhachHang = thongTinDatVe.SoDT,
                diaChi = thongTinDatVe.DiaChi,
                CMND = thongTinDatVe.Cmnd,
                IDTaiKhoan = 2
            };

            db.KhachHangs.Add(kh);

            VeDatTour veDatTour = new VeDatTour() {
                IDVeDatTour = RandomIDVeDatTour(),
                hinhThucThanhToan = "Tiền mặt",
                trangThaiVeDatTour = false,
                IDTour = thongTinDatVe.Tour.IDTour,
                IDKhachHang = kh.IDKhachHang,
                soLuongVeDatTour = thongTinDatVe.SoLuongKhach
            };

            
            Tour tour = db.Tours.Where(s => s.IDTour == thongTinDatVe.Tour.IDTour).FirstOrDefault();
            //Tour updateTour = new Tour()
            //{
            //    tourName = tour.tourName,
            //    tourDescription = tour.tourDescription,
            //    soLuongKhachToiDa = tour.soLuongKhachToiDa,
            //    soLuongKhachHienTai = tour.soLuongKhachHienTai + thongTinDatVe.SoLuongKhach,
            //    tinhTrangTour = tour.tinhTrangTour,
            //    IDHanhTrinh = tour.IDHanhTrinh,
            //    IDDichVu = tour.IDDichVu,
            //    IDKhachSan = tour.IDKhachSan,
            //    IDKhuyenMai = tour.IDKhuyenMai,
            //    gia = tour.gia
            //};
            if (tour.soLuongKhachHienTai + thongTinDatVe.SoLuongKhach <= tour.soLuongKhachToiDa)
            {
                tour.soLuongKhachHienTai = tour.soLuongKhachHienTai + thongTinDatVe.SoLuongKhach;
                db.Entry(tour).State = System.Data.Entity.EntityState.Modified;
                db.VeDatTours.Add(veDatTour);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");

            } else
            {

                ModelState.AddModelError("", "Số lượng khách đã đủ");
            }
            return View("Index", thongTinDatVe);

        }

        public int RandomIDVeDatTour()
        {
            Random rnd = new Random();
            int num = rnd.Next(1, 1000000);
            foreach (var ve in db.VeDatTours)
            {
                if (ve.IDVeDatTour == num)
                {
                    RandomIDVeDatTour();
                }
            }
            return num;
        }
        public int RandomIDKhachHang()
        {
            Random rnd = new Random();
            int num = rnd.Next(1, 1000000);
            foreach (var kh in db.KhachHangs)
            {
                if (kh.IDKhachHang == num)
                {
                    RandomIDKhachHang();
                }
            }
            return num;
        }

    }
}