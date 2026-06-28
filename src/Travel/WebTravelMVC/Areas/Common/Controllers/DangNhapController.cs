using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTravelMVC.Models;

namespace WebTravelMVC.Areas.Common.Controllers
{
    public class DangNhapController : Controller
    {
        EFTravel db = new EFTravel();
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(FormCollection f)
        {

            // Kiểm tra tên đăng nhập và mật khẩu
            string ssTaiKhoan = f["txtTaiKhoan"].ToString();
            string ssMatKhau = f["txtMatKhau"].ToString();
            if (ssTaiKhoan == "" & ssMatKhau == "")
            {
                ModelState.AddModelError("", "Vui loàng nhập tên đăng nhập và mật khẩu của bạn !");
            }
            else if (ssTaiKhoan == "")
            {
                ModelState.AddModelError("", "Bạn không được bỏ trống tên đăng nhập !");
            }
            else if (ssMatKhau == "")
            {
                ModelState.AddModelError("", "Bạn không được bỏ trống mật khẩu !");
            }
            else
            {
                var tk = db.TaiKhoans.SingleOrDefault(n => n.MaTaiKhoan == ssTaiKhoan & n.MatKhau == ssMatKhau);

                if (tk == null)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại trong hệ thống !");
                    return View();
                }
                else
                {
                    // Đăng nhập với quyền là ban quản trị
                    if(tk.MaQuyen == 1)
                    {
                        Session["TaiKhoanNV"] = tk;
                        Session["TaiKhoanKH"] = null;
                        return Redirect("/Common/Home/Index");
                    }
                    // Đăng nhập với quyền là khách hàng
                    if (tk.MaQuyen == 2)
                    {
                        Session["TaiKhoanNV"] = null;
                        ModelState.AddModelError("", "Chưa phát triển cho khách hàng!");
                        return View();
                        //Session["TaiKhoanKH"] = tk;
                        //return Redirect("/Common/Home/Index");
                    }
                }
            }
            return View();
        }
        public ActionResult DangXuatAdmin()
        {
            Session["TaiKhoanNV"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult DangXuatKH()
        {
            Session["TaiKhoanKH"] = null;
            return RedirectToAction("DangNhap", "DangNhap");
        }
    }
}