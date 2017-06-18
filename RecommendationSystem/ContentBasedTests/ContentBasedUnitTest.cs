using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ContentBased.model;
using ContentBased;
using ContentBased.controller;

namespace ContentBasedTests
{
    [TestClass]
    public class ContentBasedUnitTest
    {
        private readonly static string FILE_PATH = "assets/userItem.data";
        private readonly static char DELIMITER = ',';

        private Dictionary<string, User> data;
        private DeviationMatrix deviationMatrix;

        [TestInitialize()]
        public void Initialize()
        {
            data = DataProvider.GetData(FILE_PATH, DELIMITER);
            deviationMatrix = DeviationMatrixFactory.Instance.Create(data);
        }

        [TestMethod]
        public void TestContentBased()
        {
            var user7 = data["7"];
            var prediction = deviationMatrix.Predict(user7, "101");
            var doublesAreEqual = Math.Abs(2.60714285714286d - prediction.Rating) / prediction.Rating <= 0.0000001d;
            Assert.IsTrue(doublesAreEqual);
        }
    }
}
