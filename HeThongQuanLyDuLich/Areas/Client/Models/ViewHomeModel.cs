using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeThongQuanLyDuLich.Models;

namespace HeThongQuanLyDuLich.Areas.Client.Models
{
    public partial class ViewHomeModel
    {
        public List<Tour> Tours { get; set; }
        public List<LoaiTour> LoaiTours { get; set; }
    }
}