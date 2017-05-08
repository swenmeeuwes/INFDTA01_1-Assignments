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
            ISimilarityStrategy similarityStrategy = new ManhattanDistanceSimilarity();

            var data = DataProvider.GetData();

            var similarity = similarityStrategy.ComputeSimilarity(data["1"], data["3"]);
            Console.WriteLine(similarity);
        }
    }
}
