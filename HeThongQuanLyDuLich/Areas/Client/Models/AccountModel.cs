using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HeThongQuanLyDuLich.Models;

namespace HeThongQuanLyDuLich.Areas.Client.Models
{
    public class AccountModel
    {
        HeThongQuanLyDuLichEntities databasse = null;
        public AccountModel()
        {
            databasse = new HeThongQuanLyDuLichEntities();
        }


    }
}