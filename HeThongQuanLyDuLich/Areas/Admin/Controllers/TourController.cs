using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HeThongQuanLyDuLich.Models;

namespace HeThongQuanLyDuLich.Areas.Admin.Controllers
{
    public class TourController : Controller
    {
        private HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();

        // GET: Admin/Tour
        [Authorize]
        public ActionResult Index()
        {
            var tours = db.Tours.Include(t => t.HanhTrinh).Include(t => t.KhachSan).Include(t => t.KhuyenMai).Include(t => t.LoaiTour).Include(t => t.DichVu);
            return View(tours.ToList());
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
            ViewBag.IDHanhTrinh = new SelectList(db.HanhTrinhs, "IDHanhTrinh", "tenHanhTrinh");
            ViewBag.IDKhachSan = new SelectList(db.KhachSans, "IDKhachSan", "tenKhachSan");
            ViewBag.IDKhuyenMai = new SelectList(db.KhuyenMais, "IDKhuyenMai", "tenKhuyenMai");
            ViewBag.IDLoaiTour = new SelectList(db.LoaiTours, "IDLoaiTour", "loaiTour1");
            ViewBag.IDDichVu = new SelectList(db.DichVus, "IDDichVu", "tenDichVu");
            return View();
        }

        // POST: Admin/Tour/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTour,tourName,tourDescription,soLuongKhachToiDa,soLuongKhachHienTai,tinhTrangTour,IDHanhTrinh,IDDichVu,IDKhachSan,IDKhuyenMai,gia,IDLoaiTour")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                db.Tours.Add(tour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDHanhTrinh = new SelectList(db.HanhTrinhs, "IDHanhTrinh", "tenHanhTrinh", tour.IDHanhTrinh);
            ViewBag.IDKhachSan = new SelectList(db.KhachSans, "IDKhachSan", "tenKhachSan", tour.IDKhachSan);
            ViewBag.IDKhuyenMai = new SelectList(db.KhuyenMais, "IDKhuyenMai", "tenKhuyenMai", tour.IDKhuyenMai);
            ViewBag.IDLoaiTour = new SelectList(db.LoaiTours, "IDLoaiTour", "loaiTour1", tour.IDLoaiTour);
            ViewBag.IDDichVu = new SelectList(db.DichVus, "IDDichVu", "tenDichVu", tour.IDDichVu);
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
            ViewBag.IDHanhTrinh = new SelectList(db.HanhTrinhs, "IDHanhTrinh", "tenHanhTrinh", tour.IDHanhTrinh);
            ViewBag.IDKhachSan = new SelectList(db.KhachSans, "IDKhachSan", "tenKhachSan", tour.IDKhachSan);
            ViewBag.IDKhuyenMai = new SelectList(db.KhuyenMais, "IDKhuyenMai", "tenKhuyenMai", tour.IDKhuyenMai);
            ViewBag.IDLoaiTour = new SelectList(db.LoaiTours, "IDLoaiTour", "loaiTour1", tour.IDLoaiTour);
            ViewBag.IDDichVu = new SelectList(db.DichVus, "IDDichVu", "tenDichVu", tour.IDDichVu);
            return View(tour);
        }

        // POST: Admin/Tour/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTour,tourName,tourDescription,soLuongKhachToiDa,soLuongKhachHienTai,tinhTrangTour,IDHanhTrinh,IDDichVu,IDKhachSan,IDKhuyenMai,gia,IDLoaiTour")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tour).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDHanhTrinh = new SelectList(db.HanhTrinhs, "IDHanhTrinh", "tenHanhTrinh", tour.IDHanhTrinh);
            ViewBag.IDKhachSan = new SelectList(db.KhachSans, "IDKhachSan", "tenKhachSan", tour.IDKhachSan);
            ViewBag.IDKhuyenMai = new SelectList(db.KhuyenMais, "IDKhuyenMai", "tenKhuyenMai", tour.IDKhuyenMai);
            ViewBag.IDLoaiTour = new SelectList(db.LoaiTours, "IDLoaiTour", "loaiTour1", tour.IDLoaiTour);
            ViewBag.IDDichVu = new SelectList(db.DichVus, "IDDichVu", "tenDichVu", tour.IDDichVu);
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
            Tour tour = db.Tours.Find(id);
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
