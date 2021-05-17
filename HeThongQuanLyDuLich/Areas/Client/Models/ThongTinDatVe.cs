using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeThongQuanLyDuLich.Models;
namespace HeThongQuanLyDuLich.Areas.Client.Models
{
    public class ThongTinDatVe
    {
        public string tenKH { get; set; }
        public string soDT { get; set; }
        public string diaChi { get; set; }
        public string cmnd { get; set; }
        public int soLuongKhach { get; set; }
        public  Tour tour { get; set; }     

        

    }
}