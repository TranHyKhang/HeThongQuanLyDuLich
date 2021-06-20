using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HeThongQuanLyDuLich.Areas.Client.Code;
using HeThongQuanLyDuLich.Areas.Client.Models;
using HeThongQuanLyDuLich.Models;

namespace HeThongQuanLyDuLich.Areas.Client.Controllers
{
    public class FeedbackController : Controller
    {
        private HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();
        Random rnd = new Random();

        public int RandomID()
        {
            int id = rnd.Next(0, 10000);
            foreach(var x in db.Feedbacks)
            {
                if(x.IDFeedback == id)
                {
                    RandomID();
                }
            }

            return id;
        }

        public ActionResult Feedback()
        {
            ViewFeedbackModel model = new ViewFeedbackModel()
            {
                DanhSachTour = db.Tours.ToList()
            };
            Console.WriteLine(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult PostFeedback([Bind(Include ="DanhSachTour, IDTour, NoiDungFeedBack")]ViewFeedbackModel model)
        {
            UserSession user = SessionHelper.GetSession();
            TaiKhoan tk = null;
            foreach (var x in db.TaiKhoans)
            {
                if (x.email == user.UserName)
                {
                    tk = x;
                }
            }
            KhachHang kh = null;
            foreach (var x in db.KhachHangs)
            {
                if (x.IDTaiKhoan == tk.IDTaiKhoan)
                {
                    kh = x;
                }
            }

            int? idVeDatTour = null;

            foreach(var x in kh.VeDatTours)
            {
                if(x.IDTour == model.IDTour)
                {
                    idVeDatTour = x.IDVeDatTour;
                }
            }


            Feedback feedback = new Feedback()
            {
                IDFeedback = RandomID(),
                noiDung = model.NoiDungFeedback,
                IDVeDatTour = idVeDatTour,
                ngayFeedback = DateTime.Now
            };

            if(ModelState.IsValid)
            {
                if (idVeDatTour != null)
                {
                    db.Feedbacks.Add(feedback);
                    db.SaveChanges();
                    Response.Write("<script>alert('Gửi nhận xét thành công!!!');</script>");
                    return RedirectToAction("Index", "Home");
                } else
                {
                    ModelState.AddModelError("LoiFeedback", "Bạn không thể feedback khi chưa đặt vé");
                }
            } else
            {
                ModelState.AddModelError("ThieuNoiDung", "Nội dung không được để trống");

            }
            ViewFeedbackModel newModel = new ViewFeedbackModel()
            {
                DanhSachTour = db.Tours.ToList(),
            };
            return View("Feedback", newModel);
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
