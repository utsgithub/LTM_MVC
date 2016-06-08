using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMS_MVC;
using IMS_MVC.Controllers;
using IMS_MVC.Models;


namespace IMS_MVC.Tests.Controllers
{

    [TestClass]
    public class ControllerTest
    {
        private ExtraDbContext db = new ExtraDbContext();

        [TestCategory("Home")]
        [TestMethod]
        public void Home_Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = (ViewResult)controller.Index();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestCategory("Home")]
        [TestMethod]
        public void Home_About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = (ViewResult)controller.About();

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestCategory("Home")]
        [TestMethod]
        public void Home_Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = (ViewResult)controller.Contact();

            // Assert
            Assert.AreEqual("Your contact page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void ListAll_FiveInDatabase_ReturnsAll()
        {
            // Set up mocks / stubs

            // Call the controller
            var controller = new EngController();
            //var result = (ViewResult) controller

            // Check the output
            //var model = (List<Client>)
        }
    }
}