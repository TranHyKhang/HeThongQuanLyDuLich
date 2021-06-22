using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HeThongQuanLyDuLich.Models;
using HeThongQuanLyDuLich.Areas.Admin.Models;
using PagedList;
using PagedList.Mvc;

namespace HeThongQuanLyDuLich.Areas.Admin.Controllers
{
    public class TourController : Controller
    {
        private HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();
        Random rnd = new Random();

        // GET: Admin/Tour
        [Authorize]
        public ActionResult Index(int? page, string searchValue)
        {
            List<Tour> tours = new List<Tour>();

            if (searchValue == null)
            {
                tours = db.Tours.Include(t => t.HanhTrinhs).Include(t => t.KhachSan).Include(t => t.KhuyenMai).Include(t => t.LoaiTour).ToList();
            }
            else
            {
                string[] temp = searchValue.Split('+');
                string a = string.Join(" ", temp).ToLower();
                tours = db.Tours.Include(t => t.HanhTrinhs).Include(t => t.KhachSan).Include(t => t.KhuyenMai).Include(t => t.LoaiTour).ToList().Where(s => s.tourName.ToLower().Contains(a)).ToList();
            }
            
            foreach(var x in tours)
            {
                x.tourDescription = x.tourDescription.Substring(0, 50) + "...";
            }

            int pageSize = 8;
            int pageNum = (page ?? 1);

            return View(tours.ToPagedList(pageNum, pageSize));
        }

        [Authorize]
        public ActionResult DanhSachKhachHang(int? id, int? page, string searchValue)
        {
            ViewDanhSachVeDatTourModel model = new ViewDanhSachVeDatTourModel();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            

            int pageSize = 8;
            int pageNum = (page ?? 1);

            Tour tour = db.Tours.Find(id);
            if (searchValue != null)
            {
                string[] temp = searchValue.Split('+');
                string a = string.Join(" ", temp).ToLower();
                return View(tour.VeDatTours.Where(s => s.KhachHang.hoTenKhachHang.ToLower().Contains(a)).ToPagedList(pageNum, pageSize));
            }
            return View(tour.VeDatTours.ToPagedList(pageNum, pageSize));


        }

        // GET: Admin/Tour/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // GET: Admin/Tour/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.IDKhachSan = db.KhachSans.ToList();
            ViewBag.IDKhuyenMai = db.KhuyenMais.ToList();
            ViewBag.IDLoaiTour = db.LoaiTours.ToList();
            ViewBag.ListDichVu = db.DichVus;
            ViewTourModel thongTinTour = new ViewTourModel()
            {
                Tour = new Tour(),
                listDichVu = db.DichVus.ToList(),
                dsDichVuDuoChon = new bool[db.DichVus.Count()]
            };
           
            return View(thongTinTour);
        }

        // POST: Admin/Tour/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "IDTour,tourName,tourDescription,soLuongKhachToiDa,soLuongKhachHienTai,tinhTrangTour,IDKhachSan,IDKhuyenMai,gia,IDLoaiTour")]
        //Tour tour
        public ActionResult Create(ViewTourModel thongTinTour)
        {
            Console.WriteLine(thongTinTour);
            Tour tour = thongTinTour.Tour;
            tour.IDTour = RandomTourID();
            var newArrDichVU = db.DichVus.ToArray();
            for(int i = 0; i < thongTinTour.dsDichVuDuoChon.Length; i++)
            {
                if(thongTinTour.dsDichVuDuoChon[i] == true)
                {
                    tour.DichVus.Add(newArrDichVU[i]);
                }
            }

            //List<HanhTrinh> listHanhTrinh = new List<HanhTrinh>();

            //foreach (var hanhTrinh in db.HanhTrinhs)
            //{
            //    if (hanhTrinh.tenHanhTrinh == tour.tourName)
            //    {
            //        tour.HanhTrinhs.Add(hanhTrinh);
            //    }
            //}
            

            if (ModelState.IsValid)
            {
                db.Tours.Add(tour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDKhachSan = new SelectList(db.KhachSans, "IDKhachSan", "tenKhachSan", thongTinTour.Tour.IDKhachSan);
            ViewBag.IDKhuyenMai = new SelectList(db.KhuyenMais, "IDKhuyenMai", "tenKhuyenMai", thongTinTour.Tour.IDKhuyenMai);
            ViewBag.IDLoaiTour = new SelectList(db.LoaiTours, "IDLoaiTour", "loaiTour1", thongTinTour.Tour.IDLoaiTour);
            return View(tour);
        }

        // GET: Admin/Tour/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDKhachSan = new SelectList(db.KhachSans, "IDKhachSan", "tenKhachSan", tour.IDKhachSan);
            ViewBag.IDKhuyenMai = new SelectList(db.KhuyenMais, "IDKhuyenMai", "tenKhuyenMai", tour.IDKhuyenMai);
            ViewBag.IDLoaiTour = new SelectList(db.LoaiTours, "IDLoaiTour", "loaiTour1", tour.IDLoaiTour);
            return View(tour);
        }

        // POST: Admin/Tour/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTour,tourName,tourDescription,soLuongKhachToiDa,soLuongKhachHienTai,tinhTrangTour,IDKhachSan,IDKhuyenMai,gia,IDLoaiTour")] Tour tour)
        {
            List<HanhTrinh> listHanhTrinh = new List<HanhTrinh>();

            foreach (var hanhTrinh in db.HanhTrinhs)
            {
                if (hanhTrinh.tenHanhTrinh == tour.tourName)
                {
                    tour.HanhTrinhs.Add(hanhTrinh);
                }
            }
            
            if (ModelState.IsValid)
            {
                db.Entry(tour).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDKhachSan = new SelectList(db.KhachSans, "IDKhachSan", "tenKhachSan", tour.IDKhachSan);
            ViewBag.IDKhuyenMai = new SelectList(db.KhuyenMais, "IDKhuyenMai", "tenKhuyenMai", tour.IDKhuyenMai);
            ViewBag.IDLoaiTour = new SelectList(db.LoaiTours, "IDLoaiTour", "loaiTour1", tour.IDLoaiTour);
            return View(tour);
        }

        // GET: Admin/Tour/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Find(id);
            if (tour == null)
            {
                return HttpNotFound();
            }
            return View(tour);
        }

        // POST: Admin/Tour/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tour tour = db.Tours.Include(a => a.DichVus).Include(a=> a.HanhTrinhs).Include(a => a.HinhAnhs).Include(a => a.KhachSan). Include(a => a.KhuyenMai).Include(a => a.LoaiTour).FirstOrDefault(s => s.IDTour == id);
           
            foreach(var x in db.HinhAnhs)
            {
                if(x.Tour.tourName == tour.tourName)
                {
                    db.HinhAnhs.Remove(x);
                }
            }

            foreach(var x in db.HanhTrinhs)
            {
                if(x.tenHanhTrinh == tour.tourName)
                {
                    db.HanhTrinhs.Remove(x);
                }
            }

            db.Tours.Remove(tour);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public int RandomTourID()
        {
            int num = rnd.Next(1, 10000);
            foreach(var x in db.Tours)
            {
                if(x.IDTour == num)
                {
                    RandomTourID();
                }
            }
            return num;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
