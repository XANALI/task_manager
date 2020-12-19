using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using task_manager.Controllers;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private UserController controller;
        private ViewResult result;

        [TestInitialize]
        public void SetupContext()
        {
            controller = new UserController();
            result = controller.Index() as ViewResult;
        }


        [TestMethod]
        public void IndexViewResultNotNull()
        {
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexViewEqualIndexCshtml()
        {
            Assert.AreEqual("Index", result.ViewName);
        }

    }
}
