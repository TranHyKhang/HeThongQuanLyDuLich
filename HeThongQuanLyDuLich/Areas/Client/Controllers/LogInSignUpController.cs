using HeThongQuanLyDuLich.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyDuLich.Areas.Client.Controllers
{
    public class LogInSignUpController : Controller
    {
        [HttpGet]
        // GET: Client/LogInSignUp
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin model)
        {
            var result = new AccountModel().Login(model.UserName, model.Password);
            if (result && ModelState.IsValid)
            {
                SessionHelper.SetSession(new UserSession() { UserName = model.UserName });
                return RedirectToAction("Index", "Tour");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult SignUp([Bind(Include = "IDTaiKhoan, UserName, Password, IDLoaiTaiKhoan")] SignUpModel model)
        {
            TaiKhoan tk = new TaiKhoan()
            {
                IDTaiKhoan = model.IDTaiKhoan,
                email = model.UserName,
                matKhau = model.Password,
                IDLoaiTaiKhoan = 1
            };
            new AccountModel().SignUp(tk);
            return RedirectToAction("Index", "Login");
        }
    }
}