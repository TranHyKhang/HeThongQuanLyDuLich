using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HeThongQuanLyDuLich.Models;
using HeThongQuanLyDuLich.Areas.Admin.Code;
namespace HeThongQuanLyDuLich.Areas.Admin.Controllers
{
    public class TourController : Controller
    {
        private HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();

        // GET: Admin/Tour
        [HttpGet]
        public ActionResult Index()
        {
            var a = SessionHelper.GetSession();
            Console.WriteLine(a);
            var tours = db.Tours.Include(t => t.DichVu).Include(t => t.HanhTrinh).Include(t => t.KhachSan).Include(t => t.KhuyenMai);
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
            ViewBag.IDDichVu = new SelectList(db.DichVus, "IDDichVu", "tenDichVu");
            ViewBag.IDHanhTrinh = new SelectList(db.HanhTrinhs, "IDHanhTrinh", "tenHanhTrinh");
            ViewBag.IDKhachSan = new SelectList(db.KhachSans, "IDKhachSan", "tenKhachSan");
            ViewBag.IDKhuyenMai = new SelectList(db.KhuyenMais, "IDKhuyenMai", "tenKhuyenMai");
            return View();
        }

        // POST: Admin/Tour/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTour,tourName,tourDescription,soLuongKhachToiDa,soLuongKhachHienTai,hinhAnh,tinhTrangTour,IDHanhTrinh,IDDichVu,IDKhachSan,IDKhuyenMai,gia")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                db.Tours.Add(tour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDDichVu = new SelectList(db.DichVus, "IDDichVu", "tenDichVu", tour.IDDichVu);
            ViewBag.IDHanhTrinh = new SelectList(db.HanhTrinhs, "IDHanhTrinh", "tenHanhTrinh", tour.IDHanhTrinh);
            ViewBag.IDKhachSan = new SelectList(db.KhachSans, "IDKhachSan", "tenKhachSan", tour.IDKhachSan);
            ViewBag.IDKhuyenMai = new SelectList(db.KhuyenMais, "IDKhuyenMai", "tenKhuyenMai", tour.IDKhuyenMai);
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
            ViewBag.IDDichVu = new SelectList(db.DichVus, "IDDichVu", "tenDichVu", tour.IDDichVu);
            ViewBag.IDHanhTrinh = new SelectList(db.HanhTrinhs, "IDHanhTrinh", "tenHanhTrinh", tour.IDHanhTrinh);
            ViewBag.IDKhachSan = new SelectList(db.KhachSans, "IDKhachSan", "tenKhachSan", tour.IDKhachSan);
            ViewBag.IDKhuyenMai = new SelectList(db.KhuyenMais, "IDKhuyenMai", "tenKhuyenMai", tour.IDKhuyenMai);
            return View(tour);
        }

        // POST: Admin/Tour/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTour,tourName,tourDescription,soLuongKhachToiDa,soLuongKhachHienTai,hinhAnh,tinhTrangTour,IDHanhTrinh,IDDichVu,IDKhachSan,IDKhuyenMai,gia")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tour).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDDichVu = new SelectList(db.DichVus, "IDDichVu", "tenDichVu", tour.IDDichVu);
            ViewBag.IDHanhTrinh = new SelectList(db.HanhTrinhs, "IDHanhTrinh", "tenHanhTrinh", tour.IDHanhTrinh);
            ViewBag.IDKhachSan = new SelectList(db.KhachSans, "IDKhachSan", "tenKhachSan", tour.IDKhachSan);
            ViewBag.IDKhuyenMai = new SelectList(db.KhuyenMais, "IDKhuyenMai", "tenKhuyenMai", tour.IDKhuyenMai);
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
