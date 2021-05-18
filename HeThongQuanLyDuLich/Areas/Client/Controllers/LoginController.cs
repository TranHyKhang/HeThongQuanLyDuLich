using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongQuanLyDuLich.Areas.Client.Models;

namespace HeThongQuanLyDuLich.Areas.Client.Controllers
{
    public class LoginController : Controller
    {
        // GET: Client/Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel model, )
        {
            return RedirectToAction("Index", "Home");
        }
    }
}