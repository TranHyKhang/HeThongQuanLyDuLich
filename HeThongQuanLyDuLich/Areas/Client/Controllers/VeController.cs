using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HeThongQuanLyDuLich.Areas.Client.Controllers
{
    public class VeController : Controller
    {
        // GET: Client/Ve
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DatVe()
        {
            return View();
        }
    }
}