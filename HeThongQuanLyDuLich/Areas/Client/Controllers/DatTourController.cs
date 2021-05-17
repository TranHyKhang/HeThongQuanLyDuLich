using HeThongQuanLyDuLich.Areas.Client.Models;
using HeThongQuanLyDuLich.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyDuLich.Areas.Client.Controllers
{
    public class DatTourController : Controller
    {
        HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();

        //GET: Client/DatTour
        public ActionResult Index(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tour tour = db.Tours.Where(s => s.IDTour == id).FirstOrDefault();
            if (tour == null)
            {
                return HttpNotFound();
            }
            ThongTinDatVe a = new ThongTinDatVe()
            {
                tour = tour
            };
                return View(a);
           

        }

    }
}