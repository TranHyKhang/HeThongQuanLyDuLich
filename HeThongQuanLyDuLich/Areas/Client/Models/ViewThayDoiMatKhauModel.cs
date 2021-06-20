using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HeThongQuanLyDuLich.Models;

namespace HeThongQuanLyDuLich.Areas.Client.Models
{
    public class ViewThayDoiMatKhauModel
    {   
        [Required(ErrorMessage ="Mật khẩu cũ không được để trống")]
        public string MatKhauCu { get; set; }
        [Required(ErrorMessage = "Mật khẩu mới không được để trống")]
        public string MatKhauMoi { get; set; }
        [Required(ErrorMessage = "Mật khẩu xác nhận không được để trống")]
        public string XacNhanMatKhauMoi { get; set; }
    }
}