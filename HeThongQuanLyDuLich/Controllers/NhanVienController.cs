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
    public class NhanVienController : Controller
    {
        private HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();

        // GET: NhanVien
        [Authorize(Roles ="Admin, NhanVien")]
        public ActionResult Index()
        {
            var nhanViens = db.NhanViens.Include(n => n.ChucVu).Include(n => n.TaiKhoan);
            return View(nhanViens.ToList());
        }

        // GET: NhanVien/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: NhanVien/Create
        public ActionResult Create()
        {
            ViewBag.IDChucVu = new SelectList(db.ChucVus, "IDChucVu", "tenChucVu");
            ViewBag.IDTaiKhoan = new SelectList(db.TaiKhoans, "IDTaiKhoan", "email");
            return View();
        }

        // POST: NhanVien/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDNhanVien,hoTenNhanVien,sdtNhanVien,diaChi,CMND,IDChucVu,IDTaiKhoan")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.NhanViens.Add(nhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDChucVu = new SelectList(db.ChucVus, "IDChucVu", "tenChucVu", nhanVien.IDChucVu);
            ViewBag.IDTaiKhoan = new SelectList(db.TaiKhoans, "IDTaiKhoan", "email", nhanVien.IDTaiKhoan);
            return View(nhanVien);
        }

        // GET: NhanVien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDChucVu = new SelectList(db.ChucVus, "IDChucVu", "tenChucVu", nhanVien.IDChucVu);
            ViewBag.IDTaiKhoan = new SelectList(db.TaiKhoans, "IDTaiKhoan", "email", nhanVien.IDTaiKhoan);
            return View(nhanVien);
        }

        // POST: NhanVien/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDNhanVien,hoTenNhanVien,sdtNhanVien,diaChi,CMND,IDChucVu,IDTaiKhoan")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanVien).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDChucVu = new SelectList(db.ChucVus, "IDChucVu", "tenChucVu", nhanVien.IDChucVu);
            ViewBag.IDTaiKhoan = new SelectList(db.TaiKhoans, "IDTaiKhoan", "email", nhanVien.IDTaiKhoan);
            return View(nhanVien);
        }

        // GET: NhanVien/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NhanVien nhanVien = db.NhanViens.Find(id);
            db.NhanViens.Remove(nhanVien);
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
