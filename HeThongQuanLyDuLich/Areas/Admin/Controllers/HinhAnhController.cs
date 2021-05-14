using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HeThongQuanLyDuLich.Areas.Admin.Code;
using HeThongQuanLyDuLich.Models;

namespace HeThongQuanLyDuLich.Areas.Admin.Controllers
{
    public class HinhAnhController : Controller
    {
        private HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();

        // GET: Admin/HinhAnh
        [Authorize]
        public ActionResult Index()
        {
            var hinhAnhs = db.HinhAnhs.Include(h => h.Tour).Include(h => h.LoaiHinhAnh);
            return View(hinhAnhs.ToList());
        }

        // GET: Admin/HinhAnh/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhAnh hinhAnh = db.HinhAnhs.Find(id);
            if (hinhAnh == null)
            {
                return HttpNotFound();
            }
            return View(hinhAnh);
        }

        // GET: Admin/HinhAnh/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.IDTour = new SelectList(db.Tours, "IDTour", "tourName");
            ViewBag.IDLoaiHinhAnh = new SelectList(db.LoaiHinhAnhs, "IDLoaiHinhAnh", "loaiHinhAnh1");
            return View();
        }

        // POST: Admin/HinhAnh/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDHinhAnh,imageUrl,IDTour,IDLoaiHinhAnh")] HinhAnh hinhAnh)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];
            ContentRepository service = new ContentRepository();
            int i = service.UploadImageInDatabase(file, hinhAnh);
            if (i == 1)
            {
                return RedirectToAction("Index");
            }

            ViewBag.IDTour = new SelectList(db.Tours, "IDTour", "tourName", hinhAnh.IDTour);
            ViewBag.IDLoaiHinhAnh = new SelectList(db.LoaiHinhAnhs, "IDLoaiHinhAnh", "loaiHinhAnh1", hinhAnh.IDLoaiHinhAnh);
            return View(hinhAnh);
        }

        // GET: Admin/HinhAnh/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhAnh hinhAnh = db.HinhAnhs.Find(id);
            if (hinhAnh == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDTour = new SelectList(db.Tours, "IDTour", "tourName", hinhAnh.IDTour);
            ViewBag.IDLoaiHinhAnh = new SelectList(db.LoaiHinhAnhs, "IDLoaiHinhAnh", "loaiHinhAnh1", hinhAnh.IDLoaiHinhAnh);
            return View(hinhAnh);
        }

        // POST: Admin/HinhAnh/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDHinhAnh,imageUrl,IDTour,IDLoaiHinhAnh")] HinhAnh hinhAnh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hinhAnh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDTour = new SelectList(db.Tours, "IDTour", "tourName", hinhAnh.IDTour);
            ViewBag.IDLoaiHinhAnh = new SelectList(db.LoaiHinhAnhs, "IDLoaiHinhAnh", "loaiHinhAnh1", hinhAnh.IDLoaiHinhAnh);
            return View(hinhAnh);
        }

        // GET: Admin/HinhAnh/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HinhAnh hinhAnh = db.HinhAnhs.Find(id);
            if (hinhAnh == null)
            {
                return HttpNotFound();
            }
            return View(hinhAnh);
        }

        // POST: Admin/HinhAnh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HinhAnh hinhAnh = db.HinhAnhs.Find(id);
            db.HinhAnhs.Remove(hinhAnh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Display images
        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (id != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        public byte[] GetImageFromDataBase(int id)
        {
            var q = from temp in db.HinhAnhs where temp.IDHinhAnh == id select temp.imageUrl;
            byte[] cover = q.First();
            return cover;
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
