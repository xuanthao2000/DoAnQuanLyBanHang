﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyBanHang.Models
{
    [Serializable]
    public class CartItem
    {
        public string MaSP { get; set; }
        public string HinhSP { get; set; }
        public string TenSP { get; set; }
        public string Size { get; set; }
        public int DonGia { get; set; }
        public int SoLuong { get; set; }
        public int ThanhTien
        {
            get { return SoLuong * DonGia; }
        }
    }
}