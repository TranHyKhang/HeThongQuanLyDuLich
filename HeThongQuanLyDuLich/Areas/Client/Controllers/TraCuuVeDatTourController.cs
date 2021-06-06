using HeThongQuanLyDuLich.Areas.Client.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongQuanLyDuLich.Models;
using PagedList;
using PagedList.Mvc;

namespace HeThongQuanLyDuLich.Areas.Client.Controllers
{
    public class TraCuuVeDatTourController : Controller
    {
        HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();
        // GET: Client/TraCuuVeDatTour
        public ActionResult Index(int? page)
        {
            var userName = SessionHelper.GetSession().UserName;
            int idTaiKhoan = 0;
            foreach(var x in db.TaiKhoans)
            {
                if(x.email == userName)
                {
                    idTaiKhoan = x.IDTaiKhoan;
                }
            }
            int idKhachHang = 0;
            foreach(var x in db.KhachHangs)
            {
                if(x.IDTaiKhoan == idTaiKhoan)
                {
                    idKhachHang = x.IDKhachHang;
                }
            }

            List<VeDatTour> listVeDatTour = new List<VeDatTour>();
            foreach(var ve in db.VeDatTours)
            {
                if(ve.soLuongVeDatTour == idKhachHang)
                {
                    listVeDatTour.Add(ve);
                }
            }

            int pageSize = 4;
            int pageNum = (page ?? 1);

            return View(listVeDatTour.ToPagedList(pageNum, pageSize));
        }

        public ActionResult HuyVeDatTour(int id)
        {
            VeDatTour veDatTour = db.VeDatTours.Find(id);
            db.VeDatTours.Remove(veDatTour);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}