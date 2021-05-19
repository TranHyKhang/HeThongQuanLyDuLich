using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeThongQuanLyDuLich.Models;
namespace HeThongQuanLyDuLich.Areas.Client.Models
{
    public class ThongTinDatVe
    {
        public string TenKH { get; set; }
        public string SoDT { get; set; }
        public string DiaChi { get; set; }
        public string Cmnd { get; set; }
        public int SoLuongKhach { get; set; }
        public  Tour Tour { get; set; }     

        

    }
}