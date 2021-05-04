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
    public class HanhTrinhController : Controller
    {
        private HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();

        // GET: Admin/HanhTrinh
        public ActionResult Index()
        {
            return View(db.HanhTrinhs.ToList());
        }

        // GET: Admin/HanhTrinh/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HanhTrinh hanhTrinh = db.HanhTrinhs.Find(id);
            if (hanhTrinh == null)
            {
                return HttpNotFound();
            }
            return View(hanhTrinh);
        }

        // GET: Admin/HanhTrinh/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/HanhTrinh/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDHanhTrinh,tenHanhTrinh,diemKhoiHanh,diemKetThuc,thoiGianKhoiHanh,thoiGianKetThuc,moTa")] HanhTrinh hanhTrinh)
        {
            if (ModelState.IsValid)
            {
                db.HanhTrinhs.Add(hanhTrinh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hanhTrinh);
        }

        // GET: Admin/HanhTrinh/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HanhTrinh hanhTrinh = db.HanhTrinhs.Find(id);
            if (hanhTrinh == null)
            {
                return HttpNotFound();
            }
            return View(hanhTrinh);
        }

        // POST: Admin/HanhTrinh/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDHanhTrinh,tenHanhTrinh,diemKhoiHanh,diemKetThuc,thoiGianKhoiHanh,thoiGianKetThuc,moTa")] HanhTrinh hanhTrinh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hanhTrinh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hanhTrinh);
        }

        // GET: Admin/HanhTrinh/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HanhTrinh hanhTrinh = db.HanhTrinhs.Find(id);
            if (hanhTrinh == null)
            {
                return HttpNotFound();
            }
            return View(hanhTrinh);
        }

        // POST: Admin/HanhTrinh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HanhTrinh hanhTrinh = db.HanhTrinhs.Find(id);
            db.HanhTrinhs.Remove(hanhTrinh);
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
