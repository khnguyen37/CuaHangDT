using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHangDienThoaiDiDong.Models;

namespace QuanLyCuaHangDienThoaiDiDong.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        QLDienThoaiDataContext data = new QLDienThoaiDataContext();

        public ActionResult Index()
        {
            List<SANPHAM> dssp = data.SANPHAMs.ToList();
            return View(dssp);

        }
        //hien thi chi tiet sp
        public ActionResult HienThiChiTiet(int matl)
        {
            SANPHAM sp = data.SANPHAMs.FirstOrDefault(t => t.MASP == matl);

            //hien thi san pham lien quan
            List<SANPHAM> ds1 = data.SANPHAMs.Where(i => i.MATH == sp.MATH).ToList();
            //truyền qua viewbag
            ViewBag.lst1 = ds1;
            return View(sp);
        }
        //hien thi danh sach ten thuong hieu
        public ActionResult HTThuongHieu()
        {
            List<THUONGHIEU> dsL = data.THUONGHIEUs.ToList();
            return PartialView(dsL);
        }
        //loc san pham theo loai 
        public ActionResult HTSanPhamTheoTH(int ml)
        {
            List<SANPHAM> dsSP = data.SANPHAMs.Where(t => t.MATH == ml).ToList();
            return View("Index", dsSP);
        }
        //Đăng xuất
        public ActionResult DangXuat()
        {
            Session["KhachHang"] = null;
            return RedirectToAction("Index", "Home");
        }
        //Tìm kiếm
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XuLyTimKiem(FormCollection col)
        {
            string tk = col["txtTuKhoa"].ToString();
            List<SANPHAM> dsSP = data.SANPHAMs.Where(t => t.TENSP.Contains(tk) == true).ToList();

            return View("Index", dsSP);
        }
        //Thêm sản phẩm vào giỏ hàng
        public ActionResult ThemVaoGH(int id)
        {
            GioHang gh = Session["gio"] as GioHang;
            if (gh == null)
            {
                gh = new GioHang();
            }
            //thêm mặt hàng vào giỏ
            gh.Them(id);
            Session["gio"] = gh;
            return RedirectToAction("Index", "Home");
        }
        //Thêm sản phẩm vào giỏ hàng khi nhap vao sl
        public ActionResult ThemVaoGHTheoSL(int id, int Soluong)
        {
            GioHang gh = Session["gio"] as GioHang;
            if (gh == null)
            {
                gh = new GioHang();
            }
            //thêm mặt hàng vào giỏ
            gh.ThemSPTheoSL(id, Soluong);
            Session["gio"] = gh;
            return RedirectToAction("Index", "Home");
        }
    }
}
