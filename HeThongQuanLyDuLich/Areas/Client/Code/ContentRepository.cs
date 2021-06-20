using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using HeThongQuanLyDuLich.Models;

namespace HeThongQuanLyDuLich.Areas.Client.Code
{
    public class ContentRepository
    {
        HeThongQuanLyDuLichEntities database = new HeThongQuanLyDuLichEntities();

        public int UploadImageInDatabase(HttpPostedFileBase file, HinhAnh image)
        {
            image.imageUrl = ConvertToBytes(file);
            var HinhAnh = new HinhAnh
            {
                IDHinhAnh = image.IDHinhAnh,
                imageUrl = image.imageUrl,
                IDTour = image.IDTour,
                IDLoaiHinhAnh = image.IDLoaiHinhAnh
            };
            database.HinhAnhs.Add(HinhAnh);
            int i = database.SaveChanges();
            if (i == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
    }
}