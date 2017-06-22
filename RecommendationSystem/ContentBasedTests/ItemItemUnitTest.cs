using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ItemItem.model;
using ItemItem;
using ItemItem.controller;

namespace ContentBasedTests
{
    [TestClass]
    public class ItemItemUnitTest
    {
        private readonly static string FILE_PATH = "assets/userItem.data";
        private readonly static char DELIMITER = ',';

        private DataWrapper dataWrapper;
        private Dictionary<string, User> data;
        private DeviationMatrix deviationMatrix;

        [TestInitialize()]
        public void Initialize()
        {
            data = DataProvider.GetData(FILE_PATH, DELIMITER);
            deviationMatrix = DeviationMatrixFactory.Instance.Create(data);

            dataWrapper = new DataWrapper(data, deviationMatrix);
        }

        [TestMethod]
        public void TestPredictionAlgorithm()
        {
            // UserID, targetItem, expectedValue
            Tuple<string, string, double>[] testCases = new Tuple<string, string, double>[] {
                new Tuple<string, string, double>("7", "101", 2.60714285714286),
                new Tuple<string, string, double>("7", "103", 2.16666666666667),
                new Tuple<string, string, double>("7", "106", 3.17647058823529),

                new Tuple<string, string, double>("3", "103", 2.16666666666667),
                new Tuple<string, string, double>("3", "105", 2.33333333333333)
            };

            // Execute each test case
            foreach (var testCase in testCases)
            {
                var user = data[testCase.Item1];
                var prediction = deviationMatrix.Predict(user, testCase.Item2);
                var passed = Math.Abs(testCase.Item3 - prediction.Rating) / prediction.Rating <= 0.0000001d;
                Assert.IsTrue(passed);
            }
        }

        [TestMethod]
        public void TestPredictionAlgorithmAfterChange()
        {
            // Add the new rating to the system
            var userThatRated = data["3"];
            dataWrapper.AddRatings(userThatRated, new ArticleRating[] {
                new ArticleRating() { ArticleNumber = "105", Rating = 4.0d }
            });

            // UserID, targetItem, expectedValue
            Tuple<string, string, double>[] testCases = new Tuple<string, string, double>[] {
                new Tuple<string, string, double>("7", "101", 2.4),
                new Tuple<string, string, double>("7", "103", 2.16666666666667),
                new Tuple<string, string, double>("7", "106", 3.05555555555556)               
            };

            // Execute each test case
            foreach (var testCase in testCases)
            {
                var user = data[testCase.Item1];
                var prediction = deviationMatrix.Predict(user, testCase.Item2);
                var passed = Math.Abs(testCase.Item3 - prediction.Rating) / prediction.Rating <= 0.0000001d;
                Assert.IsTrue(passed);
            }
        }
    }
}
