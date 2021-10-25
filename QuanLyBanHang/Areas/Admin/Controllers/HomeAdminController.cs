using QuanLyBanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyBanHang.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        private qlbanhangEntities db = new qlbanhangEntities();
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            // Tổng đơn hàng
            var dh = db.DonHangs.Select(s => s).ToList();
            int tongDH = 0;
            for (int i = 0; i <= dh.Count; i++)
            {
                tongDH++;
            }
            Session["TongDH"] = tongDH;
            // Tổng  tiền
            int? tongTien = 0;
            foreach(var item in dh)
            {
                tongTien += item.ThanhTien;
            }    
            Session["TongTien"] = tongTien;
            // Đơn thành công
            int donThanhCong = 0;
            var dtc = db.DonHangs.Where(s => s.NgayGiaoHang != null).ToList();
            foreach(var item in dtc)
            {
                donThanhCong++;
            }
            Session["donThanhCong"] = donThanhCong;
            if (Session["MaNV"] == null)
                return Redirect("~/Login/Index");
            else
                return View();
        }
    }
}