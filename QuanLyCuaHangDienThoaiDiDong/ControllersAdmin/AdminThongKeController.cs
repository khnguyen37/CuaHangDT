using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHangDienThoaiDiDong.Models;

namespace QuanLyCuaHangDienThoaiDiDong.ControllersAdmin
{
    public class AdminThongKeController : Controller
    {
        //
        // GET: /AdminThongKe/

        QLDienThoaiDataContext data = new QLDienThoaiDataContext();
        public ActionResult Index()
        {
            List<HOADON> hd = data.HOADONs.ToList();
            var tongdt = (int)hd.Sum(s => s.TONGTIEN);
            ViewBag.tongdt = tongdt;

            return View(hd);
        }
        //thong ke loi nhuan
        public ActionResult ThongKeSL(int mhd)
        {
            List<CHITIETHD> ct = data.CHITIETHDs.Where(t => t.MAHD == mhd).ToList();
            var sl = (int)ct.Sum(s => s.SOLUONG);
            ViewBag.sl = sl;

            return PartialView(ct);
        }

        //Tìm kiếm theo tên khách hàng
        public ActionResult Search()
        {
            ViewBag.MaKH = new SelectList(data.KHACHHANGs.ToList(), "MAKH", "TENKH");
            return View();
        }
        [HttpPost]
        public ActionResult XuLyTimKiem(FormCollection col)
        {
            int MaKH = int.Parse(col["MaKH"].ToString());
            List<HOADON> dsHD = data.HOADONs.Where(t => t.MAKH == MaKH).ToList();
            var tongdt = (int)dsHD.Sum(s => s.TONGTIEN);
            ViewBag.tongdt = tongdt;
            return View("Index", dsHD);
        }
        //Tìm kiếm theo ngày bán
        public ActionResult Search1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XuLyTimKiem1(FormCollection col)
        {
            DateTime date1 = DateTime.Parse(col["txtDate1"]);
            DateTime date2 = DateTime.Parse(col["txtDate2"]);
            List<HOADON> dsHD = data.HOADONs.Where(t => t.NGAYBAN >= date1 && t.NGAYBAN <= date2).ToList();
            var tongdt = (int)dsHD.Sum(s => s.TONGTIEN);
            ViewBag.tongdt = tongdt;
            return View("Index", dsHD);
        }

    }
}
