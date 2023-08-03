using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHangDienThoaiDiDong.Models;
namespace QuanLyCuaHangDienThoaiDiDong.ControllersAdmin
{
    public class AdminNhapHangController : Controller
    {
        //
        // GET: /AdminNhapHang/

        QLDienThoaiDataContext data = new QLDienThoaiDataContext();
        public ActionResult Index()
        {
            int mapn = data.PHIEUNHAPs.Where(p => p.MAPN > 0).Max(p => p.MAPN);
            List<CHITIETPN> ct = data.CHITIETPNs.Where(t => t.MAPN == mapn).ToList();
            return View(ct);
        }
        [HttpGet]
        public ActionResult ThemPN()
        {
            ViewBag.MANV = new SelectList(data.NHANVIENs.ToList(), "MANV", "HOTEN");
            ViewBag.MANCC = new SelectList(data.NHACCs.ToList(), "MANCC", "TENNCC");
            ViewBag.MASP = new SelectList(data.SANPHAMs.ToList(), "MASP", "TENSP");
            return View();
        }
        [HttpPost]
        public ActionResult XuLyThemPN(PHIEUNHAP pn, FormCollection col)
        {
            try
            {
                pn.NGAYNHAP = DateTime.Now;
                data.PHIEUNHAPs.InsertOnSubmit(pn);
                data.SubmitChanges();
            }
            catch { }
            return RedirectToAction("ThemPN", "AdminNhapHang");
        }
        [HttpPost]
        public ActionResult XuLyThemCTPN(PHIEUNHAP pn, FormCollection col)
        {
            try
            {
                CHITIETPN ct = new CHITIETPN();
                int mapn = data.PHIEUNHAPs.Where(p => p.MAPN > 0).Max(p => p.MAPN);
                ct.MAPN = mapn;
                ct.MASP = int.Parse(col["MASP"]);
                ct.SOLUONG = int.Parse(col["txtSL"]);
                ct.GIANHAP = decimal.Parse(col["txtGN"]);
                ct.THANHTIEN = decimal.Parse(ct.SOLUONG.ToString()) * ct.GIANHAP;
                data.CHITIETPNs.InsertOnSubmit(ct);
                data.SubmitChanges();

            }
            catch { }
            return RedirectToAction("ThemPN", "AdminNhapHang");

        }
        //Xóa CT phiếu nhập
        public ActionResult XoaCTPN(int id, int masp)
        {
            try
            {
                CHITIETPN del = data.CHITIETPNs.SingleOrDefault(t => t.MAPN == id && t.MASP == masp);
                if (del != null)
                {
                    data.CHITIETPNs.DeleteOnSubmit(del);
                    data.SubmitChanges();
                }
            }
            catch { }

            return RedirectToAction("ThemPN", "AdminNhapHang");
        }
        public ActionResult SuaCTPN(int id, int ma)
        {
            CHITIETPN pn = data.CHITIETPNs.FirstOrDefault(t => t.MAPN == id && t.MASP == ma);
            return View(pn);
        }
        [HttpPost]
        public ActionResult XuLySuaCTPN(FormCollection col)
        {
            int ma = int.Parse(col["MASP"]);
            int id = int.Parse(col["MAPN"]);
            int sl = int.Parse(col["SOLUONG"]);
            decimal gia = decimal.Parse(col["GIANHAP"]);

            if (sl != null)
            {
                CHITIETPN ct = data.CHITIETPNs.SingleOrDefault(t => t.MAPN == id && t.MASP == ma);
                ct.SOLUONG = sl;
                ct.GIANHAP = gia;
                ct.THANHTIEN = decimal.Parse(ct.SOLUONG.ToString()) * ct.GIANHAP;
                data.SubmitChanges();
            }
            List<CHITIETPN> ctpn = data.CHITIETPNs.Where(t => t.MAPN != null).ToList();
            return RedirectToAction("ThemPN", "AdminNhapHang", ctpn);
        }
    }
}
