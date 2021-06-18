using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeThongQuanLyDuLich.Models;

namespace HeThongQuanLyDuLich.Areas.Admin.Models
{
    public class ViewDanhSachVeDatTourModel
    {
        public Tour Tour { get; set; }
        public List<VeDatTour> DanhSachVeDatTour { get; set; }
    }
}