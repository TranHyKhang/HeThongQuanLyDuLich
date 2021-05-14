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

    public partial class Tour
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tour()
        {
            this.Feedbacks = new HashSet<Feedback>();
            this.HinhAnhs = new HashSet<HinhAnh>();
            this.VeDatTours = new HashSet<VeDatTour>();
        }
    
        public int IDTour { get; set; }
        [Display(Name = "Tour")]
        public string tourName { get; set; }
        [Display(Name = "Mô tả")]
        public string tourDescription { get; set; }
        [Display(Name = "Số lượng khách tối đa")]
        public Nullable<int> soLuongKhachToiDa { get; set; }
        [Display(Name = "Số lượng khách hiện tại")]
        public Nullable<int> soLuongKhachHienTai { get; set; }
        [Display(Name = "Tình trạng tour")]
        public Nullable<bool> tinhTrangTour { get; set; }
        [Display(Name = "Hành trình")]
        public Nullable<int> IDHanhTrinh { get; set; }
        [Display(Name = "Dịch vụ")]
        public Nullable<int> IDDichVu { get; set; }
        [Display(Name = "Khách sạn")]
        public Nullable<int> IDKhachSan { get; set; }
        [Display(Name = "Khuyến mãi")]
        public Nullable<int> IDKhuyenMai { get; set; }
        [Display(Name = "Giá")]
        public Nullable<int> gia { get; set; }

        public virtual DichVu DichVu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual HanhTrinh HanhTrinh { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HinhAnh> HinhAnhs { get; set; }
        public virtual KhachSan KhachSan { get; set; }
        public virtual KhuyenMai KhuyenMai { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VeDatTour> VeDatTours { get; set; }
    }
}
