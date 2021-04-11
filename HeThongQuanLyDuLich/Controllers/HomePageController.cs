using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyDuLich.Controllers
{
    public class HomePageController : Controller
    {
        // GET: HomePage
        public ActionResult TrangChu()
        {
            return View();
        }
    }
}