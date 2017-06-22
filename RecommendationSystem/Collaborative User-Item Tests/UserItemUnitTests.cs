using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserItem;
using UserItem.model;
using UserItem.similaritystrategies;
using UserItem.useritem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserItem.Tests
{
    [TestClass()]
    public class UserItemUnitTest
    {
        private readonly static string FILE_PATH = "assets/userItem.data";
        private readonly static char DELIMITER = ',';

        private Dictionary<string, User> data;

        [TestInitialize()]
        public void Initialize()
        {
            data = DataProvider.GetData(FILE_PATH, DELIMITER);
        }

        [TestMethod]
        public void Diagnostic1()
        {
            var user3 = data["3"];
            var user4 = data["4"];

            var similarity = new PearsonCoefficientSimilarity().ComputeSimilarity(user3, user4);

            Assert.IsTrue(Math.Round(similarity) == 1);
        }

        [TestMethod]
        public void Diagnostic2()
        {
            var user7 = data["7"];
            var userPool = data.Where(u => u.Key != user7.Id).Select(u => u.Value).ToArray();

            // Pearson
            var nearestNeighboursPearson = new NearestNeighbour().ComputeNearestNeighbour(user7, userPool, 3, 0.35d, new PearsonCoefficientSimilarity());
            // Nearest neighbours 1-3
            Assert.IsTrue(Math.Abs(0.99124070716193 - nearestNeighboursPearson[0].Similarity) / nearestNeighboursPearson[0].Similarity <= 0.0000001d);
            Assert.IsTrue(Math.Abs(0.924473451641905 - nearestNeighboursPearson[1].Similarity) / nearestNeighboursPearson[1].Similarity <= 0.0000001d);
            Assert.IsTrue(Math.Abs(0.893405147441564 - nearestNeighboursPearson[2].Similarity) / nearestNeighboursPearson[2].Similarity <= 0.0000001d);

            // Cosine
            var nearestNeighboursCosine = new NearestNeighbour().ComputeNearestNeighbour(user7, userPool, 3, 0.35d, new CosineSimilarity());
            // Nearest neighbours 1-3
            Assert.IsTrue(Math.Abs(0.80555004051484 - nearestNeighboursCosine[0].Similarity) / nearestNeighboursCosine[0].Similarity <= 0.0000001d);
            Assert.IsTrue(Math.Abs(0.770024275094105 - nearestNeighboursCosine[1].Similarity) / nearestNeighboursCosine[1].Similarity <= 0.0000001d);
            Assert.IsTrue(Math.Abs(0.734178651201811 - nearestNeighboursCosine[2].Similarity) / nearestNeighboursCosine[2].Similarity <= 0.0000001d);

            // Euclidean
            var nearestNeighboursEuclidean = new NearestNeighbour().ComputeNearestNeighbour(user7, userPool, 3, 0.35d, new EuclideanDistanceSimilarity());
            // Nearest neighbours 1-3
            Assert.IsTrue(Math.Abs(0.4 - nearestNeighboursEuclidean[0].Similarity) / nearestNeighboursEuclidean[0].Similarity <= 0.0000001d);
            Assert.IsTrue(Math.Abs(0.387425886722793 - nearestNeighboursEuclidean[1].Similarity) / nearestNeighboursEuclidean[1].Similarity <= 0.0000001d);
            Assert.IsTrue(Math.Abs(0.356789172325331 - nearestNeighboursEuclidean[2].Similarity) / nearestNeighboursEuclidean[2].Similarity <= 0.0000001d);
        }

        [TestMethod]
        public void Diagnostic3()
        {
            var user7 = data["7"];
            var userPool = data.Where(u => u.Key != user7.Id).Select(u => u.Value).ToArray();

            var nearestNeighboursPearson = new NearestNeighbour().ComputeNearestNeighbour(user7, userPool, 3, 0.35d, new PearsonCoefficientSimilarity());
            var predictions = nearestNeighboursPearson.PredictRatings();

            // Predicted rating for item 101
            Assert.IsTrue(Math.Abs(2.741286897472 - predictions[0].Rating) / predictions[0].Rating <= 0.0000001d);
            Assert.IsTrue(Math.Abs(2.67090274535989 - predictions[1].Rating) / predictions[1].Rating <= 0.0000001d);
            Assert.IsTrue(Math.Abs(3.47705617849087 - predictions[2].Rating) / predictions[2].Rating <= 0.0000001d);
        }
    }
}