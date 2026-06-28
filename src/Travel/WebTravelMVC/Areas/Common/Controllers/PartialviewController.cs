using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTravelMVC.Models;

namespace WebTravelMVC.Areas.Common.Controllers
{
    public class PartialviewController : Controller
    {
        EFTravel db = new EFTravel();
        public ActionResult TheLoai_Par(int iMaDanhMuc)
        {
            var theLoai = db.TheLoais.Where(n => n.MaDanhMuc == iMaDanhMuc).ToList();
            return PartialView(theLoai);
        }

        // Bài viết mới
        public ActionResult BaiVietMoi_Par()
        {
            var baiViet = db.BaiViets.Where(n=>n.An==0).OrderByDescending(n => n.MaBaiViet).ToList().Take(4);
            return PartialView(baiViet);
        }
        
        // Điểm đến hấp dẫn => like nhiều
        public ActionResult DiemDenHapDan_Par()
        {
            var baiViet = db.BaiViets.OrderByDescending(n => n.LuotThich).ToList();
            return PartialView(baiViet);
        }
    }
}