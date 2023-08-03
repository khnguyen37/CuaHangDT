using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyCuaHangDienThoaiDiDong.Models
{
    public class Item
    {
        public int iMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sHinhAnh { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double ThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }
        QLDienThoaiDataContext data = new QLDienThoaiDataContext();
        public Item(int MaSP)
        {
            SANPHAM sp = data.SANPHAMs.Single(n => n.MASP == MaSP);
            if (sp != null)
            {
                iMaSP = MaSP;
                sTenSP = sp.TENSP;
                sHinhAnh = sp.HINHANH;
                dDonGia = double.Parse(sp.GIABAN.ToString());
                iSoLuong = 1;
            }

        }
    }
    public class GioHang
    {
        public List<Item> lst;
        public GioHang()
        {
            lst = new List<Item>();
        }
        public GioHang(List<Item> lstGH)
        {
            if (lstGH == null)
            {
                lst = new List<Item>();
            }
            else
            {
                lst = lstGH;
            }
        }
        public int SoMatHang()
        {
            if (lst == null)
                return 0;
            return lst.Count;
        }
        public int TongSLHang()
        {
            int iTongSoLuong = 0;
            if (lst != null)
            {
                iTongSoLuong = lst.Sum(n => n.iSoLuong);
            } return iTongSoLuong;
        }
        public double TongThanhTien()
        {
            double iTongTien = 0;
            if (lst != null)
            {
                iTongTien = lst.Sum(n => n.ThanhTien);
            } return iTongTien;
        }
        public int Them(int iMaSP)
        {
            Item sanpham = lst.Find(n => n.iMaSP == iMaSP);
            if (sanpham == null)
            {
                Item sp = new Item(iMaSP);
                if (sp == null)
                    return -1;
                lst.Add(sp);
            }
            else
            {
                sanpham.iSoLuong++;
            } return 1;
        }
        public int ThemSPTheoSL(int iMaSP, int SoLuong)
        {
            Item sanpham = lst.Find(n => n.iMaSP == iMaSP);
            if (sanpham == null)
            {
                Item sp = new Item(iMaSP);
                sp.iSoLuong = SoLuong;
                if (sp == null)
                    return -1;
                lst.Add(sp);
            }
            else
            {
                sanpham.iSoLuong = SoLuong;
            } return 1;
        }
        public int Xoa(int iMaSP)
        {
            Item sanpham = lst.Find(n => n.iMaSP == iMaSP);
            if (sanpham != null)
            {
                lst.Remove(sanpham);
                return 1;
            } return -1;
        }
        public int Sua(int MaSP, int SoLuong)
        {
            Item sanpham = lst.Find(n => n.iMaSP == MaSP);
            if (sanpham != null)
            {
                sanpham.iSoLuong = SoLuong;
                return 1;
            } return -1;
        }
        public void XoaGioHang()
        {
            lst.Clear();
        }
    }
}