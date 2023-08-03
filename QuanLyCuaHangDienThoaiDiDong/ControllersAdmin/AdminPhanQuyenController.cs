using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHangDienThoaiDiDong.Models;
namespace QuanLyCuaHangDienThoaiDiDong.ControllersAdmin
{
    public class AdminPhanQuyenController : Controller
    {
        //
        // GET: /AdminPhanQuyen/
        QLDienThoaiDataContext data = new QLDienThoaiDataContext();
        public ActionResult Index()
        {
            List<PHANQUYEN> dspq = data.PHANQUYENs.ToList();
            ViewBag.MANV = new SelectList(data.NHANVIENs.ToList(), "MANV", "HOTEN");
            return View(dspq);
        }
        public ActionResult PhanQuyen(int id)
        {
            ViewBag.MANV = new SelectList(data.NHANVIENs.ToList(), "MANV", "HOTEN");
            PHANQUYEN pq = data.PHANQUYENs.SingleOrDefault(t => t.ID == id);
            return View(pq);
        }
        [HttpPost]
        public ActionResult XuLyPhanQuyen(FormCollection col)
        {
            int id = int.Parse(col["ID"]);

            bool qldh = bool.Parse(col["QL_DONHANG"]);
            bool qlsp = bool.Parse(col["QL_SANPHAM"]);
            bool qlkh = bool.Parse(col["QL_KHACHHANG"]);
            bool qlncc = bool.Parse(col["QL_NHACC"]);
            bool qlnh = bool.Parse(col["QL_NHAPHANG"]);
            bool tk = bool.Parse(col["THONGKE"]);

                PHANQUYEN pq = data.PHANQUYENs.SingleOrDefault(t => t.ID == id);
                pq.QL_DONHANG = qldh;
                pq.QL_SANPHAM = qlsp;
                pq.QL_KHACHHANG = qlkh;
                pq.QL_NHACC = qlncc;
                pq.QL_NHAPHANG = qlnh;
                pq.THONGKE = tk;
                data.SubmitChanges();

            List<PHANQUYEN> spq = data.PHANQUYENs.Where(t => t.MANV != null).ToList();

            return RedirectToAction("Index", "AdminPhanQuyen");
        }
    }
}
