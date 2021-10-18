using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanHang.Models;
using QuanLyBanHang.Controllers;

namespace QuanLyBanHang.Controllers
{
    public class GioHangController : Controller
    {
        private qlbanhangEntities db = new qlbanhangEntities();
        // GET: GioHang
        public ActionResult Index()
        {
            if (Session["MaKH"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                List<CartItem> giohang = Session["giohang"] as List<CartItem>;
                return View(giohang);
            }
        }
        // Khai báo phương thức thêm sản phẩm vào giỏ hàng

        public RedirectToRouteResult Update(string MaSP, int txtSoLuong,QuanLyBanHang.Models.SanPham product)
        {
            SanPham sp = db.SanPhams.Find(MaSP);

            // Tìm Cartem 
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem item = giohang.FirstOrDefault(m => m.MaSP == MaSP);
            if (item != null )
            {
                if (sp.SoLuong >= txtSoLuong)
                {
                    item.SoLuong = txtSoLuong;
                    Session["giohang"] = giohang;
                }
                else
                {
                    product.SLErrorMessage = "Số lượng sản phẩm không đủ để đặt";
                }

            }
            
            return RedirectToAction("Index");
        }
        public RedirectToRouteResult DelCartItem(string MaSP)
        {
            // Tìm CartItem muốn xóa
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem item = giohang.FirstOrDefault(m => m.MaSP == MaSP);
            if (item != null)
            {
                giohang.Remove(item);
                Session["giohang"] = giohang;
                Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            }
            return RedirectToAction("Index");
        }
    }
}