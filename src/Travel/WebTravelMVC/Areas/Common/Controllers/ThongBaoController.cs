using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTravelMVC.Models;

namespace WebTravelMVC.Areas.Common.Controllers
{
    public class ThongBaoController : Controller
    {
        //EFTravel db = new EFTravel();
        public ActionResult ThanhCong()
        {
            return View();
        }
        public ActionResult ThatBai()
        {
            return View();
        }
    }
}