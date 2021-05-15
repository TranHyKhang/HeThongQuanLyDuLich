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
    public class LoaiHinhAnhController : Controller
    {
        private HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();

        // GET: Admin/LoaiHinhAnh
        [Authorize]
        public ActionResult Index()
        {
            return View(db.LoaiHinhAnhs.ToList());
        }

        // GET: Admin/LoaiHinhAnh/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiHinhAnh loaiHinhAnh = db.LoaiHinhAnhs.Find(id);
            if (loaiHinhAnh == null)
            {
                return HttpNotFound();
            }
            return View(loaiHinhAnh);
        }

        // GET: Admin/LoaiHinhAnh/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiHinhAnh/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDLoaiHinhAnh,loaiHinhAnh1")] LoaiHinhAnh loaiHinhAnh)
        {
            if (ModelState.IsValid)
            {
                db.LoaiHinhAnhs.Add(loaiHinhAnh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiHinhAnh);
        }

        // GET: Admin/LoaiHinhAnh/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiHinhAnh loaiHinhAnh = db.LoaiHinhAnhs.Find(id);
            if (loaiHinhAnh == null)
            {
                return HttpNotFound();
            }
            return View(loaiHinhAnh);
        }

        // POST: Admin/LoaiHinhAnh/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDLoaiHinhAnh,loaiHinhAnh1")] LoaiHinhAnh loaiHinhAnh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiHinhAnh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiHinhAnh);
        }

        // GET: Admin/LoaiHinhAnh/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiHinhAnh loaiHinhAnh = db.LoaiHinhAnhs.Find(id);
            if (loaiHinhAnh == null)
            {
                return HttpNotFound();
            }
            return View(loaiHinhAnh);
        }

        // POST: Admin/LoaiHinhAnh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiHinhAnh loaiHinhAnh = db.LoaiHinhAnhs.Find(id);
            db.LoaiHinhAnhs.Remove(loaiHinhAnh);
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
