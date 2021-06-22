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
    public class HanhTrinhController : Controller
    {
        private HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();
        Random rnd = new Random();
        static int oldNum = 0;

        // GET: Admin/HanhTrinh
        [Authorize]
        public ActionResult Index(int? page, string searchValue)
        {
            List<HanhTrinh> dsHanhTrinh = new List<HanhTrinh>();

            if (searchValue == null)
            {
                dsHanhTrinh = db.HanhTrinhs.ToList();
            } else
            {
                string[] temp = searchValue.Split('+');
                string a = string.Join(" ", temp).ToLower();
                dsHanhTrinh = db.HanhTrinhs.ToList().Where(s => s.tenHanhTrinh.ToLower().Contains(a)).ToList();
            }
            foreach(var x in dsHanhTrinh)
            {
                x.moTaHanhTrinh = x.moTaHanhTrinh.Substring(0, 50) + "...";
            }

            int pageSize = 8;
            int pageNum = (page ?? 1);

            return View(dsHanhTrinh.ToPagedList(pageNum, pageSize));
        }

        // GET: Admin/HanhTrinh/Details/5
        [Authorize]
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
        [Authorize]
        public ActionResult Create(int? id)
        {
            string tourName = db.Tours.Find(id).tourName.ToString();
            HanhTrinh model = new HanhTrinh()
            {
                tenHanhTrinh = tourName
            };
            if(id != null)
            {
                return View(model);
            }
            return View();
        }

        // POST: Admin/HanhTrinh/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tenHanhTrinh,diemKhoiHanh,diemKetThuc,thoiGianKhoiHanh,thoiGianKetThuc,moTaHanhTrinh")] HanhTrinh hanhTrinh)
        {
            hanhTrinh.IDHanhTrinh = RandomHanhTrinhID();

            if (ModelState.IsValid)
            {
                db.HanhTrinhs.Add(hanhTrinh);
                foreach(var x in  db.Tours)
                {
                    if(x.tourName == hanhTrinh.tenHanhTrinh)
                    {
                        x.HanhTrinhs.Add(hanhTrinh);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hanhTrinh);
        }

        // GET: Admin/HanhTrinh/Edit/5
        [Authorize]
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
        public ActionResult Edit([Bind(Include = "IDHanhTrinh,tenHanhTrinh,diemKhoiHanh,diemKetThuc,thoiGianKhoiHanh,thoiGianKetThuc,moTaHanhTrinh")] HanhTrinh hanhTrinh)
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
        [Authorize]
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
            HanhTrinh hanhTrinh = db.HanhTrinhs.Include(a=> a.Tours).FirstOrDefault(s => s.IDHanhTrinh == id);
            db.HanhTrinhs.Remove(hanhTrinh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public int RandomHanhTrinhID()
        {
            int num = rnd.Next(1, 10000);
            foreach (var x in db.HanhTrinhs)
            {
                if (x.IDHanhTrinh == num)
                {
                    num = oldNum + 1;
                }
                if (num < oldNum)
                {
                    num = oldNum + 1;
                }
            }
            oldNum = num;
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
