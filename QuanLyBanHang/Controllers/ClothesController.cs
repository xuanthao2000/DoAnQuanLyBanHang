using QuanLyBanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyBanHang.Controllers
{
    public class ClothesController : Controller
    {
        
        qlbanhangEntities db = new qlbanhangEntities();
        // GET: AoSoMi
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AoThun()
        {
            var sanPhams = db.SanPhams.Where(s => s.MaLoaiSP == 1).ToList();
            return View(sanPhams);
        }
        // GET: AoSoMi
        public ActionResult TuiSach()
        {
            var sanPhams = db.SanPhams.Where(s => s.MaLoaiSP == 4).ToList();
            return View(sanPhams);
        }
        // GET: AoSoMi
        public ActionResult SoMi()
        {
            var sanPhams = db.SanPhams.Where(s => s.MaLoaiSP == 3).ToList();
            return View(sanPhams);
        }
        public ActionResult Hoodie()
        {
            var sanPhams = db.SanPhams.Where(s => s.MaLoaiSP == 2).ToList();
            return View(sanPhams);
        }
    }
}