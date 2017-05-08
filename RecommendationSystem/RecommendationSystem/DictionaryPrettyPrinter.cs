using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationSystem
{
    class DictionaryPrettyPrinter
    {
        // TEMP
        public static void PrettyPrint(Dictionary<string, Dictionary<string, double>> userRatings)
        {
            foreach (var userRating in userRatings.OrderBy(r => r.Key))
            {
                Console.WriteLine("User {0} has rated the following articles:", userRating.Key);
                var articleRatings = userRating.Value.OrderBy(articleRating => articleRating.Key);
                foreach (var articleRating in articleRatings)
                {
                    Console.WriteLine("Article {0} with a rating of {1}", articleRating.Key, articleRating.Value);
                }
            }
        }
    }
}
