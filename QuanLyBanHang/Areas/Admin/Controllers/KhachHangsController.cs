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
    public class KhachHangsController : Controller
    {
        private qlbanhangEntities db = new qlbanhangEntities();

        // GET: Admin/KhachHangs
        public ActionResult Index()
        {
            if (Session["MaNV"] == null)
                return Redirect("~/Login/Index");
            else
                return View(db.KhachHangs.ToList());
        }
        public List<KhachHang> DSKH()
        {
            return db.KhachHangs.ToList();
        }
        // GET: Admin/KhachHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // GET: Admin/KhachHangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/KhachHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKH,TenKH,GioiTinh,DiaChi,DienThoai,Email,Password")] KhachHang khachHang)
        {
            KhachHang mailkh = db.KhachHangs.Where(x => x.Email == khachHang.Email).FirstOrDefault();
            Nhanvien mailNV = db.Nhanviens.Where(x => x.Email == khachHang.Email).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (mailkh == null && mailNV == null)
                {
                    db.KhachHangs.Add(khachHang);
                    db.SaveChanges();
                    SetAlert("Thêm khách hàng thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("mailkh", "Email đã tồn tại, vui lòng kiểm tra lại !");
                    ViewBag.Message = "Email đã tồn tại, vui lòng kiểm tra lại !";
                }
            }

            return View(khachHang);
        }

        // GET: Admin/KhachHangs/Edit/5

        public ActionResult Edit(int id)
        {
            KhachHang kh = db.KhachHangs.FirstOrDefault(x => x.MaKH == id);
            return View(kh);
        }
        [HttpPost]
        public ActionResult Edit(KhachHang k)
        {
            KhachHang ukh = db.KhachHangs.Find(k.MaKH);
            ukh.MaKH = k.MaKH;
            ukh.TenKH = k.TenKH;
            ukh.DiaChi = k.DiaChi;
            ukh.DienThoai = k.DienThoai;
            ukh.GioiTinh = k.GioiTinh;
            ukh.Password = k.Password;
            db.SaveChanges();
            SetAlert("Sửa khách hàng thành công", "success");
            return RedirectToAction("Edit");
        }    
        // GET: Admin/KhachHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: Admin/KhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhachHang khachHang = db.KhachHangs.Find(id);
            db.KhachHangs.Remove(khachHang);
            db.SaveChanges();
            SetAlert("Xóa khách hàng thành công", "success");
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
