using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace QuanLyBanHang.Models
{
    public class CartModel
    {
        public SanPham SanPham { get; set; }
        public int SoLuong { get; set; }
        public int DonGia { get; set; }
    }
}