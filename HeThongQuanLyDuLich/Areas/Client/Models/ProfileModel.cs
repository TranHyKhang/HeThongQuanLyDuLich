using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeThongQuanLyDuLich.Models;

namespace HeThongQuanLyDuLich.Areas.Client.Models
{
    public class ProfileModel
    {
        public KhachHang KhachHang { get; set; }
        public TaiKhoan TaiKhoan { get; set; }
    }
}