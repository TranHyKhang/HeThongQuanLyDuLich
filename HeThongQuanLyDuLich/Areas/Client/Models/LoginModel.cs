using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HeThongQuanLyDuLich.Areas.Client.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Vui lòng nhập email !!!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu !!!")]
        public string UserPassword { get; set; }
    }
}