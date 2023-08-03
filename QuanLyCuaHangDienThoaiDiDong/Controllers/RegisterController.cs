using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHangDienThoaiDiDong.Models;

namespace QuanLyCuaHangDienThoaiDiDong.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/

        QLDienThoaiDataContext data = new QLDienThoaiDataContext();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XuLyDangKy(KHACHHANG kh, FormCollection c)
        {
            string mk = c["PasW"];
            string mk2 = c["MATKHAUKH"];
            if (mk != mk2)
            {
                ViewBag.errorMassage = "Thông tin mật khẩu không trùng khớp!";
                return View("Index");
            }
            else
            {
                data.KHACHHANGs.InsertOnSubmit(kh);
                data.SubmitChanges();
                return RedirectToAction("Index", "Login");
            }
        }

    }
}
