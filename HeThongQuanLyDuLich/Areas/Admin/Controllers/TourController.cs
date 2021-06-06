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

namespace HeThongQuanLyDuLich.Areas.Admin.Controllers
{
    public class TourController : Controller
    {
        private HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();

        // GET: Admin/Tour
        public ActionResult Index()
        {
            ViewTourModel a = new ViewTourModel();
            var tours = db.Tours.Include(t => t.KhachSan).Include(t => t.KhuyenMai).Include(t => t.LoaiTour);
            return View(tours.ToList());
        }

        // GET: Admin/Tour/Details/5
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
        public ActionResult Create()
        {
            ViewBag.IDKhachSan = db.KhachSans.ToList();
            ViewBag.IDKhuyenMai = db.KhuyenMais.ToList();
            ViewBag.IDLoaiTour = db.LoaiTours.ToList();
            ViewBag.ListDichVu = db.DichVus;
            ViewTourModel thongTinTour = new ViewTourModel()
            {
                Tour = new Tour(),
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
            var newArrDichVU = db.DichVus.ToArray();
            for(int i = 0; i < thongTinTour.dsDichVuDuoChon.Length; i++)
            {
                if(thongTinTour.dsDichVuDuoChon[i] == true)
                {
                    tour.DichVus.Add(newArrDichVU[i]);
                }
            }

            foreach (var hanhTrinh in db.HanhTrinhs)
            {
                if (hanhTrinh.tenHanhTrinh == tour.tourName)
                {
                    tour.HanhTrinhs.Add(hanhTrinh);
                }
            }

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
            Tour tour = db.Tours.Include(a => a.DichVus).Include(a => a.HinhAnhs).Include(a => a.KhachSan). Include(a => a.KhuyenMai).Include(a => a.LoaiTour).FirstOrDefault(s => s.IDTour == id);
           
            db.Tours.Remove(tour);
            db.SaveChanges();
            return RedirectToAction("Index");
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
