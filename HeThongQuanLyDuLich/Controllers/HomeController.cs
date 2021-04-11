using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyDuLich.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Haha()
        {
            return View();
        }

        public ActionResult hii()
        {
            return View();
        }

        public ActionResult hykhang()
        {
            return View();
        }

        public ActionResult HyKhangDepTrai()
        {
            return View();
        }
    }
}