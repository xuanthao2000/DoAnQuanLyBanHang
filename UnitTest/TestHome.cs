using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyBanHang.Controllers;
using System.Web.Mvc;
using QuanLyBanHang;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class TestHome
    {
        [TestMethod]
        public void Index()
        {
            HomeController home = new HomeController();

            ViewResult result = home.Index("Gori") as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
