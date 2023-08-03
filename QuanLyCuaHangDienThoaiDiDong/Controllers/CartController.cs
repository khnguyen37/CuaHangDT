using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHangDienThoaiDiDong.Models;
namespace QuanLyCuaHangDienThoaiDiDong.Controllers
{
    public class CartController : Controller
    {
        //
        // GET: /Cart/

   QLDienThoaiDataContext data = new QLDienThoaiDataContext();
        public ActionResult Index()
        {
            GioHang gh = Session["gio"] as GioHang;
            return View(gh);
        }
        // xác nhận thông tin đặt hàng
        public ActionResult XacNhanThongTin(FormCollection col)
        {
            ViewBag.MANV = new SelectList(data.NHANVIENs.ToList(), "MANV", "HOTEN");

            KHACHHANG kh = Session["KhachHang"] as KHACHHANG;
            if (kh == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(kh);
        }
        [HttpPost]
        public ActionResult LuuDatHang(FormCollection col)
        {
            try
            {
                GioHang gh = (GioHang)Session["gio"];
                KHACHHANG kh = (KHACHHANG)Session["KhachHang"];
                DateTime ngaygiao = DateTime.Parse(col["txtDate"]);
                HOADON hd = new HOADON();
                hd.MANV = int.Parse(col["MANV"].ToString());
                hd.MAKH = kh.MAKH;
                hd.NGAYBAN = DateTime.Now;

                if (ngaygiao >= DateTime.Now)
                {
                    hd.NGAYGIAO = ngaygiao;
                    data.HOADONs.InsertOnSubmit(hd);
                    data.SubmitChanges();
                    foreach (Item sp in gh.lst)
                    {
                        CHITIETHD ct = new CHITIETHD();
                        ct.MAHD = hd.MAHD;
                        ct.MASP = sp.iMaSP;
                        ct.SOLUONG = sp.iSoLuong;
                        ct.DONGIA = decimal.Parse(sp.dDonGia.ToString());
                        ct.THANHTIEN = decimal.Parse(sp.ThanhTien.ToString());
                        data.CHITIETHDs.InsertOnSubmit(ct);

                    }
                    data.SubmitChanges();
                    gh.XoaGioHang();
                    ViewBag.tb = "Đặt hàng thành công";

                }
                else
                {
                    ViewBag.tb = "Ngay giao khong hop le";
                }

                return View();
               
            }
            catch
            {
                ViewBag.tb = "Bạn chưa chọn ngày hẹn giao hàng !!";
                return View();
            }
        }
        //Xóa giỏ hàng
        public ActionResult XoaGH()
        {
            GioHang gh = (GioHang)Session["gio"];
            gh.XoaGioHang();
            return RedirectToAction("Index", "Cart");
        }
        //Xóa SP đã chọn
        public ActionResult Xoa(int id)
        {
            GioHang gh = (GioHang)Session["gio"];
            gh.Xoa(id);
            return RedirectToAction("Index", "Cart");
        }
         //sửa sp đã chọn trong giỏ hàng
        //public ActionResult SuaGH()
        //{
        //    return View();
        //}

        //public ActionResult SuaSPTrongGH(int id, int Soluong)
        //{
        //    GioHang gh = Session["gio"] as GioHang;
        //    if (gh == null)
        //    {
        //        gh = new GioHang();
        //    }
        //    //thêm mặt hàng vào giỏ
        //    gh.Sua(id, Soluong);
        //    Session["gio"] = gh;
        //    return RedirectToAction("Index", "Cart");
        //}
    }
}
