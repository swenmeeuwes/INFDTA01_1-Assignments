using RecommendationSystem.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationSystem.similaritystrategies
{
    public class CosineSimilarity : ISimilarityStrategy
    {
        public double ComputeSimilarity(User u1, User u2)
        {
            var articleNumbersU1 = u1.articleRatings.Select(r => r.ArticleNumber);
            var articleNumbersU2 = u2.articleRatings.Select(r => r.ArticleNumber);

            // Find all article numbers that have at least 1 rating => merge article numbers from user 1 and user 2
            // The empty values will get filled in by '0' later
            var mergedArticleNumbers = articleNumbersU1.Union(articleNumbersU2).Distinct().ToArray();

            var sumationDotProduct = 0d;
            var sumationVectorXLength = 0d;
            var sumationVectorYLength = 0d;
            for (int i = 0; i < mergedArticleNumbers.Length; i++)
            {
                var currentArticleNumber = mergedArticleNumbers[i];

                var r1 = u1.articleRatings.FirstOrDefault(r => r.ArticleNumber == currentArticleNumber);
                var r2 = u2.articleRatings.FirstOrDefault(r => r.ArticleNumber == currentArticleNumber);

                // If there are any empty records fill them with 0
                if (r1 == null)
                    r1 = new ArticleRating() {
                        ArticleNumber = currentArticleNumber,
                        Rating = 0d
                    };

                if (r2 == null)
                    r2 = new ArticleRating()
                    {
                        ArticleNumber = currentArticleNumber,
                        Rating = 0d
                    };

                // Calculate this iteration of the cosine formula
                sumationDotProduct += r1.Rating * r2.Rating;
                sumationVectorXLength += Math.Pow(r1.Rating, 2);
                sumationVectorYLength += Math.Pow(r2.Rating, 2);
            }

            return sumationDotProduct / (Math.Sqrt(sumationVectorXLength) * Math.Sqrt(sumationVectorYLength));
        }
    }
}
