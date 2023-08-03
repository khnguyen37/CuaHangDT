using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHangDienThoaiDiDong.Models;

namespace QuanLyCuaHangDienThoaiDiDong.ControllersAdmin
{
    public class AdminQuanLyController : Controller
    {
        //
        // GET: /AdminQuanLy/

        QLDienThoaiDataContext data = new QLDienThoaiDataContext();
        public ActionResult QLDonDatHang()
        {
            List<HOADON> dshd = data.HOADONs.ToList();
            return View(dshd);
        }
        //đếm sl cthd
        public ActionResult ThongKe(int mhd)
        {
            List<CHITIETHD> dsct = data.CHITIETHDs.Where(c => c.MAHD == mhd).ToList();

            //thong kê thành tiền
            var thanhtien = (int)dsct.Sum(ct => ct.SOLUONG * ct.SANPHAM.GIABAN);
            ViewBag.tt = thanhtien;
            return PartialView(dsct);
        }
        public ActionResult LietKeHoaDon(int mhd)
        {
            List<CHITIETHD> dsct = data.CHITIETHDs.Where(c => c.MAHD == mhd).ToList();
            return PartialView(dsct);
        }
        [HttpPost]
        public ActionResult CapNhatGiaoHang(FormCollection c)
        {
            int n = int.Parse(c["tong"]);
            for (int i = 1; i <= n; i++)
            {
                string tenck = i.ToString();
                if (c[tenck] != null)
                {
                    //cập nhật tình trạng giao hàng tại hóa đơn có mã hd trong nút checkbox
                    int mhd = int.Parse(c[tenck]);
                    HOADON hd = data.HOADONs.SingleOrDefault(h => h.MAHD == mhd);
                    hd.NGAYTHANHTOAN = DateTime.Now;
                    UpdateModel(hd);
                    data.SubmitChanges();
                }
            }
            return RedirectToAction("QLDonDatHang", "AdminQuanLy");
        }

    }
}
