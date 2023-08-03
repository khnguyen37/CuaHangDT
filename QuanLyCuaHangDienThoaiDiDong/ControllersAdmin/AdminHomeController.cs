using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyCuaHangDienThoaiDiDong.ControllersAdmin
{
    public class AdminHomeController : Controller
    {
        //
        // GET: /AdminHome/

        public ActionResult Index()
        {
            return View();
        }
        //Đăng xuất
        public ActionResult DangXuat()
        {
            Session["NhanVien"] = null;
            return RedirectToAction("Index", "AdminLogin");
        }

    }
}
