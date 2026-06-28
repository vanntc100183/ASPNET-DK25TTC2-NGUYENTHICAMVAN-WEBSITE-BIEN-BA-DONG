using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTravelMVC.Models;

namespace WebTravelMVC.Areas.Common.Controllers
{
    public class HomeController : Controller
    {
        EFTravel db = new EFTravel();
        public ActionResult Index()
        {
            ViewBag.NoiBat = db.BaiViets.OrderByDescending(n => n.LuotThich).ToList(); // bài viết nổi bật
            ViewBag.DoanhNghiepvaLuHanh = db.BaiViets.Where(n => n.MaTheLoai==2).ToList(); // doanh nghiệp và lữ hành
            return View();
        }
    }
}