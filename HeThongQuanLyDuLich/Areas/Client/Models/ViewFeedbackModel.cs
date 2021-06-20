using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HeThongQuanLyDuLich.Models;

namespace HeThongQuanLyDuLich.Areas.Client.Models
{
    public class ViewFeedbackModel
    {
        public List<Tour> DanhSachTour { get; set; }

        public Nullable<int> IDTour { get; set; }

        [Required(ErrorMessage ="Hãy nhập nội dung")]
        public string NoiDungFeedback { get; set; }
    }
}