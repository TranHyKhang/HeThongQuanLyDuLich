using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HeThongQuanLyDuLich.Models;
namespace HeThongQuanLyDuLich.Areas.Client.Models
{
    public class ThongTinDatVe
    {
        [Required(ErrorMessage = "Vui lòng nhập họ tên !!!")]
        public string TenKH { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại !!!")]
        public string SoDT { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ !!!")]
        public string DiaChi { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập chứng minh nhân dân !!!")]
        public string Cmnd { get; set; }
        public int SoLuongKhach { get; set; }
        public string HinhThucThanhToan { get; set; }
        public Tour Tour { get; set; }
    }
}