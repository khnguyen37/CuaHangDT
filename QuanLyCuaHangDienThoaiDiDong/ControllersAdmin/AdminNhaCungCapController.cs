using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyCuaHangDienThoaiDiDong.Models;
namespace QuanLyCuaHangDienThoaiDiDong.ControllersAdmin
{
    public class AdminNhaCungCapController : Controller
    {
        //
        // GET: /AdminNhaCungCap/


        QLDienThoaiDataContext data = new QLDienThoaiDataContext();
        public ActionResult Index()
        {
            List<NHACC> ds = data.NHACCs.ToList();
            return View(ds);
        }
        public ActionResult ThemNCC()
        {
            return View();
        }
        [HttpPost]
        public ActionResult XuLyThemNCC(NHACC ncc)
        {
            data.NHACCs.InsertOnSubmit(ncc);
            data.SubmitChanges();
            return RedirectToAction("Index", "AdminNhaCungCap");
        }
        public ActionResult XoaNCC(int id)
        {
            try
            {
                NHACC del = data.NHACCs.SingleOrDefault(t => t.MANCC == id);
                if (del != null)
                {
                    data.NHACCs.DeleteOnSubmit(del);
                    data.SubmitChanges();
                }
            }
            catch { }
            return RedirectToAction("Index", "AdminNhaCungCap");
        }
        // sửa Nhà cung cấp
        public ActionResult SuaNCC(int id)
        {
            NHACC ncc = data.NHACCs.SingleOrDefault(t => t.MANCC == id);
            return View(ncc);
        }
        [HttpPost]
        public ActionResult XuLySuaNCC(FormCollection col)
        {
            int id = int.Parse(col["MANCC"]);
            var tenncc = col["TENNCC"];
            var diachi = col["DCHI"];
            var dt = col["DTHOAI"];

            if (tenncc != null)
            {
                NHACC ncc = data.NHACCs.SingleOrDefault(t => t.MANCC == id);
                ncc.TENNCC = tenncc;
                ncc.DCHI = diachi;
                ncc.DTHOAI = dt;

                data.SubmitChanges();
            }
            List<NHACC> sncc = data.NHACCs.Where(t => t.MANCC != null).ToList();
            return RedirectToAction("Index", "AdminNhaCungCap", sncc);
        }
    }
}
