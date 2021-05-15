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
        public ActionResult Index()
        {
            return View();
        }
        // GET: Client/DatTour
        /* [Authorize]
         public ActionResult Index(int? id)
         {
             if(id==null)
             {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             Tour tour = db.Tours.Find(id);
             if(tour ==null )
             {
                 return HttpNotFound();

             }    
             return View(tour);
         }*/

    }
}