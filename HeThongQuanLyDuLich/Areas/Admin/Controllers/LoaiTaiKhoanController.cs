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
    public class LoaiTaiKhoanController : Controller
    {
        private HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();

        // GET: Admin/LoaiTaiKhoan
        [Authorize]
        public ActionResult Index()
        {
            return View(db.LoaiTaiKhoans.ToList());
        }

        // GET: Admin/LoaiTaiKhoan/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiTaiKhoan loaiTaiKhoan = db.LoaiTaiKhoans.Find(id);
            if (loaiTaiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(loaiTaiKhoan);
        }

        // GET: Admin/LoaiTaiKhoan/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiTaiKhoan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDLoaiTaiKhoan,loaiTaiKhoan1")] LoaiTaiKhoan loaiTaiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.LoaiTaiKhoans.Add(loaiTaiKhoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiTaiKhoan);
        }

        // GET: Admin/LoaiTaiKhoan/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiTaiKhoan loaiTaiKhoan = db.LoaiTaiKhoans.Find(id);
            if (loaiTaiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(loaiTaiKhoan);
        }

        // POST: Admin/LoaiTaiKhoan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDLoaiTaiKhoan,loaiTaiKhoan1")] LoaiTaiKhoan loaiTaiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiTaiKhoan).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiTaiKhoan);
        }

        // GET: Admin/LoaiTaiKhoan/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiTaiKhoan loaiTaiKhoan = db.LoaiTaiKhoans.Find(id);
            if (loaiTaiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(loaiTaiKhoan);
        }

        // POST: Admin/LoaiTaiKhoan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiTaiKhoan loaiTaiKhoan = db.LoaiTaiKhoans.Find(id);
            db.LoaiTaiKhoans.Remove(loaiTaiKhoan);
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
