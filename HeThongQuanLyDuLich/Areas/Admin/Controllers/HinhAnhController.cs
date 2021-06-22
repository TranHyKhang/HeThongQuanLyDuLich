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
using PagedList;
using PagedList.Mvc;

namespace HeThongQuanLyDuLich.Areas.Admin.Controllers
{
    public class HinhAnhController : Controller
    {
        private HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();
        Random rnd = new Random();

        // GET: Admin/HinhAnh
        [Authorize]
        public ActionResult Index(int? page, string searchValue)
        {
            List<HinhAnh> dsHinhAnh = new List<HinhAnh>();

            if (searchValue == null)
            {
                dsHinhAnh = db.HinhAnhs.Include(h => h.Tour).Include(h => h.LoaiHinhAnh).ToList();
            } else
            {
                string[] temp = searchValue.Split('+');
                string a = string.Join(" ", temp).ToLower();
                dsHinhAnh = db.HinhAnhs.Include(h => h.Tour).Include(h => h.LoaiHinhAnh).Where(s => s.Tour.tourName.ToLower().Contains(a)).ToList();
            }
            int pageSize = 8;
            int pageNum = (page ?? 1);
            return View(dsHinhAnh.ToPagedList(pageNum, pageSize));
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
            Console.WriteLine(hinhAnh);
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
            ViewBag.IDTour = db.Tours.ToList();
            ViewBag.IDLoaiHinhAnh = new SelectList(db.LoaiHinhAnhs, "IDLoaiHinhAnh", "loaiHinhAnh1");
            return View();
        }

        // POST: Admin/HinhAnh/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "imageUrl,IDTour,IDLoaiHinhAnh")] HinhAnh hinhAnh)
        {
            hinhAnh.IDHinhAnh = RandomHinhAnhID();
            HttpPostedFileBase file = Request.Files["ImageData"];
            ContentRepository service = new ContentRepository();
            int i = service.UploadImageInDatabase(file, hinhAnh);
            if (i == 1)
            {
                return RedirectToAction("Details", "Tour", new { id = hinhAnh.IDTour});
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

            HttpPostedFileBase file = Request.Files["ImageData"];
            ContentRepository service = new ContentRepository();
            byte[] img = service.ConvertToBytes(file);
            hinhAnh.imageUrl = img;
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

        public int RandomHinhAnhID()
        {
            int num = rnd.Next(1, 10000);
            foreach (var x in db.HinhAnhs)
            {
                if (x.IDHinhAnh == num)
                {
                    RandomHinhAnhID();
                }
            }
            return num;
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
