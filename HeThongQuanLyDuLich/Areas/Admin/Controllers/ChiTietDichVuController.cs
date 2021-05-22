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
    public class ChiTietDichVuController : Controller
    {
        private HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();

        // GET: Admin/ChiTietDichVu
        public ActionResult Index()
        {
            var chiTietDichVus = db.ChiTietDichVus.Include(c => c.DichVu);
            return View(chiTietDichVus.ToList());
        }

        // GET: Admin/ChiTietDichVu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDichVu chiTietDichVu = db.ChiTietDichVus.Find(id);
            if (chiTietDichVu == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDichVu);
        }

        // GET: Admin/ChiTietDichVu/Create
        public ActionResult Create()
        {
            ViewBag.IDDichVu = new SelectList(db.DichVus, "IDDichVu", "tenDichVu");
            return View();
        }

        // POST: Admin/ChiTietDichVu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDChiTietDichVu,moTaDichVu,IDDichVu,tenChiTietDichVu")] ChiTietDichVu chiTietDichVu)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietDichVus.Add(chiTietDichVu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDDichVu = new SelectList(db.DichVus, "IDDichVu", "tenDichVu", chiTietDichVu.IDDichVu);
            return View(chiTietDichVu);
        }

        // GET: Admin/ChiTietDichVu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDichVu chiTietDichVu = db.ChiTietDichVus.Find(id);
            if (chiTietDichVu == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDDichVu = new SelectList(db.DichVus, "IDDichVu", "tenDichVu", chiTietDichVu.IDDichVu);
            return View(chiTietDichVu);
        }

        // POST: Admin/ChiTietDichVu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDChiTietDichVu,moTaDichVu,IDDichVu,tenChiTietDichVu")] ChiTietDichVu chiTietDichVu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietDichVu).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDDichVu = new SelectList(db.DichVus, "IDDichVu", "tenDichVu", chiTietDichVu.IDDichVu);
            return View(chiTietDichVu);
        }

        // GET: Admin/ChiTietDichVu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDichVu chiTietDichVu = db.ChiTietDichVus.Find(id);
            if (chiTietDichVu == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDichVu);
        }

        // POST: Admin/ChiTietDichVu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietDichVu chiTietDichVu = db.ChiTietDichVus.Find(id);
            db.ChiTietDichVus.Remove(chiTietDichVu);
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
