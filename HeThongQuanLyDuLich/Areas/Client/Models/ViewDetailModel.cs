using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeThongQuanLyDuLich.Models;

namespace HeThongQuanLyDuLich.Areas.Client.Models
{
    public class ViewDetailModel
    {
        public Tour Tour { get; set; }
        public List<List<string>> ChiTietHanhTrinh { get; set; }
    }
}