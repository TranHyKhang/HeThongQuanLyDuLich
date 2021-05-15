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
    public class ChiTietHanhTrinhController : Controller
    {
        private HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();

        // GET: Admin/ChiTietHanhTrinh
        [Authorize]
        public ActionResult Index()
        {
            var chiTietHanhTrinhs = db.ChiTietHanhTrinhs.Include(c => c.HanhTrinh);
            return View(chiTietHanhTrinhs.ToList());
        }

        // GET: Admin/ChiTietHanhTrinh/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHanhTrinh chiTietHanhTrinh = db.ChiTietHanhTrinhs.Find(id);
            if (chiTietHanhTrinh == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHanhTrinh);
        }

        // GET: Admin/ChiTietHanhTrinh/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.IDHanhTrinh = new SelectList(db.HanhTrinhs, "IDHanhTrinh", "tenHanhTrinh");
            return View();
        }

        // POST: Admin/ChiTietHanhTrinh/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDChiTietHanhTrinh,moTa,IDHanhTrinh")] ChiTietHanhTrinh chiTietHanhTrinh)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietHanhTrinhs.Add(chiTietHanhTrinh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDHanhTrinh = new SelectList(db.HanhTrinhs, "IDHanhTrinh", "tenHanhTrinh", chiTietHanhTrinh.IDHanhTrinh);
            return View(chiTietHanhTrinh);
        }

        // GET: Admin/ChiTietHanhTrinh/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHanhTrinh chiTietHanhTrinh = db.ChiTietHanhTrinhs.Find(id);
            if (chiTietHanhTrinh == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDHanhTrinh = new SelectList(db.HanhTrinhs, "IDHanhTrinh", "tenHanhTrinh", chiTietHanhTrinh.IDHanhTrinh);
            return View(chiTietHanhTrinh);
        }

        // POST: Admin/ChiTietHanhTrinh/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDChiTietHanhTrinh,moTa,IDHanhTrinh")] ChiTietHanhTrinh chiTietHanhTrinh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietHanhTrinh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDHanhTrinh = new SelectList(db.HanhTrinhs, "IDHanhTrinh", "tenHanhTrinh", chiTietHanhTrinh.IDHanhTrinh);
            return View(chiTietHanhTrinh);
        }

        // GET: Admin/ChiTietHanhTrinh/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHanhTrinh chiTietHanhTrinh = db.ChiTietHanhTrinhs.Find(id);
            if (chiTietHanhTrinh == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHanhTrinh);
        }

        // POST: Admin/ChiTietHanhTrinh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietHanhTrinh chiTietHanhTrinh = db.ChiTietHanhTrinhs.Find(id);
            db.ChiTietHanhTrinhs.Remove(chiTietHanhTrinh);
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
