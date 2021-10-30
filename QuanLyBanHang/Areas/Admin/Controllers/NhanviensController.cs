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
    public class NhanviensController : Controller
    {
        private qlbanhangEntities db = new qlbanhangEntities();

        // GET: Admin/Nhanviens
        public ActionResult Index()
        {
            var nv = db.Nhanviens.Where(s => s.Admin == false).ToList();
            if (Session["MaNV"] == null)
                return Redirect("~/Login/Index");
            else
                return View(nv);

        }

        // GET: Admin/Nhanviens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nhanvien nhanvien = db.Nhanviens.Find(id);
            if (nhanvien == null)
            {
                return HttpNotFound();
            }
            return View(nhanvien);
        }

        // GET: Admin/Nhanviens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Nhanviens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNV,HoNV,Ten,Diachi,Dienthoai,Email,Password,Admin")] Nhanvien nhanvien)
        {
            Nhanvien mailNV = db.Nhanviens.Where(x => x.Email == nhanvien.Email).FirstOrDefault();
            KhachHang mailKH = db.KhachHangs.Where(x => x.Email == nhanvien.Email).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if(mailNV == null && mailKH == null)
                {  
                    db.Nhanviens.Add(nhanvien);
                    db.SaveChanges();
                    SetAlert("Thêm nhân viên thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Email đã tồn tại, vui lòng kiểm tra lại !";
                }
            }

            return View(nhanvien);
        }

        // GET: Admin/Nhanviens/Edit/5
        public ActionResult Edit(int id)
        {
            Nhanvien kh = db.Nhanviens.FirstOrDefault(x => x.MaNV == id);
            return View(kh);
        }
        [HttpPost]
        public ActionResult Edit(Nhanvien n)
        {
            Nhanvien unv = db.Nhanviens.Find(n.MaNV);
            unv.MaNV = n.MaNV;
            unv.HoNV = n.HoNV;
            unv.Ten = n.Ten;
            unv.Diachi = n.Diachi;
            unv.Dienthoai = n.Dienthoai;
            unv.Password = n.Password;
            db.SaveChanges();
            SetAlert("Sửa nhân viên thành công", "success");
            return RedirectToAction("Edit");
        }

        // GET: Admin/Nhanviens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nhanvien nhanvien = db.Nhanviens.Find(id);
            if (nhanvien == null)
            {
                return HttpNotFound();
            }
            return View(nhanvien);
        }

        // POST: Admin/Nhanviens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nhanvien nhanvien = db.Nhanviens.Find(id);
            db.Nhanviens.Remove(nhanvien);
            db.SaveChanges();
            SetAlert("Xóa nhân viên thành công", "success");
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
        public ActionResult EditInfo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nhanvien nhanvien = db.Nhanviens.Find(id);
            if (nhanvien == null)
            {
                return HttpNotFound();
            }
            return View(nhanvien);
        }

        // POST: Admin/Nhanviens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInfo([Bind(Include = "MaNV,HoNV,Ten,Diachi,Dienthoai,Email,Password,Admin")] Nhanvien nhanvien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanvien).State = EntityState.Modified;
                nhanvien.Admin = (bool)Session["Admin"];
                db.SaveChanges();
                SetAlert("Cập nhật thông tin thành công", "success");
                return RedirectToAction("EditInfo");
            }
            return View(nhanvien);
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
