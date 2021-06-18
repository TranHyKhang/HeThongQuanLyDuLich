using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeThongQuanLyDuLich.Models;

namespace HeThongQuanLyDuLich.Areas.Admin.Models
{
    public class ViewProfileModel
    {
        public NhanVien NhanVien { get; set; }
        public TaiKhoan TaiKhoan { get; set; }
    }
}