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
        public ActionResult Index(string search)
        {
            if (search == null)
            {
                var sanPhams = db.SanPhams.Where(s => s.MaLoaiSP == 3).ToList();
                return View(sanPhams);
            }
            else
            {
                return View(db.SanPhams.Where(s => s.TenSP.Contains(search)).Where(s => s.MaLoaiSP == 3));
            }
        }
        public ActionResult SortDescending(string search)
        {
            if (search == null)
            {
                var sanPhams = db.SanPhams.Where(s => s.MaLoaiSP == 3);
                var sp = sanPhams.OrderByDescending(s => s.DonGia).ToList();
                return View(sp);
            }
            else
            {
                return View(db.SanPhams.Where(s => s.TenSP.Contains(search)).Where(s => s.MaLoaiSP == 3).OrderByDescending(s => s.DonGia));
            }
        }
        public ActionResult SortAscending(string search)
        {
            if (search == null)
            {
                var sanPhams = db.SanPhams.Where(s => s.MaLoaiSP == 3);
                var sp = sanPhams.OrderBy(s => s.DonGia).ToList();
                return View(sp);
            }
            else
            {
                return View(db.SanPhams.Where(s => s.TenSP.Contains(search)).Where(s => s.MaLoaiSP == 3).OrderBy(s => s.DonGia));
            }
        }
    }
}