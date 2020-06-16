using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PostcardGenerator;

namespace PostcardGEneratorTests
{
    [TestClass]
    public class PostcardGeneratorTests
    {
        /// <summary>
        /// Nothing more than a test mothod for testing IsRight function, which tests for numbers that are zero.
        /// </summary>
        [TestMethod]
        public void DivideByZero()
        {
            //MainWindow main = new MainWindow();
            //bool left = main.IsRight(1);
            //Assert.IsTrue(left);

            bool left = MainWindow.IsRight(1);
            //Assert.IsTrue(left);

            left = MainWindow.IsRight(0);
            Assert.IsTrue(left);
        }
        /// <summary>
        /// Nothing more than a test mothod for testing CanParse function, which tests if the type can pe parsed as double.
        /// </summary>
        [TestMethod]
        public void TestingForStrings()
        {
            bool test1 = MainWindow.CanParse("1");
            Assert.IsTrue(test1);
        }
    }
}
