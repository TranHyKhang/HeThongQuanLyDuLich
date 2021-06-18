using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HeThongQuanLyDuLich.Models;
using PagedList;
using PagedList.Mvc;

namespace HeThongQuanLyDuLich.Areas.Admin.Controllers
{
    public class VeDatTourController : Controller
    {
        private HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();

        // GET: Admin/VeDatTour
        [Authorize]
        public ActionResult Index(string searchValue, int? page)
        {

            int pageSize = 8;
            int pageNum = (page ?? 1);
            if (searchValue != null)
            {
                string[] temp = searchValue.Split('+');
                string a = string.Join(" ", temp).ToLower();
                return View(db.VeDatTours.Include(v => v.KhachHang).Include(v => v.Tour).ToList().Where(s => s.trangThaiVeDatTour == true).Where(s => s.KhachHang.hoTenKhachHang.ToLower().Contains(a)).ToPagedList(pageNum, pageSize));
            }
            return View(db.VeDatTours.Include(v => v.KhachHang).Include(v => v.Tour).ToList().Where(s => s.trangThaiVeDatTour == true).ToPagedList(pageNum, pageSize));
        }

        [Authorize]
        public ActionResult VeChuaXacNhan(string searchValue, int? page)
        {

            int pageSize = 8;
            int pageNum = (page ?? 1);
            if (searchValue != null)
            {
                string[] temp = searchValue.Split('+');
                string a = string.Join(" ", temp).ToLower();
                return View(db.VeDatTours.Include(v => v.KhachHang).Include(v => v.Tour).ToList().Where(s => s.trangThaiVeDatTour == false).Where(s => s.KhachHang.hoTenKhachHang.ToLower().Contains(a)).ToPagedList(pageNum, pageSize));
            }
            return View(db.VeDatTours.Include(v => v.KhachHang).Include(v => v.Tour).ToList().Where(s => s.trangThaiVeDatTour == false).ToPagedList(pageNum, pageSize));
        }
        // GET: Admin/VeDatTour/Details/5
        [Authorize]
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
        [Authorize]
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
        public ActionResult Create([Bind(Include = "IDVeDatTour,hinhThucThanhToan,trangThaiVeDatTour,IDTour,IDKhachHang,soLuongVeDatTour,ngayDatVe")] VeDatTour veDatTour)
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
        [Authorize]
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
        public ActionResult Edit([Bind(Include = "IDVeDatTour,hinhThucThanhToan,trangThaiVeDatTour,IDTour,IDKhachHang,soLuongVeDatTour,ngayDatVe")] VeDatTour veDatTour)
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
        [Authorize]
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
