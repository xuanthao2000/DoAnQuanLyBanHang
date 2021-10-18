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
        public ActionResult Index(string search)
        {
            if (search == null)
            {
                var sanPhams = db.SanPhams.Where(s => s.MaLoaiSP == 2).ToList();
                return View(sanPhams);
            }
            else
            {
                return View(db.SanPhams.Where(s => s.TenSP.Contains(search)).Where(s => s.MaLoaiSP == 2));
            }
        }
        public ActionResult SortDescending(string search)
        {
            if (search == null)
            {
                var sanPhams = db.SanPhams.Where(s => s.MaLoaiSP == 2);
                var sp = sanPhams.OrderByDescending(s => s.DonGia).ToList();
                return View(sp);
             }
            else
            {
                return View(db.SanPhams.Where(s => s.TenSP.Contains(search)).Where(s => s.MaLoaiSP == 2).OrderByDescending(s => s.DonGia));
            }
}
        public ActionResult SortAscending(string search)
        {
            if (search == null)
            {
                var sanPhams = db.SanPhams.Where(s => s.MaLoaiSP == 2);
                var sp = sanPhams.OrderBy(s => s.DonGia);
                return View(sp);
            }
            else
            {
                return View(db.SanPhams.Where(s => s.TenSP.Contains(search)).Where(s => s.MaLoaiSP == 2).OrderBy(s => s.DonGia));
            }
}
    }
}