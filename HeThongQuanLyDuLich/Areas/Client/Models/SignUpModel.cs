using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeThongQuanLyDuLich.Areas.Client.Models
{
    public class SignUpModel
    {
        public int IDTaiKhoan { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int IDLoaiTaiKhoan { get; set; }
    }
}