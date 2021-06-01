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
        public byte[] GetImageFromDataBase(int id)
        {
            var q = from temp in db.HinhAnhs where temp.IDHinhAnh == id select temp.imageUrl;
            byte[] cover = q.First();
            return cover;
        }
    }
}