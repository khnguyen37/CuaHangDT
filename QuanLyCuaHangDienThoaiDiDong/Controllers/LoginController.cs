using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHangDienThoaiDiDong.Models;
namespace QuanLyCuaHangDienThoaiDiDong.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        QLDienThoaiDataContext data = new QLDienThoaiDataContext();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XuLyDangNhap(FormCollection c)
        {
            string tk = c["txtUserName"];
            string mk = c["txtPassWord"];
            //vào csdl trong bảng Khách hàng tìm record có tên và mk trùng thông tin nhập
            KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(t => t.TAIKHOANKH == tk && t.MATKHAUKH == mk);
            if (kh == null)
            {
                ViewBag.errorMassage = "Thông tin không chính xác !";
                //return RedirectToAction("Index");
                return View("Index");
            }
            else
            {
                Session["KhachHang"] = kh;
                return RedirectToAction("Index", "Home");
            }
        }

    }
}
