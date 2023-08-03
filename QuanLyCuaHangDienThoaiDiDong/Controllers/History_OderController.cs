using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHangDienThoaiDiDong.Models;
namespace QuanLyCuaHangDienThoaiDiDong.Controllers
{
    public class History_OderController : Controller
    {
        //
        // GET: /History_Oder/

        QLDienThoaiDataContext data = new QLDienThoaiDataContext();
        //Load thông tin lịch sử mua hàng
        public ActionResult Index()
        {
            KHACHHANG kh = (KHACHHANG)Session["KhachHang"];
            int kh1 = int.Parse(kh.MAKH.ToString());
            using (QLDienThoaiDataContext data = new QLDienThoaiDataContext())
            {
                List<SANPHAM> sp = data.SANPHAMs.ToList();
                List<HOADON> hd = data.HOADONs.ToList();
                List<CHITIETHD> ct = data.CHITIETHDs.ToList();
                var obj = from h in hd
                          join c in ct on h.MAHD equals c.MAHD
                          join s in sp on c.MASP equals s.MASP
                          where h.MAKH == kh1
                          select new Class1
                          {
                              hoadon = h,
                              cthd = c,
                              sanpham = s
                          };
                return View(obj);
            }
        }

    }
}
