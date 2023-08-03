using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHangDienThoaiDiDong.Models;

namespace QuanLyCuaHangDienThoaiDiDong.ControllersAdmin
{
    public class AdminKhachHangController : Controller
    {
        //
        // GET: /AdminKhachHang/

        QLDienThoaiDataContext data = new QLDienThoaiDataContext();
        public ActionResult Index()
        {
            List<KHACHHANG> ds = data.KHACHHANGs.ToList();
            return View(ds);
        }
        public ActionResult ThemKH()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XuLyThemKH(KHACHHANG kh)
        {
            data.KHACHHANGs.InsertOnSubmit(kh);
            data.SubmitChanges();
            return RedirectToAction("Index", "AdminKhachHang");
        }
        public ActionResult XoaKH(int id)
        {
            try
            {
                KHACHHANG del = data.KHACHHANGs.SingleOrDefault(t => t.MAKH == id);
                if (del != null)
                {
                    data.KHACHHANGs.DeleteOnSubmit(del);
                    data.SubmitChanges();
                }
            }
            catch { }
            return RedirectToAction("Index", "AdminKhachHang");
        }
        // sửa Khách hàng
        public ActionResult SuaKH(int id)
        {
            KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(t => t.MAKH == id);
            return View(kh);
        }
        [HttpPost]
        public ActionResult XuLySuaKH(FormCollection col)
        {
            int id = int.Parse(col["MAKH"]);
            var tenkh = col["TENKH"];
            var gioitinh = col["GIOITINH"];
            var diachi = col["DIACHI"];
            var dt = col["DIENTHOAI"];
            var tk = col["TAIKHOANKH"];
            var mk = col["MATKHAUKH"];
            if (tenkh != null)
            {
                KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(t => t.MAKH == id);
                kh.TENKH = tenkh;
                kh.GIOITINH = gioitinh;
                kh.DIACHI = diachi;
                kh.DIENTHOAI = dt;
                kh.TAIKHOANKH = tk;
                kh.MATKHAUKH = mk;
                data.SubmitChanges();
            }
            List<KHACHHANG> skh = data.KHACHHANGs.Where(t => t.MAKH != null).ToList();
            return RedirectToAction("Index", "AdminKhachHang", skh);
        }

    }
}
