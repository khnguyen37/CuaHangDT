using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHangDienThoaiDiDong.Models;
namespace QuanLyCuaHangDienThoaiDiDong.ControllersAdmin
{
    public class AdminLoginController : Controller
    {
        //
        // GET: /AdminLogin/

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
            NHANVIEN nv = data.NHANVIENs.SingleOrDefault(t => t.TAIKHOANNV == tk && t.MATKHAUNV == mk);
            
            if (nv == null)
            {
                ViewBag.errorMassage = "Thông tin không chính xác !";
                //return RedirectToAction("Index");
                return View("Index");
            }
            else
            {
                PHANQUYEN pq = data.PHANQUYENs.SingleOrDefault(t => t.MANV == nv.MANV);
                Session["NhanVien"] = nv;
                Session["PhanQuyen"] = pq;
                return RedirectToAction("Index", "AdminHome");
            }
        }
    }
}
