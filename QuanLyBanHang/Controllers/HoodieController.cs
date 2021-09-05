using QuanLyBanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyBanHang.Controllers
{
    public class HoodieController : Controller
    {
        qlbanhangEntities db = new qlbanhangEntities();
        // GET: Hoodie
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Where(s => s.MaLoaiSP == 2).ToList();
            return View(sanPhams);
        }
        public ActionResult SortDescending()
        {
            var sanPhams = db.SanPhams.Where(s => s.MaLoaiSP == 2);
            var sp = sanPhams.OrderByDescending(s => s.DonGia).ToList();
            return View(sp);
        }
        public ActionResult SortAscending()
        {
            var sanPhams = db.SanPhams.Where(s => s.MaLoaiSP == 2);
            var sp = sanPhams.OrderBy(s => s.DonGia);
            return View(sp);
        }
    }
}