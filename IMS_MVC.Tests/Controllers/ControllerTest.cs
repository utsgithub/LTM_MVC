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
            ViewResult result =(ViewResult) controller.About();

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
            Assert.AreEqual("Your contact page.",result.ViewBag.Message);
        }

        [TestCategory("User")]
        [TestMethod]
        public void Users_Index()
        {
            // Arrange
            UsersController controller = new UsersController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual(db.Users.ToList(),result);
        }

        [TestCategory("User")]
        [TestMethod]
        public void User_Details()
        {
            UsersController controller = new UsersController();
            
            ViewResult result = (ViewResult)controller.Details(1);
            Assert.IsNotNull(result);
        }

        [TestCategory("User")]
        [TestMethod]
        public void User_Details_Unvalid_ID()
        {
            UsersController controller = new UsersController();

            ViewResult result =(ViewResult) controller.Details(null) ;
            Assert.IsNotNull(result);
        }

        [TestCategory("User")]
        [TestMethod]
        public void User_Create()
        {
            UsersController controller = new UsersController();

            ViewResult result = (ViewResult)controller.Create();
            Assert.IsNotNull(result);
        }

        [TestCategory("User")]
        [TestMethod]
        public void User_Delete()
        {
            UsersController controller = new UsersController();

            ViewResult result = (ViewResult)controller.Delete(1);
            Assert.IsNotNull(result);
        }

        [TestCategory("User")]
        [TestMethod]
        public void User_Delete_Confirmed()
        {
            UsersController controller = new UsersController();

            ViewResult result = (ViewResult)controller.DeleteConfirmed(1);
            Assert.IsNotNull(result);
        }

    }
}
