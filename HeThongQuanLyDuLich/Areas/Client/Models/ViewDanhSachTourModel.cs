using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeThongQuanLyDuLich.Models;

namespace HeThongQuanLyDuLich.Areas.Client.Models
{
    public class ViewDanhSachTourModel
    {
        public List<Tour> danhSachTour { get; set; }
        public LoaiTour loaiTour { get; set; }
        public String gioiThieuLoaiTour { get; set; }
    }
}