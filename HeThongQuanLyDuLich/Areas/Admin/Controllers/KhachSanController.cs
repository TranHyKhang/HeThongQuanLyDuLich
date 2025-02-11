﻿using System;
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
    public class KhachSanController : Controller
    {
        private HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();
        Random rnd = new Random();

        // GET: Admin/KhachSan
        [Authorize]
        public ActionResult Index()
        {
            return View(db.KhachSans.ToList());
        }

        // GET: Admin/KhachSan/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachSan khachSan = db.KhachSans.Find(id);
            if (khachSan == null)
            {
                return HttpNotFound();
            }
            return View(khachSan);
        }

        // GET: Admin/KhachSan/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/KhachSan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tenKhachSan,moTaKhachSan,giaKhachSan")] KhachSan khachSan)
        {

            khachSan.IDKhachSan = RandomKhachSanID();

            if (ModelState.IsValid)
            {
                db.KhachSans.Add(khachSan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(khachSan);
        }

        // GET: Admin/KhachSan/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachSan khachSan = db.KhachSans.Find(id);
            if (khachSan == null)
            {
                return HttpNotFound();
            }
            return View(khachSan);
        }

        // POST: Admin/KhachSan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDKhachSan,tenKhachSan,moTaKhachSan,giaKhachSan")] KhachSan khachSan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachSan).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khachSan);
        }

        // GET: Admin/KhachSan/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachSan khachSan = db.KhachSans.Find(id);
            if (khachSan == null)
            {
                return HttpNotFound();
            }
            return View(khachSan);
        }

        // POST: Admin/KhachSan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhachSan khachSan = db.KhachSans.Find(id);
            db.KhachSans.Remove(khachSan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public int RandomKhachSanID()
        {
            int num = rnd.Next(1, 10000);
            foreach (var x in db.KhachSans)
            {
                if (x.IDKhachSan == num)
                {
                    RandomKhachSanID();
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
