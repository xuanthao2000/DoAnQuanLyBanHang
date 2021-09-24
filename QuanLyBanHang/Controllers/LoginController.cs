using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using QuanLyBanHang.Models;
using System.Net;

namespace QuanLyBanHang.Controllers
{
    public class LoginController : Controller
    {
        qlbanhangEntities db = new qlbanhangEntities();
        // GET: Login
        public ActionResult Index()
        {
            var sp = db.SanPhams.Where(x => x.DangGiamGia == true).FirstOrDefault();
            if (sp != null)
            {
                Session["DangGiamGia"] = sp.DangGiamGia;
            }
            else
            {
                Session["DangGiamGia"] = null;
            }
            return View();
        }

        // GET: SignIn
        [HttpPost]
        public ActionResult SignIn(QuanLyBanHang.Models.KhachHang customer, QuanLyBanHang.Models.Nhanvien emp)
        {
            using (qlbanhangEntities db = new qlbanhangEntities())
            {
                

                var NhanViens = db.Nhanviens.Where(x => x.Email == customer.Email && x.Password == customer.Password).FirstOrDefault();
                var userDetails = db.KhachHangs.Where(x => x.Email == customer.Email && x.Password == customer.Password).FirstOrDefault();

                if (userDetails == null && NhanViens == null)
                {
                    customer.LoginErrorMessage = "Tên tài khoản hoặc mật khẩu không đúng";
                    return View("Index", customer);
                }
                else if (userDetails != null)
                {
                    Session["MaKH"] = userDetails.MaKH;
                    Session["TenKH"] = userDetails.TenKH;
                    Session["DiaChi"] = userDetails.DiaChi;
                    Session["Email"] = userDetails.Email;

                    if(userDetails.GioiTinh == "Nam")
                    {
                        Session["GioiTinh"] = userDetails.GioiTinh;
                    }
                    else {
                        Session["GioiTinh"] = null;
                    }
                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Session["MaNV"] = NhanViens.MaNV;
                    Session["Ten"] = NhanViens.Ten; ;
                    Session["Admin"] = NhanViens.Admin;
                    return RedirectToAction("HomeAdmin", "Admin");
                }
            }
        }


        //create a string MD5

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "MaKH,TenKH,DiaChi,DienThoai,Fax,Email,Password")] KhachHang khachHang)
        {

            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
                ViewBag.Message = "Bạn đã tạo tài khoản thành công";
                return RedirectToAction("SignUp");
            }

            return View(khachHang);
        }
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index", "Login");
        }
        public ActionResult Edit(int? id)
        {
            id = (int)Session["MaKH"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //KhachHang a = db.KhachHangs.Find(id);
            //var pw = a.Password;
            //Session["pwCu"] = pw;
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,Email,TenKH,DiaChi,GioiTinh,DienThoai,Password")] KhachHang khachHang,FormCollection frm)
        {


            string cfPw = frm["confirmPw"].ToString();
            //string oldPw = frm["oldPw"].ToString();
           
            if (ModelState.IsValid)
            {
                //if (oldPw != (string)Session["pw"])
                //{
                    if (khachHang.Password == cfPw)
                    {
                    db.Entry(khachHang).State = EntityState.Modified;
                    db.SaveChanges();
                    }else
                    {
                        khachHang.LoginErrorMessage = "Xác nhận mật khẩu sai";
                    }
                //}
                //else
                //{
                //    khachHang.LoginErrorMessage = "Mật khẩu cũ sai";
                //}
                return RedirectToAction("Edit","Login");
            }
            return View(khachHang);
            
        }
        public ActionResult myOrders()
        {
            //DonHang donHang = db.DonHangs.Find(id);
            var a = (int)Session["MaKH"];
            //var ct = db.DonHangs.Where(m => m.MaKH == (int)Session["MaKH"]).ToList();
            var 
            donHangs = db.DonHangs.Where(m => m.MaKH == a);
            return View(donHangs.ToList());
        }
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
    }
}