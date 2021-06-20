using HeThongQuanLyDuLich.Areas.Client.Code;
using HeThongQuanLyDuLich.Areas.Client.Models;
using HeThongQuanLyDuLich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace HeThongQuanLyDuLich.Areas.Client.Controllers
{
    public class DatTourController : Controller
    {
        HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();
        Random rnd = new Random();

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
            Tour tour = db.Tours.Where(s => s.IDTour == thongTinDatVe.Tour.IDTour).FirstOrDefault();
            ThongTinDatVe a = new ThongTinDatVe()
            {
                Tour = tour,
                SoLuongKhach = 0
            };
            if (tour.soLuongKhachHienTai + thongTinDatVe.SoLuongKhach <= tour.soLuongKhachToiDa)
            {
                return RedirectToAction("Index", new { id = thongTinDatVe.Tour.IDTour, soLuongKhach = thongTinDatVe.SoLuongKhach });
            } else
            {
                ModelState.AddModelError("VuotQuaSoLuongKhach", "Xin lỗi quý khách, hiện tại tour không đủ chỗ cho số lượng vé quý khách yêu cầu!!!");
                return View("SoLuongVeDatTour", a);
            }
        }

        [HttpPost]
        public ActionResult PostVeDatTour(List<ThongTinDatVe> thongTinDatVe)
        {
            int idTour = thongTinDatVe.ToArray()[0].Tour.IDTour;
            Tour tour = db.Tours.Where(s => s.IDTour == idTour).FirstOrDefault();
            thongTinDatVe.FirstOrDefault().Tour = tour;
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
            foreach (var kh1 in db.KhachHangs)
            {
                if (kh1.IDTaiKhoan == idTaiKhoan)
                {
                    khachHang = kh1;
                }
            }

            tour.soLuongKhachHienTai = tour.soLuongKhachHienTai + thongTinDatVe.Count();
            db.Entry(tour).State = System.Data.Entity.EntityState.Modified;


            //Tinh tong tien
            int tongTienDichVu = 0;
            foreach(var x in tour.DichVus)
            {
                tongTienDichVu += (int)x.giaDichVu;
            }
            int tongTien = (int)tour.gia + tongTienDichVu + (int)tour.KhachSan.giaKhachSan - (int)tour.KhuyenMai.giaTriKhuyenMai;

            int tong = tongTien;
            if (idTaiKhoan != null)
            {
                VeDatTour veDatTourOfMember = new VeDatTour()
                {
                    IDVeDatTour = RandomIDVeDatTour(),
                    hinhThucThanhToan = thongTinDatVe.FirstOrDefault().HinhThucThanhToan,
                    trangThaiVeDatTour = false,
                    IDTour = thongTinDatVe.FirstOrDefault().Tour.IDTour,
                    IDKhachHang = khachHang.IDKhachHang,
                    soLuongVeDatTour = khachHang.IDKhachHang,
                    ngayDatVe = DateTime.Now
                };
                db.VeDatTours.Add(veDatTourOfMember);

                for (int i = 1; i < thongTinDatVe.ToArray().Length; i++)
                {
                    KhachHang kh = new KhachHang()
                    {
                        IDKhachHang = RandomIDKhachHang(),
                        hoTenKhachHang = thongTinDatVe.ToArray()[i].TenKH,
                        sdtKhachHang = thongTinDatVe.ToArray()[i].SoDT,
                        diaChi = thongTinDatVe.ToArray()[i].DiaChi,
                        CMND = thongTinDatVe.ToArray()[i].Cmnd,
                        IDTaiKhoan = null
                    };

                    VeDatTour veDatTour = new VeDatTour()
                    {
                        IDVeDatTour = kh.IDKhachHang + 1,
                        hinhThucThanhToan = thongTinDatVe.FirstOrDefault().HinhThucThanhToan,
                        trangThaiVeDatTour = false,
                        IDTour = thongTinDatVe.FirstOrDefault().Tour.IDTour,
                        IDKhachHang = kh.IDKhachHang,
                        soLuongVeDatTour = veDatTourOfMember.soLuongVeDatTour,
                        ngayDatVe = DateTime.Now

                    };
                    tong += tongTien;
                    db.VeDatTours.Add(veDatTour);
                    db.KhachHangs.Add(kh);
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

            if (ModelState.IsValid)
            {
                db.SaveChanges();

                //Send mail
                DateTime date;
                DateTime dateTime = DateTime.Now;
                date = dateTime.AddDays(3);

                if(khachHang.IDTaiKhoan != null)
                {
                    string content = System.IO.File.ReadAllText(Server.MapPath("~/Areas/Client/template/neworder.html"));
                    content = content.Replace("{{IDVeDatTour}}", tour.IDTour.ToString());
                    content = content.Replace("{{TenKhachHang}}", khachHang.hoTenKhachHang);
                    content = content.Replace("{{Email}}", userName.UserName);
                    content = content.Replace("{{SDT}}", khachHang.sdtKhachHang);
                    content = content.Replace("{{DiaChi}}", khachHang.diaChi);
                    content = content.Replace("{{ThoiHanThanhToan}}", date.ToString());
                    content = content.Replace("{{TongTien}}", tong.ToString());
                    new MailHelper().SendMail(userName.UserName, "Vé đặt tour", content);
                }
                
                return RedirectToAction("PostVeDatTourSuccess", "DatTour");
            }
            return View("Index", thongTinDatVe);

        }

        public ActionResult PostVeDatTourSuccess()
        {
            return View();
        }

        public int RandomIDKhachHang()
        {
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

        public int RandomIDVeDatTour()
        {
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
        
    }
}