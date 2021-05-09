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
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost] 
        public ActionResult Index([Bind(Include ="IDTaiKhoan, UserName, Password, IDLoaiTaiKhoan")]SignUpModel model)
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