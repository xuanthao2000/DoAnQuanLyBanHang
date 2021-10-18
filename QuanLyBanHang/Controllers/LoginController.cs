﻿using System;
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
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using QuanLyBanHang.common;

namespace QuanLyBanHang.Controllers
{
    public class LoginController : Controller
    {
        qlbanhangEntities db = new qlbanhangEntities();


        // GET: Login
        public ActionResult Index()
        {

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
                    customer.LoginErrorMessage = "Tên tài khoản và mật khẩu không được bỏ trống.";
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
                    Session["EmailNv"] = NhanViens.Email;
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
        public ActionResult SignUp([Bind(Include = "MaKH,TenKH,DiaChi,DienThoai,GioiTinh,Email,Password")] KhachHang khachHang)
        {
            
            KhachHang mailkh = db.KhachHangs.Where(x => x.Email == khachHang.Email).FirstOrDefault();
            Nhanvien mailNV = db.Nhanviens.Where(x => x.Email == khachHang.Email).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if(mailkh == null && mailNV == null)
                {
                    db.KhachHangs.Add(khachHang);
                    db.SaveChanges();
                    ViewBag.Message = "Bạn đã tạo tài khoản thành công";
                    return RedirectToAction("SignUp");
                }
                else
                {
                    ModelState.AddModelError("mailkh","Email đã tồn tại, vui lòng kiểm tra lại !");
                    ViewBag.Message = "Email đã tồn tại, vui lòng kiểm tra lại !";
                }

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
        public ActionResult Edit([Bind(Include = "MaKH,TenKH,DiaChi,GioiTinh,DienThoai,Password")] KhachHang khachHang,FormCollection frm)
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
                    khachHang.Email = (string)Session["Email"];
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
            for (int i = 0; i < ct.Count; i++)
            {
                db.CTDHs.Remove(ct[i]);
            }
            db.DonHangs.Remove(donHang);

            db.SaveChanges();
            return RedirectToAction("myOrders");


        }
        public ActionResult ForgotPw()
        {
            return View() ;
        }
        public string random()
        {
            //int Numrd;
            //Numrd = rd.Next(9999, 100000);

            string Numrd_str;
            Random rd = new Random();
            Numrd_str = rd.Next(9999, 100000).ToString();
            return Numrd_str;
        }
        [HttpPost]
        public RedirectToRouteResult sendEmail(FormCollection frm)
        {
            string email = frm["email"].ToString();

            string content = System.IO.File.ReadAllText(Server.MapPath("~/content/Template/forgotPw.html"));
            string newPass = random();
            content = content.Replace("{{NewPassword}}", newPass);
            content = content.Replace("{{Email}}", email);
            //content = content.Replace("{{Total}}", total.ToString("N0"));
            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

            new MailHelper().SendMail(email, "Đơn hàng mới từ Shein Shop", content);
            new MailHelper().SendMail(toEmail, "Mật khẩu mới từ Shein Shop", content);

            KhachHang kh = db.KhachHangs.Where(s => s.Email == email).SingleOrDefault();
            kh.Password = newPass;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}