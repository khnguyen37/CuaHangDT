using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHangDienThoaiDiDong.Models;
namespace QuanLyCuaHangDienThoaiDiDong.ControllersAdmin
{
    public class AdminSanPhamController : Controller
    {
        //
        // GET: /AdminSanPham/

        QLDienThoaiDataContext data = new QLDienThoaiDataContext();
        public ActionResult Index()
        {
            List<SANPHAM> ds = data.SANPHAMs.ToList();
            ViewBag.MATH = new SelectList(data.THUONGHIEUs.ToList(), "MATH", "TENTH");
            ViewBag.MANCC = new SelectList(data.NHACCs.ToList(), "MANCC", "TENNCC");
            return View(ds);
        }
        //Thêm sp
        [HttpGet]
        public ActionResult ThemSP()
        {
            ViewBag.MATH = new SelectList(data.THUONGHIEUs.ToList(), "MATH", "TENTH");
            ViewBag.MANCC = new SelectList(data.NHACCs.ToList(), "MANCC", "TENNCC");
            return View();
        }
        [HttpPost]
        public ActionResult XuLyThemSP(SANPHAM sp, HttpPostedFileBase UploadFile)
        {
            sp.DVT = "Cái";
            sp.HINHANH = UploadFile.FileName;
            UploadFile.SaveAs(Server.MapPath("/Images/HinhAnh/" + UploadFile.FileName));
            data.SANPHAMs.InsertOnSubmit(sp);
            data.SubmitChanges();
            return RedirectToAction("Index", "AdminSanPham");
        }
        //Xóa SP
        public ActionResult XoaSP(int id)
        {
            SANPHAM del = data.SANPHAMs.SingleOrDefault(t => t.MASP == id);
            if (del != null)
            {
                data.SANPHAMs.DeleteOnSubmit(del);
                data.SubmitChanges();
            }
            return RedirectToAction("Index", "AdminSanPham");
        }
        //Sửa SP
        public ActionResult SuaSP(int id)
        {
            ViewBag.MATH = new SelectList(data.THUONGHIEUs.ToList(), "MATH", "TENTH");
            ViewBag.MANCC = new SelectList(data.NHACCs.ToList(), "MANCC", "TENNCC");
            SANPHAM ssp = data.SANPHAMs.SingleOrDefault(t => t.MASP == id);
            return View(ssp);
        }
        [HttpPost]
        public ActionResult XuLySuaSP(FormCollection col, HttpPostedFileBase UploadFile)
        {
            int id = int.Parse(col["MASP"]);
            var tensp = col["TENSP"];
            int math = int.Parse(col["MATH"]);
            int mancc = int.Parse(col["MANCC"]);
            int sl = int.Parse(col["SOLUONG"]);
            var dvt = col["DVT"];
            decimal giaban = decimal.Parse(col["GIABAN"]);
            if (tensp != null)
            {
                SANPHAM sp = data.SANPHAMs.SingleOrDefault(t => t.MASP == id);
                sp.TENSP = tensp;
                sp.MATH = math;
                sp.MANCC = mancc;
                sp.SOLUONG = sl;
                sp.DVT = dvt;
                sp.GIABAN = giaban;
                sp.HINHANH = UploadFile.FileName;
                UploadFile.SaveAs(Server.MapPath("/Images/HinhAnh/" + UploadFile.FileName));
                data.SubmitChanges();
            }
            List<SANPHAM> ssp = data.SANPHAMs.Where(t => t.MASP != null).ToList();
            return RedirectToAction("Index", "AdminSanPham", ssp);
        }

    }
}
