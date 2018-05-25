using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BloodDonation;
using BloodDonation.Controllers;

namespace BloodDonation.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            LoginController controller = new LoginController();

            // Act
            ViewResult result = controller.IndexAsync() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
