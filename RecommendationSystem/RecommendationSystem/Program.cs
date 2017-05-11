using RecommendationSystem.similaritystrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = DataProvider.GetData();

            ISimilarityStrategy similarityStrategy = new EuclideanDistanceSimilarity();
            var euclideanDistanceSimilarity = similarityStrategy.ComputeSimilarity(data["1"], data["2"]);
            Console.WriteLine("Euclidean distance similarity: {0}", euclideanDistanceSimilarity);

            similarityStrategy = new ManhattanDistanceSimilarity();
            var manhattanDistanceSimilarity = similarityStrategy.ComputeSimilarity(data["1"], data["2"]);
            Console.WriteLine("Manhattan distance similarity: {0}", manhattanDistanceSimilarity);

            similarityStrategy = new PearsonCoefficientSimilarity();
            var pearsonCoefficientSimilarity = similarityStrategy.ComputeSimilarity(data["1"], data["2"]);
            Console.WriteLine("Pearson coefficient similarity: {0}", pearsonCoefficientSimilarity);

            similarityStrategy = new CosineSimilarity();
            var cosineSimilarity = similarityStrategy.ComputeSimilarity(data["1"], data["2"]);
            Console.WriteLine("Cosine similarity: {0}", cosineSimilarity);
        }
    }
}
