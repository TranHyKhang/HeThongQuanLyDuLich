using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongQuanLyDuLich.Models;
using HeThongQuanLyDuLich.Areas.Client.Models;
using HeThongQuanLyDuLich.Areas.Client.Code;

namespace HeThongQuanLyDuLich.Areas.Client.Controllers
{
    public class HomeController : Controller
    {
        HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();
        // GET: Client/Home
        public ActionResult Index()
        {
            ViewHomeModel homeModel = new ViewHomeModel()
            {
                Tours = db.Tours.ToList(),
                LoaiTours = db.LoaiTours.ToList()
            };
            return View(homeModel);
        }

        public ActionResult DetailTour(int id)
        {
            var tour = db.Tours.Where(s => s.IDTour == id).FirstOrDefault();
            foreach (var hanhTrinh in db.HanhTrinhs)
            {
                if (hanhTrinh.tenHanhTrinh == tour.tourName)
                {
                    tour.HanhTrinhs.Add(hanhTrinh);
                }
            }

            ViewDetailModel detailModel = new ViewDetailModel()
            {
                Tour = tour,
                ChiTietHanhTrinh = new List<List<string>>()
            };

            foreach (var hanhTrinh in tour.HanhTrinhs)
            {
                string[] arr = hanhTrinh.moTaHanhTrinh.Split(' ');

                List<string> listString = new List<string>();
                for(int i = 0; i < 1; i++)
                {
                    int start = 0;
                    int end = 0;
                    string temp = "";
                    for(int j = 0; j < arr.Length; j++)
                    {
                        if(arr[j] == "Trưa:" || arr[j] == "Chiều:")
                        {
                            end = (j - start);
                            temp = String.Join(" ", arr.SubArray(start, end));
                            start = j;
                            listString.Add(temp);
                        } 
                        if(j + 1 == arr.Length)
                        {
                            end = (j - start) + 1;
                            temp = String.Join(" ", arr.SubArray(start, end));
                            start = j;
                            listString.Add(temp);
                        }
                        
                    }
                }
                detailModel.ChiTietHanhTrinh.Add(listString);
            }

           
            
            return View(detailModel);
        }

    }
}