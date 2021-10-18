using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanHang.Models;
using QuanLyBanHang.Controllers;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using QuanLyBanHang.common;

namespace QuanLyBanHang.Controllers
{
    public class DatHangController : Controller
    {
        qlbanhangEntities db = new qlbanhangEntities();
        // GET: DatHang
        public ActionResult Index()
        {

            
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            int tongTien = 0;
            foreach (CartItem ls in giohang)
            {
                
                tongTien += ls.ThanhTien;
            }
            DonHang dh = new DonHang();
            dh.MaKH = int.Parse(Session["MaKH"].ToString());
            dh.NgayLapHD = DateTime.Now;
            dh.DiaChiGiaoHang = Session["DiaChi"].ToString();
            dh.ThanhTien = tongTien;
            db.DonHangs.Add(dh);
            // Lưu thông tin dứ liệu vào bảng đơn hàng
            db.SaveChanges();
            // Lấy id vừa tạo lưu vào bảng CTDH
            
            int intOrderId = dh.MaDH;
            foreach(var item in giohang)
            {
                CTDH ct = new CTDH();
                
                ct.MaDH = intOrderId;
                ct.MaSP = item.MaSP;
                ct.Soluong = item.SoLuong;
                ct.ThanhTien = item.ThanhTien;
                SanPham sp = db.SanPhams.Find(ct.MaSP);
                sp.SoLuong -= item.SoLuong;

                db.CTDHs.Add(ct);
                db.SaveChanges();
                Session["giohang"] = null;
               Session["count"] = null;

            }
            KhachHang k = db.KhachHangs.Find((int)Session["MaKH"]);
            string shipName = (string)Session["TenKH"];
            string mobile = k.DienThoai;
            string address = dh.DiaChiGiaoHang;
            string email = (string)Session["Email"];
            string money = Convert.ToString(dh.ThanhTien) + " VNĐ";

            sendEmail(shipName, mobile, address, email,money);
            return View();
        }
        public void sendEmail(string shipName, string mobile, string address, string email, string money)
        {
            string content = System.IO.File.ReadAllText(Server.MapPath("~/content/template/neworder.html"));

            content = content.Replace("{{CustomerName}}", shipName);
            content = content.Replace("{{Phone}}", mobile);
            content = content.Replace("{{Email}}", email);
            content = content.Replace("{{Address}}", address);
            content = content.Replace("{{Total}}", money);
            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

            new MailHelper().SendMail(email, "Đơn hàng mới từ Shein Shop", content);
            new MailHelper().SendMail(toEmail, "Đơn hàng mới từ Shein Shop", content);
        }    

    }
}