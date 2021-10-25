using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyBanHang.Models;
using PagedList;
using System.IO;
using QuanLyBanHang.Areas.Admin.Controllers;

namespace QuanLyBanHang.Areas.Admin.Controllers
{
    public class SanPhamsController : Controller
    {
        private qlbanhangEntities db = new qlbanhangEntities();

        // GET: Admin/SanPhams
        public ActionResult Index(string searchString,string currentFilter,int? page)
        {
            sanPhamCanNhap();

            var lstProduct = db.SanPhams.Include(s => s.LoaiSP).ToList();

            // Phân trang
            var spSearch = db.SanPhams.Where(s => s.TenSP.Contains(searchString)).ToList();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            if (Session["MaNV"] == null)
            { 
                return Redirect("~/Login/Index");
            }
            else if (searchString != null)
            {
                page = 1;
                return View(spSearch.ToPagedList(pageNumber,pageSize));
            }
            else
            {
                searchString = currentFilter;
                ViewBag.currentFilter = searchString;
                
                return View(lstProduct.ToPagedList(pageNumber, pageSize));
            }
        }
        public void sanPhamCanNhap()
        {
            var a = db.SanPhams.Where(s => s.SoLuong < 10).ToList();
            int b = 0;
            for (int i = 0; i < a.Count; i++)
            {
                b++;
                Session["SPCannhap"] = b;
            }
        }

        // GET: Admin/SanPhams/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoaiSP");
            return View();
        }

        // POST: Admin/SanPhams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,DonGia,SoLuong,MaLoaiSP,HinhSP")] SanPham sanPham,HttpPostedFileBase HinhSP)
        {

            if (ModelState.IsValid)
            {
                if(HinhSP != null && HinhSP.ContentLength >0)
                {
                    string fileName = Path.GetFileName(HinhSP.FileName);
                    string path = Server.MapPath("~/Images/" + fileName);
                    sanPham.HinhSP = "Images/" + fileName;
                    HinhSP.SaveAs(path);
                }
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                SetAlert("Thêm sản phẩm thành công","success");
                return RedirectToAction("Index");
                
            }

            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoaiSP", sanPham.MaLoaiSP);
            return View(sanPham);
        }
        
        // GET: Admin/SanPhams/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoaiSP", sanPham.MaLoaiSP);
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,DonGia,SoLuong,MaLoaiSP,HinhSP")] SanPham sanPham, HttpPostedFileBase HinhUpload,string HinhSP)
        {
            if (ModelState.IsValid)
            {
                if (HinhUpload != null && HinhUpload.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(HinhUpload.FileName);
                    string path = Server.MapPath("~/Images/" + fileName);
                    sanPham.HinhSP = "Images/" + fileName;
                    HinhUpload.SaveAs(path);
                }
                else
                {
                    sanPham.HinhSP = HinhSP;
                }
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Sửa sản phẩm thành công", "success");
                return RedirectToAction("Edit");
            }
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSPs, "MaLoaiSP", "TenLoaiSP", sanPham.MaLoaiSP);
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {

            SanPham sanPham = db.SanPhams.Find(id);
            var ct = db.CTDHs.Where(m => m.MaSP == id).ToList();
            for (int i = 0; i < ct.Count; i++)
            {
                db.CTDHs.Remove(ct[i]);
            }
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            SetAlert("Xóa sản phẩm thành công", "success");
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
        public void SetAlert(string message, string type)
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
