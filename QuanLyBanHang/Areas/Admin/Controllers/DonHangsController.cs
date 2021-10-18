using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Areas.Admin.Controllers
{
    public class DonHangsController : Controller
    {
        private qlbanhangEntities db = new qlbanhangEntities();

        // GET: Admin/DonHangs
        public ActionResult Index()
        {
            var donHangs = db.DonHangs.Include(d => d.KhachHang).Include(d => d.Nhanvien).ToList();
            // Tổng đơn hàng
            var dh = db.DonHangs.Select(s =>s).ToList();
            int tongDH = 0;
            for(int i=0;i<=dh.Count;i++)
            {
                tongDH++;
            }
            Session["TongDH"] = tongDH;
            // Tổng  tiền
            var thanhtien = db.DonHangs.Select(s => s).ToList();
            int tongTien = 0;
            for (int i = 0; i <= dh.Count; i++)
            {
                tongTien += i;
            }
            Session["TongTien"] = tongTien;


            return View(donHangs);
        }

        // GET: Admin/DonHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // GET: Admin/DonHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH");
            ViewBag.MaNV = new SelectList(db.Nhanviens, "MaNV", "HoNV");
            return View();
        }

        // POST: Admin/DonHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDH,MaKH,MaNV,NgayLapHD,NgayGiaoHang,DiaChiGiaoHang")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.DonHangs.Add(donHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", donHang.MaKH);
            ViewBag.MaNV = new SelectList(db.Nhanviens, "MaNV", "HoNV", donHang.MaNV);
            return View(donHang);
        }

        // GET: Admin/DonHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // POST: Admin/DonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonHang donHang = db.DonHangs.Find(id);

            var ct = db.CTDHs.Where(m => m.MaDH == id).ToList();
            for(int i=0;i<ct.Count;i++)
            {
                db.CTDHs.Remove(ct[i]);
            }
            db.DonHangs.Remove(donHang);
            
            db.SaveChanges();
            return RedirectToAction("Index");

            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult UnapprovedOrder()
        {
            var donHangs = db.DonHangs.Where(s => s.NgayGiaoHang == null);
            //var sp = sanPhams.OrderByDescending(s => s.DonGia).ToList();
            return View(donHangs.ToList());
        }
        public ActionResult ApprovedOrder()
        {
            var donHangs = db.DonHangs.Where(s => s.NgayGiaoHang != null);
            //var sp = sanPhams.OrderByDescending(s => s.DonGia).ToList();
            return View(donHangs.ToList());
        }
        public ActionResult Browser(int id)
        {
            DonHang donHang = db.DonHangs.Find(id);
            donHang.NgayGiaoHang = DateTime.Now;
            donHang.MaNV = (int)Session["MaNV"];
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
