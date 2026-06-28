using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTravelMVC.Models;

namespace WebTravelMVC.Areas.Common.Controllers
{
    public class BaiVietController : Controller
    {
        EFTravel db = new EFTravel();

        [HttpGet]
        public ActionResult XemChiTiet(int iMaBaiViet)
        {
            var baiViet = db.BaiViets.SingleOrDefault(n => n.MaBaiViet == iMaBaiViet);
            return View(baiViet);
        }

        // bài viết theo thể loại
        [HttpGet]
        public ActionResult DanhSachBaiViet(int iMaTheLoai)
        {
            var theLoai = db.TheLoais.SingleOrDefault(n => n.MaTheLoai == iMaTheLoai);
            ViewBag.BaiViet = db.BaiViets.Where(n => n.MaTheLoai == iMaTheLoai).ToList();
            return View(theLoai);
        }
        // bài viết theo thể loại
        [HttpGet]
        public ActionResult BaiVietAll()
        {
            var BaiViet = db.BaiViets.ToList();
            return View(BaiViet);
        }

        // Thêm mới
        [HttpGet]
        public ActionResult ThemMoiBaiViet()
        {
            ViewBag.ListTheLoai = db.TheLoais.ToList();
            return View();
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken()]
        public ActionResult ThemMoiBaiViet(BaiViet model, HttpPostedFileBase AnhBia)
        {
            try
            {
                if (Session["TaiKhoanNV"] == null)
                {
                    return RedirectToAction("DangNhap", "DangNhap");
                }
                TaiKhoan tk = (TaiKhoan)Session["TaiKhoanNV"];

                BaiViet bv = new BaiViet();
                string tenAnh = "AnhMacDinh.jpg";
                bv.TenBaiViet = model.TenBaiViet;
                bv.NgayDang = DateTime.Now;
                #region Lưu hình ảnh vào thư mục
                if (AnhBia != null)
                {
                    tenAnh = Path.GetFileName(AnhBia.FileName);
                    var duongDan = Path.Combine(Server.MapPath("~/Assets/images"), tenAnh);
                    if (System.IO.File.Exists(duongDan))
                    {

                    }
                    else
                    {
                        AnhBia.SaveAs(duongDan);
                    }
                }
                #endregion
                bv.AnhBia = tenAnh;
                bv.NgayCapNhat = null;
                bv.NoiDung = model.NoiDung;
                bv.LuotThich = 0;
                bv.An = 0;
                bv.MaTheLoai = model.MaTheLoai;
                bv.MaTaiKhoan = tk.MaTaiKhoan;
                // cộng số lượng bài viết thể loại
                var SLBV = db.TheLoais.SingleOrDefault(n => n.MaTheLoai == model.MaTheLoai);
                SLBV.SoLuongBaiViet++;
                db.BaiViets.Add(bv);
                db.SaveChanges();

                return RedirectToAction("ThanhCong", "ThongBao");
            }
            catch
            {
                return RedirectToAction("ThatBai", "ThongBao");
            }
        }

        // Cập nhật bài viết
        [HttpGet]
        public ActionResult CapNhatBaiViet(int iMaBaiViet)
        {
            ViewBag.ListTheLoai = db.TheLoais.ToList();
            var baiViet = db.BaiViets.SingleOrDefault(n => n.MaBaiViet == iMaBaiViet);
            return View(baiViet);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken()]
        public ActionResult CapNhatBaiViet(BaiViet model, HttpPostedFileBase AnhBia)
        {
            try
            {
                if (Session["TaiKhoanNV"] == null)
                {
                    return RedirectToAction("DangNhap", "DangNhap");
                }
                TaiKhoan tk = (TaiKhoan)Session["TaiKhoanNV"];
                var bv = db.BaiViets.SingleOrDefault(n => n.MaBaiViet == model.MaBaiViet);
                bv.TenBaiViet = model.TenBaiViet;
                #region Lưu hình ảnh vào thư mục
                if (AnhBia != null)
                {
                    string tenAnh = Path.GetFileName(AnhBia.FileName);
                    var duongDan = Path.Combine(Server.MapPath("~/Assets/images"), tenAnh);
                    if (System.IO.File.Exists(duongDan))
                    {
                    }
                    else
                    {
                        AnhBia.SaveAs(duongDan);
                    }
                    bv.AnhBia = tenAnh;
                }
                #endregion
                bv.NgayCapNhat = DateTime.Now;
                bv.NoiDung = model.NoiDung;
                bv.LuotThich = 0;
                bv.An = model.An;
                bv.MaTheLoai = model.MaTheLoai;
                bv.MaTaiKhoan = tk.MaTaiKhoan;
                db.SaveChanges();

                return RedirectToAction("ThanhCong", "ThongBao");
            }
            catch
            {
                return RedirectToAction("ThatBai", "ThongBao");
            }
        }

        // Xóa bài viết
        [HttpGet]
        public ActionResult XoaBaiViet(int iMaBaiViet)
        {
            try
            {
                var baiViet = db.BaiViets.SingleOrDefault(n => n.MaBaiViet == iMaBaiViet);
                // trừ số lượng bài viết thể loại
                var SLBV = db.TheLoais.SingleOrDefault(n => n.MaTheLoai == baiViet.MaTheLoai);
                SLBV.SoLuongBaiViet--;
                db.BaiViets.Remove(baiViet);
                db.SaveChanges();
                return RedirectToAction("ThanhCong", "ThongBao");
            }
            catch
            {
                return RedirectToAction("ThatBai", "ThongBao");
            }
        }
        // Ẩn bài viết
        [HttpGet]
        public ActionResult AnBaiViet(int iMaBaiViet)
        {
            try
            {
                var baiViet = db.BaiViets.SingleOrDefault(n => n.MaBaiViet == iMaBaiViet);
                // bài viết đang ẩn => admin muốn hiển thị lại
                if (baiViet.An == 1)
                {
                    baiViet.An = 0;
                }
                // bài viết đang hiển thị => admin muốn ẩn
                else
                {
                    baiViet.An = 1;
                }
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