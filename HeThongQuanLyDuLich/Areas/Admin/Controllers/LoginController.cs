using HeThongQuanLyDuLich.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongQuanLyDuLich.Areas.Admin.Code;
using System.Web.Security;

namespace HeThongQuanLyDuLich.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Index(LoginModel model)
        {
            if(model.UserName != null && model.Password != null)
            {
                var result = new AccountModel().Login(model.UserName, model.Password);
                if (result && ModelState.IsValid)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return RedirectToAction("Index", "Tour");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                }
            }
            return View(model);
        }

        public ActionResult SignOut()
        {
            //SessionHelper.ClearSession();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}