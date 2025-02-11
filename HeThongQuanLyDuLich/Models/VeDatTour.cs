﻿//------------------------------------------------------------------------------
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
    using System.ComponentModel.DataAnnotations;

    public partial class VeDatTour
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VeDatTour()
        {
            this.Feedbacks = new HashSet<Feedback>();
        }

        [Display(Name = "ID Vé đặt tour")]
        public int IDVeDatTour { get; set; }
        [Display(Name = "Hình thức thanh toán")]
        public string hinhThucThanhToan { get; set; }
        [Display(Name = "Trạng thái vé")]
        public Nullable<bool> trangThaiVeDatTour { get; set; }
        [Display(Name = "ID Tour")]
        public Nullable<int> IDTour { get; set; }
        [Display(Name = "ID Khách hàng")]
        public Nullable<int> IDKhachHang { get; set; }
        [Display(Name = "ID người đặt  vé")]
        public Nullable<int> soLuongVeDatTour { get; set; }
        [Display(Name = "Ngày đặt vé")]
        public Nullable<System.DateTime> ngayDatVe { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
