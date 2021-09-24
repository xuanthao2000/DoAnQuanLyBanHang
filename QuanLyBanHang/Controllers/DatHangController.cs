using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanHang.Models;
using QuanLyBanHang.Controllers;

namespace QuanLyBanHang.Controllers
{
    public class DatHangController : Controller
    {
        qlbanhangEntities obj = new qlbanhangEntities();
        // GET: DatHang
        public ActionResult Index()
        {
            // Lấy thông tin dữ liệu từ biến
            //var listCart = (List<CartModel>)Session["giohang"];
            // Gán dữ liệu cho đặt hàng
            DonHang dh = new DonHang();
            dh.MaKH = int.Parse(Session["MaKH"].ToString());
            dh.NgayLapHD = DateTime.Now;
            dh.DiaChiGiaoHang = Session["DiaChi"].ToString();
            obj.DonHangs.Add(dh);
            // Lưu thông tin dứ liệu vào bảng đơn hàng
            obj.SaveChanges();
            // Lấy id vừa tạo lưu vào bảng CTDH
            int intOrderId = dh.MaDH;
            
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            foreach(var item in giohang)
            {
                
                CTDH ct = new CTDH();
                
                ct.MaDH = intOrderId;
                ct.MaSP = item.MaSP;
                ct.Soluong = item.SoLuong;
                ct.ThanhTien = item.ThanhTien;
                SanPham sp = obj.SanPhams.Find(ct.MaSP);
                sp.SoLuong -= item.SoLuong;

                obj.CTDHs.Add(ct);
                obj.SaveChanges();
                Session["giohang"] = null;
               Session["count"] = null;

            }
            
            return View();
        }
    }
}