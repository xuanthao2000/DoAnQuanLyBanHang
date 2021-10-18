using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyBanHang.Controllers;
using System.Web.Mvc;
using QuanLyBanHang.Areas.Admin.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class TestCustomer
    {
        KhachHangsController kh;
        [TestMethod]
        public void Test_DeleteCustomer_Success()
        {
            kh = new KhachHangsController();
            Assert.AreEqual(3, kh.DSKH().Count);
            kh.Delete(20);
            Assert.AreEqual(2, kh.DSKH().Count);
        }
    }
}
