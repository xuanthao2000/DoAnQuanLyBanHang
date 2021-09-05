using QuanLyBanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyBanHang.Controllers
{
    public class ShirtController : Controller
    {
        qlbanhangEntities db = new qlbanhangEntities();
        // GET: Shirt
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Where(s => s.MaLoaiSP == 3).ToList();
            return View(sanPhams);
        }
        public ActionResult SortDescending()
        {
            var sanPhams = db.SanPhams.Where(s => s.MaLoaiSP == 3);
            var sp = sanPhams.OrderByDescending(s => s.DonGia).ToList();
            return View(sp);
        }
        public ActionResult SortAscending()
        {
            var sanPhams = db.SanPhams.Where(s => s.MaLoaiSP == 3);
            var sp = sanPhams.OrderBy(s => s.DonGia);
            return View(sp);
        }
    }
}