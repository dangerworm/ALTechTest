using System;
using System.Collections.Generic;
using System.Text;
using ALTechTest.ViewModels;
using Moq;
using NUnit.Framework;

namespace ALTechTestTests
{
    public class WorkViewModelTests
    {
        private Mock<WorkViewModel> MockWorkViewModel { get; set; }

        [SetUp]
        public void Setup()
        {
            MockWorkViewModel = new Mock<WorkViewModel>();
        }

        [Test]
        public void Test1()
        {
            // Arrange
            // Act
            // Assert
            Assert.Pass();
        }
    }
}
