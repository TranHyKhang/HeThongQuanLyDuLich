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
    
    public partial class HinhAnh
    {
        public int IDHinhAnh { get; set; }
        public byte[] imageUrl { get; set; }
        public Nullable<int> IDTour { get; set; }
        public Nullable<int> IDLoaiHinhAnh { get; set; }
    
        public virtual LoaiHinhAnh LoaiHinhAnh { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
