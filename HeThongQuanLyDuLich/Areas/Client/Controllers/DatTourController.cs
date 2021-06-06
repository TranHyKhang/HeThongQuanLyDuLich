using HeThongQuanLyDuLich.Areas.Client.Code;
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
        public ActionResult Index(int id, int soLuongKhach)
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
                Tour = tour,
                SoLuongKhach = soLuongKhach
            };
            
            List<ThongTinDatVe> listThongTinDatVe = new List<ThongTinDatVe>();

            var userName = SessionHelper.GetSession();
            
            int? idTaiKhoan = null;

            if(userName != null)
            {
                foreach (var x in db.TaiKhoans)
                {
                    if (x.email == userName.UserName)
                    {
                        idTaiKhoan = x.IDTaiKhoan;

                    }
                }
            }
            
            if(idTaiKhoan != null)
            {
                KhachHang khachHang = null;
                foreach (var kh in db.KhachHangs)
                {
                    if (kh.IDTaiKhoan == idTaiKhoan)
                    {
                        khachHang = kh;
                    }
                }
                ThongTinDatVe b = new ThongTinDatVe()
                {
                    TenKH = khachHang.hoTenKhachHang,
                    SoDT = khachHang.sdtKhachHang,
                    DiaChi = khachHang.diaChi,
                    Cmnd = khachHang.CMND,
                    Tour = tour,
                    SoLuongKhach = soLuongKhach
                };

                listThongTinDatVe.Add(b);
                for (int i = 0; i < soLuongKhach - 1; i++)
                {
                    listThongTinDatVe.Add(a);
                }
                return View(listThongTinDatVe);
            }
            else
            {
                for (int i = 0; i < soLuongKhach; i++)
                {
                    listThongTinDatVe.Add(a);
                }
                return View(listThongTinDatVe);
            }
           
        }

        public ActionResult SoLuongVeDatTour(int? id)
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
                Tour = tour,
                SoLuongKhach = 0
            };
            return View(a);
        }

        [HttpPost]
        public ActionResult PostSoLuongVeDatTour(ThongTinDatVe thongTinDatVe)
        {
            return RedirectToAction("Index", new { id = thongTinDatVe.Tour.IDTour, soLuongKhach = thongTinDatVe.SoLuongKhach });
        }

        [HttpPost]
        public ActionResult PostVeDatTour(List<ThongTinDatVe> thongTinDatVe)
        {

            int idTour = thongTinDatVe.ToArray()[0].Tour.IDTour;
            Tour tour = db.Tours.Where(s => s.IDTour == idTour).FirstOrDefault();
            
            var userName = SessionHelper.GetSession();

            int? idTaiKhoan = null;

            if (userName != null)
            {
                foreach (var x in db.TaiKhoans)
                {
                    if (x.email == userName.UserName)
                    {
                        idTaiKhoan = x.IDTaiKhoan;

                    }
                }
            }
            KhachHang khachHang = null;
            foreach (var kh in db.KhachHangs)
            {
                if (kh.IDTaiKhoan == idTaiKhoan)
                {
                    khachHang = kh;
                }
            }


            if (tour.soLuongKhachHienTai + thongTinDatVe.Count() <= tour.soLuongKhachToiDa)
            {
                tour.soLuongKhachHienTai = tour.soLuongKhachHienTai + thongTinDatVe.Count();
                db.Entry(tour).State = System.Data.Entity.EntityState.Modified;

                if(idTaiKhoan != null)
                {
                    VeDatTour veDatTourOfMember = new VeDatTour()
                    {
                        IDVeDatTour = RandomIDVeDatTour() + 3,
                        hinhThucThanhToan = thongTinDatVe.FirstOrDefault().HinhThucThanhToan,
                        trangThaiVeDatTour = false,
                        IDTour = thongTinDatVe.FirstOrDefault().Tour.IDTour,
                        IDKhachHang = khachHang.IDKhachHang,
                        soLuongVeDatTour = khachHang.IDKhachHang,
                        ngayDatVe = DateTime.Now
                    };
                    db.VeDatTours.Add(veDatTourOfMember);



                    for(int i = 1; i < thongTinDatVe.ToArray().Length; i++)
                    {
                        KhachHang kh = new KhachHang()
                        {
                            IDKhachHang = RandomIDKhachHang() + 2,
                            hoTenKhachHang = thongTinDatVe.ToArray()[i].TenKH,
                            sdtKhachHang = thongTinDatVe.ToArray()[i].SoDT,
                            diaChi = thongTinDatVe.ToArray()[i].DiaChi,
                            CMND = thongTinDatVe.ToArray()[i].Cmnd,
                            IDTaiKhoan = null
                        };

                        db.KhachHangs.Add(kh);

                        VeDatTour veDatTour = new VeDatTour()
                        {
                            IDVeDatTour = RandomIDVeDatTour() + 1,
                            hinhThucThanhToan = thongTinDatVe.FirstOrDefault().HinhThucThanhToan,
                            trangThaiVeDatTour = false,
                            IDTour = thongTinDatVe.FirstOrDefault().Tour.IDTour,
                            IDKhachHang = kh.IDKhachHang,
                            soLuongVeDatTour = khachHang.IDKhachHang,
                            ngayDatVe = DateTime.Now

                        };
                        db.VeDatTours.Add(veDatTour);

                    }
                    db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var x in thongTinDatVe)
                    {
                        KhachHang kh = new KhachHang()
                        {
                            IDKhachHang = RandomIDKhachHang() + 1,
                            hoTenKhachHang = x.TenKH,
                            sdtKhachHang = x.SoDT,
                            diaChi = x.DiaChi,
                            CMND = x.Cmnd,
                            IDTaiKhoan = null
                        };

                        db.KhachHangs.Add(kh);

                        VeDatTour veDatTour = new VeDatTour()
                        {
                            IDVeDatTour = RandomIDVeDatTour() + 1,
                            hinhThucThanhToan = thongTinDatVe.FirstOrDefault().HinhThucThanhToan,
                            trangThaiVeDatTour = false,
                            IDTour = thongTinDatVe.FirstOrDefault().Tour.IDTour,
                            IDKhachHang = kh.IDKhachHang,
                            soLuongVeDatTour = kh.IDKhachHang,
                            ngayDatVe = DateTime.Now

                        };
                        db.VeDatTours.Add(veDatTour);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }


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