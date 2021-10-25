using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Areas.Admin.Controllers
{
    public class DonHangsController : Controller
    {
        private qlbanhangEntities db = new qlbanhangEntities();

        // GET: Admin/DonHangs
        public ActionResult Index(int? page)
        {
            DonHangCanDuyet();
            var donHangs = db.DonHangs.Include(d => d.KhachHang).Include(d => d.Nhanvien);
            var dHang = donHangs.OrderByDescending(s => s.MaDH).ToList();
      
            // Page
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            page = 1;
            if (Session["MaNV"] == null)
                return Redirect("~/Login/Index");
            else
                return View(dHang.ToPagedList(pageNumber, pageSize));
        }
        public void DonHangCanDuyet()
        {
            
            var a = db.DonHangs.Where(s => s.NgayGiaoHang == null).ToList();
            int b = 0;
            for (int i = 0; i < a.Count; i++)
            {
                b++;
                Session["DHCanDuyet"] = b;
            }
        }    

        // GET: Admin/DonHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cTDH = db.CTDHs.Where(m => m.MaDH == id);
            if (cTDH == null)
            {
                return HttpNotFound();
            }
            return View(cTDH.ToList());
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
            SetAlert("Hủy đơn hàng thành công", "success");
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
            DonHangCanDuyet();
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
            SetAlert("Duyệt đơn hàng thành công", "success");
            return RedirectToAction("Index");
        }
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;

            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }

}

