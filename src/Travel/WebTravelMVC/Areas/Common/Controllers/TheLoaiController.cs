using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTravelMVC.Models;

namespace WebTravelMVC.Areas.Common.Controllers
{
    public class TheLoaiController : Controller
    {
        EFTravel db = new EFTravel();

        [HttpGet]
        public ActionResult ListTheLoai()
        {
            var theLoai = db.TheLoais.ToList();
            return View(theLoai);
        }
        // Cập nhật
        [HttpGet]
        public ActionResult CapNhatTheLoai(int iMaTheLoai)
        {
            ViewBag.DanhMuc = db.DanhMucs.ToList();
            var theLoai = db.TheLoais.SingleOrDefault(n=>n.MaTheLoai == iMaTheLoai);
            return View(theLoai);
        }
        [HttpPost]
        public ActionResult CapNhatTheLoai(TheLoai Model)
        {
            var tl = db.TheLoais.SingleOrDefault(n => n.MaTheLoai == Model.MaTheLoai);
            tl.TenTheLoai = Model.TenTheLoai;
            tl.MaDanhMuc = Model.MaDanhMuc;
            db.SaveChanges();
            return RedirectToAction("ThanhCong", "ThongBao");
        }
        // Cập nhật
        [HttpGet]
        public ActionResult ThemMoiTheLoai()
        {
            ViewBag.DanhMuc = db.DanhMucs.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoiTheLoai(TheLoai Model)
        {
            TheLoai tl = new TheLoai();
            tl.TenTheLoai = Model.TenTheLoai;
            tl.SoLuongBaiViet = 0;
            tl.MaDanhMuc = Model.MaDanhMuc;
            // trừ số lượng danh mục thể loại
            var SLTL = db.DanhMucs.SingleOrDefault(n => n.MaDanhMuc == Model.MaDanhMuc);
            SLTL.SoLuongTheLoai++;
            db.TheLoais.Add(tl);
            db.SaveChanges();
            return RedirectToAction("ThanhCong", "ThongBao");
        }


        // Xóa thể loại
        [HttpGet]
        public ActionResult XoaTheLoai(int iMaTheLoai)
        {
            try
            {
                var theLoai = db.TheLoais.SingleOrDefault(n => n.MaTheLoai== iMaTheLoai);
                // trừ số lượng danh mục thể loại
                var SLTL = db.DanhMucs.SingleOrDefault(n => n.MaDanhMuc == theLoai.MaDanhMuc);
                SLTL.SoLuongTheLoai--;
                db.TheLoais.Remove(theLoai);
                db.SaveChanges();
                return RedirectToAction("ThanhCong", "ThongBao");
            }
            catch
            {
                return RedirectToAction("ThatBai", "ThongBao");
            }
        }
    }
}