//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HeThongQuanLyDuLich.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class VeDatTour
    {
        public int IDVeDatTour { get; set; }
        public string hinhThucThanhToan { get; set; }
        public Nullable<bool> trangThaiVeDatTour { get; set; }
        public Nullable<int> IDTour { get; set; }
        public Nullable<int> IDKhachHang { get; set; }
        public Nullable<int> soLuongVeDatTour { get; set; }
    
        public virtual KhachHang KhachHang { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
