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
    public class VeDatTourController : Controller
    {
        private HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();

        // GET: Admin/VeDatTour
        public ActionResult Index()
        {
            var veDatTours = db.VeDatTours.Include(v => v.KhachHang).Include(v => v.Tour).Where(s => s.trangThaiVeDatTour == true);
            return View(veDatTours.ToList());
        }

        public ActionResult VeChuaXacNhan()
        {
            var veDatTours = db.VeDatTours.Include(v => v.KhachHang).Include(v => v.Tour).Where(s => s.trangThaiVeDatTour == false);
            return View(veDatTours.ToList());
        }

        // GET: Admin/VeDatTour/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VeDatTour veDatTour = db.VeDatTours.Find(id);
            if (veDatTour == null)
            {
                return HttpNotFound();
            }
            return View(veDatTour);
        }

        // GET: Admin/VeDatTour/Create
        public ActionResult Create()
        {
            ViewBag.IDKhachHang = new SelectList(db.KhachHangs, "IDKhachHang", "hoTenKhachHang");
            ViewBag.IDTour = new SelectList(db.Tours, "IDTour", "tourName");
            return View();
        }

        // POST: Admin/VeDatTour/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDVeDatTour,hinhThucThanhToan,trangThaiVeDatTour,IDTour,IDKhachHang,soLuongVeDatTour")] VeDatTour veDatTour)
        {
            if (ModelState.IsValid)
            {
                db.VeDatTours.Add(veDatTour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDKhachHang = new SelectList(db.KhachHangs, "IDKhachHang", "hoTenKhachHang", veDatTour.IDKhachHang);
            ViewBag.IDTour = new SelectList(db.Tours, "IDTour", "tourName", veDatTour.IDTour);
            return View(veDatTour);
        }

        // GET: Admin/VeDatTour/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VeDatTour veDatTour = db.VeDatTours.Find(id);
            if (veDatTour == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDKhachHang = new SelectList(db.KhachHangs, "IDKhachHang", "hoTenKhachHang", veDatTour.IDKhachHang);
            ViewBag.IDTour = new SelectList(db.Tours, "IDTour", "tourName", veDatTour.IDTour);
            return View(veDatTour);
        }

        // POST: Admin/VeDatTour/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDVeDatTour,hinhThucThanhToan,trangThaiVeDatTour,IDTour,IDKhachHang,soLuongVeDatTour")] VeDatTour veDatTour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(veDatTour).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDKhachHang = new SelectList(db.KhachHangs, "IDKhachHang", "hoTenKhachHang", veDatTour.IDKhachHang);
            ViewBag.IDTour = new SelectList(db.Tours, "IDTour", "tourName", veDatTour.IDTour);
            return View(veDatTour);
        }

        // GET: Admin/VeDatTour/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VeDatTour veDatTour = db.VeDatTours.Find(id);
            if (veDatTour == null)
            {
                return HttpNotFound();
            }
            return View(veDatTour);
        }

        // POST: Admin/VeDatTour/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VeDatTour veDatTour = db.VeDatTours.Find(id);
            Tour tour = db.Tours.Where(s => s.IDTour == veDatTour.IDTour).FirstOrDefault();
            tour.soLuongKhachHienTai -= veDatTour.soLuongVeDatTour;
            db.Entry(tour).State = System.Data.Entity.EntityState.Modified;
            db.VeDatTours.Remove(veDatTour);
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
