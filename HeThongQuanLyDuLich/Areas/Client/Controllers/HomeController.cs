using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongQuanLyDuLich.Models;

namespace HeThongQuanLyDuLich.Areas.Client.Controllers
{
    public class HomeController : Controller
    {
        HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();
        // GET: Client/Home
        public ActionResult Index()
        {
            return View(db.Tours.ToList());
        }
        public ActionResult ListTour(Tour T)
        {
            return View(db.Tours.ToList());
        }
        public ActionResult DetailsTour(int id)
        {
            return View(db.Tours.Where(s => s.IDTour == id).FirstOrDefault());
        }
    }
}