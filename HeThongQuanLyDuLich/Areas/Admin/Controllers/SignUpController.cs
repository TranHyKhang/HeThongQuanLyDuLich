using HeThongQuanLyDuLich.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongQuanLyDuLich.Models;

namespace HeThongQuanLyDuLich.Areas.Admin.Controllers
{
    public class SignUpController : Controller
    {
        // GET: Admin/SignUp
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public int RandomID()
        {
            Random rnd = new Random();
            int num = rnd.Next(1, 1000000);
            return num;
        }

        [HttpPost] 
        public ActionResult Index([Bind(Include ="IDTaiKhoan, UserName, Password, ConfirmPassword, IDLoaiTaiKhoan")]SignUpModel model)
        {
            TaiKhoan tk = new TaiKhoan()
            {
                IDTaiKhoan = RandomID(),
                email = model.UserName,
                matKhau = model.Password,
                IDLoaiTaiKhoan = 1
            };
            new AccountModel().SignUp(tk);
            return RedirectToAction("Index", "Login");
        }
    }
}