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
    public class CTDHsController : Controller
    {
        private qlbanhangEntities db = new qlbanhangEntities();

        // GET: Admin/CTDHs
        public ActionResult Index()
        {
            var cTDHs = db.CTDHs.Include(c => c.DonHang).Include(c => c.SanPham);
            var ctdh = cTDHs.OrderByDescending(s => s.MaDH).ToList();
            if (Session["MaNV"] == null)
                return Redirect("~/Login/Index");
            else
                return View(ctdh.ToList());
        }

        // GET: Admin/CTDHs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTDH cTDH = db.CTDHs.SingleOrDefault(m => m.MaDH == id);
            if (cTDH == null)
            {
                return HttpNotFound();
            }
            return View(cTDH);
        }

        // GET: Admin/CTDHs/Create

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
