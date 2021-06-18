using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongQuanLyDuLich.Models;

namespace HeThongQuanLyDuLich.Areas.Client.Controllers
{
    public class DoSomeThingController : Controller
    {
        HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();
        // GET: Client/DoSomeThing
        public ActionResult Index()
        {
            return View();
        }

        //Display images
        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (id != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        public ActionResult RetrieveImageByUserName()
        {
            byte[] cover = GetImageByUserNameFromDatabase();
            if(cover != null)
            {
                return File(cover, "image/jpg");

            }
            return base.File("~/Areas/Client/Assets/img/defaultAvatar.jpg", "image/jpg");
        }

        public ActionResult RetrieveUserImage(int id)
        {
            byte[] cover = GetImageUserFromDatabase(id);
            if(id != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        public byte[] GetImageByUserNameFromDatabase()
        {
            TaiKhoan tk = null;
            string name = HttpContext.User.Identity.Name;
            foreach(var x in db.TaiKhoans)
            {
                if(x.email == name)
                {
                    tk = x;
                }
            }

            byte[] cover = tk.avatar;
            return cover;
        }

        public byte[] GetImageFromDataBase(int id)
        {
            var q = from temp in db.HinhAnhs where temp.IDHinhAnh == id select temp.imageUrl;
            byte[] cover = q.First();
            return cover;
        }

        public byte[] GetImageUserFromDatabase(int id)
        {
            TaiKhoan tk = null;
            foreach(var x in db.TaiKhoans)
            {
                if(x.IDTaiKhoan == id)
                {
                    tk = x;
                }
            }

            byte[] cover = tk.avatar;
            return cover;
        }
    }
}