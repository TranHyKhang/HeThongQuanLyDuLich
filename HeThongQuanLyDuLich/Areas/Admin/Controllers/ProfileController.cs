using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HeThongQuanLyDuLich.Models;
using HeThongQuanLyDuLich.Areas.Admin.Models;
using HeThongQuanLyDuLich.Areas.Admin.Code;

namespace HeThongQuanLyDuLich.Areas.Admin.Controllers
{
    public class ProfileController : Controller
    {
        HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();
        // GET: Admin/Profile
        [Authorize]
        public ActionResult Index()
        {
            string userEmail = HttpContext.User.Identity.Name;
            TaiKhoan tk = null;
            foreach(var x in db.TaiKhoans)
            {
                if(x.email == userEmail)
                {
                    tk = x;
                }
            }
            NhanVien nv = null;
            foreach(var x in db.NhanViens)
            {
                if(x.IDTaiKhoan == tk.IDTaiKhoan)
                {
                    nv = x;
                }
            }

            ViewProfileModel model = new ViewProfileModel()
            {
                NhanVien = nv,
                TaiKhoan = tk
            };
            Console.WriteLine(model);
            return View(model);
        }

        [Authorize]
        public ActionResult ThayDoiThongTin()
        {
            string userEmail = HttpContext.User.Identity.Name;
            TaiKhoan tk = null;
            foreach (var x in db.TaiKhoans)
            {
                if (x.email == userEmail)
                {
                    tk = x;
                }
            }
            NhanVien nv = null;
            foreach (var x in db.NhanViens)
            {
                if (x.IDTaiKhoan == tk.IDTaiKhoan)
                {
                    nv = x;
                }
            }

            ViewProfileModel model = new ViewProfileModel()
            {
                NhanVien = nv,
                TaiKhoan = tk
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult PostThayDoiThongTin(ViewProfileModel model)
        {
            string userEmail = HttpContext.User.Identity.Name;
            TaiKhoan tk = null;
            foreach (var x in db.TaiKhoans)
            {
                if (x.email == userEmail)
                {
                    tk = x;
                }
            }
            NhanVien nv = null;
            foreach (var x in db.NhanViens)
            {
                if (x.IDTaiKhoan == tk.IDTaiKhoan)
                {
                    nv = x;
                }
            }
            

            nv.hoTenNhanVien = model.NhanVien.hoTenNhanVien;
            nv.sdtNhanVien = model.NhanVien.sdtNhanVien;
            nv.diaChi = model.NhanVien.diaChi;
            nv.CMND = model.NhanVien.CMND;

            tk.email = model.TaiKhoan.email;


            if (ModelState.IsValid)
            {
                db.Entry(nv).State = System.Data.Entity.EntityState.Modified;
                db.Entry(tk).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Success");
            }
            return View("ThayDoiThongTin", model);

        }

        [HttpPost]
        public ActionResult PostThayDoiHinhAnh(ViewProfileModel model)
        {
            string userEmail = HttpContext.User.Identity.Name;
            TaiKhoan tk = null;
            foreach (var x in db.TaiKhoans)
            {
                if (x.email == userEmail)
                {
                    tk = x;
                }
            }
            NhanVien nv = null;
            foreach (var x in db.NhanViens)
            {
                if (x.IDTaiKhoan == tk.IDTaiKhoan)
                {
                    nv = x;
                }
            }
            

            HttpPostedFileBase file = Request.Files["uploadPhoto"];

            ContentRepository service = new ContentRepository();

            if (file.FileName != "")
            {
                tk.avatar = service.ConvertToBytes(file);

            }

            db.Entry(tk).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            model.NhanVien = nv;
            model.TaiKhoan = tk;

            return View("ThayDoiThongTin", model);
        }

        [Authorize]
        public ActionResult Success()
        {
            return View();
        }
    }
}