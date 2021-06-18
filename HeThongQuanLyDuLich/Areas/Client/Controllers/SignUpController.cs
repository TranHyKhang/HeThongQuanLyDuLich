using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongQuanLyDuLich.Models;
using HeThongQuanLyDuLich.Areas.Client.Models;

namespace HeThongQuanLyDuLich.Areas.Client.Controllers
{
    public class SignUpController : Controller
    {
        HeThongQuanLyDuLichEntities database = new HeThongQuanLyDuLichEntities();
        Random rnd = new Random();
        // GET: Admin/SignUp
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public int RandomID()
        {
            int num = rnd.Next(1, 1000000);
            foreach (var acc in database.TaiKhoans)
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
            if(tk.email == null)
            {
                return false;
            }
            foreach (var acc in database.TaiKhoans)
            {
                if (acc.email == tk.email)
                {
                    rs = false;
                    break;
                }
            }
            return rs;
        }

        [HttpPost]
        public ActionResult Index([Bind(Include = "IDTaiKhoan, UserName, Password, ConfirmPassword, IDLoaiTaiKhoan, IDKhachHang, HoTenKhachHang, SDT, DiaChi, CMND")]SignUpModel model)
        {

            TaiKhoan tk = new TaiKhoan()
            {
                IDTaiKhoan = RandomID(),
                email = model.UserName,
                matKhau = model.Password,
                IDLoaiTaiKhoan = 2
            };

            KhachHang kh = new KhachHang()
            {
                IDKhachHang = tk.IDTaiKhoan + 1,
                hoTenKhachHang = model.HoTenKhachHang,
                sdtKhachHang = model.SDT,
                diaChi = model.DiaChi,
                CMND = model.CMND,
                IDTaiKhoan = tk.IDTaiKhoan
            };

            if (CheckUserName(tk) == true)
            {
                if (model.ConfirmPassword == tk.matKhau)
                {
                    new AccountModel().SignUp(tk);
                    database.KhachHangs.Add(kh);
                    database.SaveChanges();
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu không khớp");
                }
            }
            else
            {
                ModelState.AddModelError("", "Tên tài khoản đã tồn tại hoặc không hợp lệ");
            }
            return View(model);

        }
    }
}