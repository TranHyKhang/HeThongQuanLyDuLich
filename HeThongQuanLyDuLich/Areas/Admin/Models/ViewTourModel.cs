using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeThongQuanLyDuLich.Models;

namespace HeThongQuanLyDuLich.Areas.Admin.Models
{
    public partial class ViewTourModel
    {
        HeThongQuanLyDuLichEntities db = new HeThongQuanLyDuLichEntities();

        public Tour Tour { get; set; }
        
        public List<DichVu> listDichVu { get; set; }

        public bool[] dsDichVuDuoChon { get; set; }
    }
}