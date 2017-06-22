using UserItem.model;
using UserItem.similaritystrategies;
using UserItem.useritem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserItem
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = DataProvider.GetData("assets/u.data", '\t');

            var targetUserId = "4";
            var targetUser = data[targetUserId];
            var userPool = data.Where(u => u.Key != targetUserId).Select(u => u.Value).ToArray();
            var nearestNeighbours = new NearestNeighbour().ComputeNearestNeighbour(targetUser, userPool, 12, 0.35d, new CosineSimilarity());

            // Prediction ratings test from slides lesson 2 - put in a unit test maybe?
            var testNeighbours = new User[] {
                new User("1", new ArticleRating[] {
                    new ArticleRating() { ArticleNumber = "1", Rating = 4.5d }
                }) { Similarity = 0.5 },
                new User("2", new ArticleRating[] {
                    new ArticleRating() { ArticleNumber = "1", Rating = 5d }
                }) { Similarity = 0.7 },
                new User("3", new ArticleRating[] {
                    new ArticleRating() { ArticleNumber = "1", Rating = 3.5d }
                }) { Similarity = 0.8 }
            };
            var r = testNeighbours.PredictRatings(); // Should be 4.275 according to slide
            // -----

            var predictions = nearestNeighbours.PredictRatings();

            Console.WriteLine("User {0} nearest neighbours are:", targetUser.Id);
            Array.ForEach(nearestNeighbours, p => Console.WriteLine(p));

            Console.WriteLine("User {0} has the following predictions:", targetUser.Id);
            Array.ForEach(predictions.OrderBy(p => p.ArticleNumber).ToArray(), p => Console.WriteLine(p));

            //ISimilarityStrategy similarityStrategy = new EuclideanDistanceSimilarity();
            //var euclideanDistanceSimilarity = similarityStrategy.ComputeSimilarity(data["1"], data["2"]);
            //Console.WriteLine("Euclidean distance similarity: {0}", euclideanDistanceSimilarity);

            //similarityStrategy = new ManhattanDistanceSimilarity();
            //var manhattanDistanceSimilarity = similarityStrategy.ComputeSimilarity(data["1"], data["2"]);
            //Console.WriteLine("Manhattan distance similarity: {0}", manhattanDistanceSimilarity);

            //similarityStrategy = new PearsonCoefficientSimilarity();
            //var pearsonCoefficientSimilarity = similarityStrategy.ComputeSimilarity(data["1"], data["2"]);
            //Console.WriteLine("Pearson coefficient similarity: {0}", pearsonCoefficientSimilarity);

            //similarityStrategy = new CosineSimilarity();
            //var cosineSimilarity = similarityStrategy.ComputeSimilarity(data["1"], data["2"]);
            //Console.WriteLine("Cosine similarity: {0}", cosineSimilarity);
        }
    }
}
