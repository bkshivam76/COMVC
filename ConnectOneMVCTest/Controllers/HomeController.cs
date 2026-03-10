using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConnectOneMVC;
using ConnectOneMVC.Controllers;
using ConnectOneMVC.Areas.Profile.Controllers;

namespace ConnectOneMVCTest.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            BankController controller = new BankController();

            // Act
            ViewResult result = controller.Frm_Bank_Info() as ViewResult;

            // Assert
            Assert.AreEqual("Welcome to DevExpress Extensions for ASP.NET MVC!", result.ViewBag.Message);
        }
    }
}
