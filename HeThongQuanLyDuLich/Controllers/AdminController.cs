using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HeThongQuanLyDuLich.Models;

namespace HeThongQuanLyDuLich.Controllers
{
    public class AdminController : Controller
    {

        private HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }
        

        // GET: Tour
        public ActionResult Tour()
        {
            var tours = db.Tours.Include(t => t.DichVu).Include(t => t.HanhTrinh).Include(t => t.KhachSan).Include(t => t.KhuyenMai);
            return View("Tour/Tour",tours.ToList());
        }

        // GET: Tour/Details/5
        public ActionResult DetailsTour(int? id)
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
            return View("Tour/DetailsTour",tour);
        }

        // GET: Tour/Create
        public ActionResult CreateTour()
        {
            ViewBag.IDDichVu = new SelectList(db.DichVus, "IDDichVu", "tenDichVu");
            ViewBag.IDHanhTrinh = new SelectList(db.HanhTrinhs, "IDHanhTrinh", "tenHanhTrinh");
            ViewBag.IDKhachSan = new SelectList(db.KhachSans, "IDKhachSan", "tenKhachSan");
            ViewBag.IDKhuyenMai = new SelectList(db.KhuyenMais, "IDKhuyenMai", "tenKhuyenMai");
            return View("Tour/CreateTour");
        }

        // POST: Tour/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTour([Bind(Include = "IDTour,tourName,tourDescription,soLuongKhachToiDa,soLuongKhachHienTai,hinhAnh,tinhTrangTour,IDHanhTrinh,IDDichVu,IDKhachSan,IDKhuyenMai,gia")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                db.Tours.Add(tour);
                db.SaveChanges();
                return RedirectToAction("Tour/Tour");
            }

            ViewBag.IDDichVu = new SelectList(db.DichVus, "IDDichVu", "tenDichVu", tour.IDDichVu);
            ViewBag.IDHanhTrinh = new SelectList(db.HanhTrinhs, "IDHanhTrinh", "tenHanhTrinh", tour.IDHanhTrinh);
            ViewBag.IDKhachSan = new SelectList(db.KhachSans, "IDKhachSan", "tenKhachSan", tour.IDKhachSan);
            ViewBag.IDKhuyenMai = new SelectList(db.KhuyenMais, "IDKhuyenMai", "tenKhuyenMai", tour.IDKhuyenMai);
            return View("Tour/CreateTour",tour);
        }

        // GET: Tour/Edit/5
        public ActionResult EditTour(int? id)
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
            return View("Tour/EditTour",tour);
        }

        // POST: Tour/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTour([Bind(Include = "IDTour,tourName,tourDescription,soLuongKhachToiDa,soLuongKhachHienTai,hinhAnh,tinhTrangTour,IDHanhTrinh,IDDichVu,IDKhachSan,IDKhuyenMai,gia")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tour).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Tour/Tour");
            }
            ViewBag.IDDichVu = new SelectList(db.DichVus, "IDDichVu", "tenDichVu", tour.IDDichVu);
            ViewBag.IDHanhTrinh = new SelectList(db.HanhTrinhs, "IDHanhTrinh", "tenHanhTrinh", tour.IDHanhTrinh);
            ViewBag.IDKhachSan = new SelectList(db.KhachSans, "IDKhachSan", "tenKhachSan", tour.IDKhachSan);
            ViewBag.IDKhuyenMai = new SelectList(db.KhuyenMais, "IDKhuyenMai", "tenKhuyenMai", tour.IDKhuyenMai);
            return View("Tour/EditTour",tour);
        }

        // GET: Tour/Delete/5
        public ActionResult DeleteTour(int? id)
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
            return View("Tour/DeleteTour",tour);
        }

        // POST: Tour/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tour tour = db.Tours.Find(id);
            db.Tours.Remove(tour);
            db.SaveChanges();
            return RedirectToAction("Tour/Tour");
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