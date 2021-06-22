using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HeThongQuanLyDuLich.Areas.Admin.Models
{
    public class SignUpModel
    {

        public int IDTaiKhoan { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu !!!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập xác nhận mật khẩu !!!")]
        public string ConfirmPassword { get; set; }
        public int IDLoaiTaiKhoan { get; set; }

        //Info Customer
        public int IDNhanVien { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ tên !!!")]
        public string HoTenNhanVien { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại !!!")]
        public string SDT { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ !!!")]
        public string DiaChi { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập chứng minh nhân dân !!!")]
        public string CMND { get; set; }
    }
}