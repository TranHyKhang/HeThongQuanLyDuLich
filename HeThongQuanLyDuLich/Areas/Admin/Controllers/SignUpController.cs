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
        HeThongQuanLyDuLichEntities database = new HeThongQuanLyDuLichEntities();
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
            foreach(var acc in database.TaiKhoans)
            {
                if (acc.IDTaiKhoan == num)
                {
                    RandomID();
                }
            }
            return num;
        }

        public bool CheckUserName(TaiKhoan tk)
        {
            bool rs = true;
            foreach(var acc in database.TaiKhoans)
            {
                if(acc.email == tk.email)
                {
                    rs = false;
                    break;
                }
            }
            return rs;
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
            if (CheckUserName(tk) == true)
            {
                if(model.ConfirmPassword == tk.matKhau)
                {
                    new AccountModel().SignUp(tk);
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ModelState.AddModelError("","Mật khẩu không khớp");
                }
            }
            else
            {
                ModelState.AddModelError("", "Tên tài khoản đã tồn tại");
            }
            return View(model);

        }
    }
}