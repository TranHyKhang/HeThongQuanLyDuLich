using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Entity;
using System.Web;
using HeThongQuanLyDuLich.Models;


namespace HeThongQuanLyDuLich.Areas.Admin.Models
{
    public class AccountModel
    {
        private HeThongQuanLyDuLichEntities context = null;

        public AccountModel()
        {
            context = new HeThongQuanLyDuLichEntities();
        }


        public bool Login(string userName, string userPassword)
        {
            object[] sqlParams =
            {
                new SqlParameter("@UserName", userName),
                new SqlParameter("@UserPassword", userPassword)
            };
            var res = context.Database.SqlQuery<bool>("Sp_Account_Login_Admin @UserName, @UserPassword", sqlParams).SingleOrDefault();
            return res;
        }

        public void SignUp(TaiKhoan tk)
        {
            context.TaiKhoans.Add(tk);
            context.SaveChanges();
        }
    }
}