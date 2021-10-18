using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyBanHang.Models;
using QuanLyBanHang.Controllers;

namespace QuanLyBanHang.Controllers
{
    public class HomeController : Controller
    {
        qlbanhangEntities db = new qlbanhangEntities();
        // GET: Home
        public ActionResult Index(string search)
        {
            
            if (search == null)
            {
                var sanPhams = db.SanPhams.Include(s => s.LoaiSP);
                return View(sanPhams.ToList());
            }
            else
            {
                return View(db.SanPhams.Where(s => s.TenSP.Contains(search)));
            }
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            Session["MaSP"] = id;
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            
            return View(sanPham);
        }

        [HttpPost]
        public RedirectToRouteResult AddToCart(FormCollection frm)
        {
            string MaSP = frm["MaSP"].ToString();
            SanPham sanpham = db.SanPhams.Find(MaSP);
            if(sanpham.SoLuong > 0 )
            { 
            if (Session["giohang"] == null)
            {
                Session["giohang"] = new List<CartItem>();
                Session["count"] = 0;

            }
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            SanPham sp = db.SanPhams.Find(MaSP);
            int sl = Convert.ToInt16(frm["sl"]);
            //Kiểm tra sản phẩm này có trong giỏ hàng chưa
            if (giohang.FirstOrDefault(m => m.MaSP == MaSP) == null)
            {
                
                CartItem newItem = new CartItem();
                newItem.MaSP = MaSP;
                newItem.HinhSP = sanpham.HinhSP;
                newItem.TenSP = sanpham.TenSP;
                if (sp.SoLuong >= sl)
                {
                    newItem.SoLuong = sl;
                }
                else
                {
                    newItem.SoLuong = (int)sp.SoLuong;
                }
                newItem.DonGia = Convert.ToInt32(sp.DonGia);
                giohang.Add(newItem);
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            }
            else
            {
                CartItem cartItem = giohang.FirstOrDefault(m => m.MaSP == MaSP);

                if (cartItem.SoLuong+sl <= sp.SoLuong)
                {
                    cartItem.SoLuong += sl;
                }
                else
                {
                    cartItem.SoLuong = (int)sp.SoLuong;
                }
            }

            Session["giohang"] = giohang;
            }
            return RedirectToAction("Index", "giohang");
        }
        
    }
}
