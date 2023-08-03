using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHangDienThoaiDiDong.Models;

namespace QuanLyCuaHangDienThoaiDiDong.ControllersAdmin
{
    public class AdminNhanVienController : Controller
    {
        //
        // GET: /AdminNhanVien/

        QLDienThoaiDataContext data = new QLDienThoaiDataContext();
        public ActionResult Index()
        {
            List<NHANVIEN> ds = data.NHANVIENs.ToList();
            return View(ds);
        }
        [HttpGet]
        public ActionResult ThemNV()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XuLyThemNV(NHANVIEN nv)
        {
            data.NHANVIENs.InsertOnSubmit(nv);
            data.SubmitChanges();
            return RedirectToAction("Index", "AdminNhanVien");
        }
        //Xóa nv
        public ActionResult XoaNV(int id)
        {
            try
            {
                NHANVIEN del = data.NHANVIENs.SingleOrDefault(t => t.MANV == id);
                if (del != null)
                {
                    data.NHANVIENs.DeleteOnSubmit(del);
                    data.SubmitChanges();
                }
            }
            catch { }
            return RedirectToAction("Index", "AdminNhanVien");
        }
        // sửa nhân viên
        public ActionResult SuaNV(int id)
        {
            NHANVIEN nv = data.NHANVIENs.SingleOrDefault(t => t.MANV == id);
            return View(nv);
        }
        [HttpPost]
        public ActionResult XuLySuaNV(FormCollection col)
        {
            int id = int.Parse(col["MANV"]);
            var tennv = col["HOTEN"];
            var gioitinh = col["GIOITINH"];
            var diachi = col["DIACHI"];
            var dt = col["DIENTHOAI"];
            decimal luong = decimal.Parse(col["LUONG"]);
            var tk = col["TAIKHOANNV"];
            var mk = col["MATKHAUNV"];
            if (tennv != null && diachi != null && tk != null && mk != null)
            {
                NHANVIEN nv = data.NHANVIENs.SingleOrDefault(t => t.MANV == id);
                nv.HOTEN = tennv;
                nv.GIOITINH = gioitinh;
                nv.DIACHI = diachi;
                nv.DIENTHOAI = dt;
                nv.LUONG = luong;
                nv.TAIKHOANNV = tk;
                nv.MATKHAUNV = mk;
                data.SubmitChanges();
            }
            List<NHANVIEN> snv = data.NHANVIENs.Where(t => t.MANV != null).ToList();
            return RedirectToAction("Index", "AdminNhanVien", snv);
        }

    }
}
