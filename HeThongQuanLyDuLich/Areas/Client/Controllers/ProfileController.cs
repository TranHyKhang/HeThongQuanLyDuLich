using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongQuanLyDuLich.Models;
using HeThongQuanLyDuLich.Areas.Client.Code;
using HeThongQuanLyDuLich.Areas.Client.Models;

namespace HeThongQuanLyDuLich.Areas.Client.Controllers
{
    public class ProfileController : Controller
    {
        HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();
        // GET: Client/Profile
        public ActionResult Index()
        {
            UserSession user = SessionHelper.GetSession();
            TaiKhoan tk = null;
            foreach(var x in db.TaiKhoans)
            {
                if(x.email == user.UserName)
                {
                    tk = x;
                }
            }
            KhachHang kh = null;
            foreach(var x in db.KhachHangs)
            {
                if(x.IDTaiKhoan == tk.IDTaiKhoan)
                {
                    kh = x;
                }
            }

            ProfileModel profile = new ProfileModel()
            {
                KhachHang = kh,
                TaiKhoan = tk
            };
            return View(profile);
        }

        public ActionResult ThayDoiThongTin()
        {
            UserSession user = SessionHelper.GetSession();
            TaiKhoan tk = null;
            foreach (var x in db.TaiKhoans)
            {
                if (x.email == user.UserName)
                {
                    tk = x;
                }
            }
            KhachHang kh = null;
            foreach (var x in db.KhachHangs)
            {
                if (x.IDTaiKhoan == tk.IDTaiKhoan)
                {
                    kh = x;
                }
            }

            ProfileModel profile = new ProfileModel()
            {
                KhachHang = kh,
                TaiKhoan = tk
            };
            return View(profile);
        }

        [HttpPost]
        public ActionResult PostThayDoiThongTin(ProfileModel model)
        {
            UserSession user = SessionHelper.GetSession();
            TaiKhoan tk = null;
            foreach (var x in db.TaiKhoans)
            {
                if (x.email == user.UserName)
                {
                    tk = x;
                }
            }

            KhachHang kh = null;
            foreach (var x in db.KhachHangs)
            {
                if (x.IDTaiKhoan == tk.IDTaiKhoan)
                {
                    kh = x;
                }
            }

            kh.hoTenKhachHang = model.KhachHang.hoTenKhachHang;
            kh.sdtKhachHang = model.KhachHang.sdtKhachHang;
            kh.diaChi = model.KhachHang.diaChi;
            kh.CMND = model.KhachHang.CMND;

            tk.email = model.TaiKhoan.email;
            

            if (ModelState.IsValid)
            {
                db.Entry(kh).State = System.Data.Entity.EntityState.Modified;
                db.Entry(tk).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Success");
            }
            return View("ThayDoiThongTin",model);

        }

        [HttpPost]
        public ActionResult PostThayDoiHinhAnh(ProfileModel model)
        {
            UserSession user = SessionHelper.GetSession();
            TaiKhoan tk = null;
            foreach (var x in db.TaiKhoans)
            {
                if (x.email == user.UserName)
                {
                    tk = x;
                }
            }

            KhachHang kh = null;
            foreach (var x in db.KhachHangs)
            {
                if (x.IDTaiKhoan == tk.IDTaiKhoan)
                {
                    kh = x;
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

            model.KhachHang = kh;
            model.TaiKhoan = tk;

            return View("ThayDoiThongTin", model);
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}