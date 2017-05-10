using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecommendationSystem.model;

namespace RecommendationSystem.similaritystrategies
{
    class PearsonCoefficientSimilarity : ISimilarityStrategy
    {
        public double ComputeSimilarity(User u1, User u2)
        {
            var articleNumbersU1 = u1.articleRatings.Select(r => r.ArticleNumber);
            var articleNumbersU2 = u2.articleRatings.Select(r => r.ArticleNumber);

            // Filter out empty fields
            var articleRatingsU1 = u1.articleRatings.Where(r => articleNumbersU2.Contains(r.ArticleNumber)).ToArray();
            var articleRatingsU2 = u2.articleRatings.Where(r => articleNumbersU1.Contains(r.ArticleNumber)).ToArray();

            // Numerator (up)
            // Denominator (down)
            var summationNumerator = 0d;
            var summationNumeratorAverageX = 0d;
            var summationNumeratorAverageY = 0d;

            var summationDenominatorLeft = 0d;
            var summationDenominatorAverageX = 0d;
            var summationDenominatorRight = 0d;
            var summationDenominatorAverageY = 0d;

            // Shortcuts
            var x = articleRatingsU1;
            var y = articleRatingsU2;
            for (int i = 0; i < articleRatingsU1.Length; i++)
            {
                summationNumerator += x[i].Rating * y[i].Rating;
                summationNumeratorAverageX += x[i].Rating;
                summationNumeratorAverageY += y[i].Rating;

                summationDenominatorLeft += Math.Pow(x[i].Rating, 2);
                summationDenominatorAverageX += x[i].Rating;
                summationDenominatorRight += Math.Pow(y[i].Rating, 2);
                summationDenominatorAverageY += y[i].Rating;
            }

            // n = Length - 1 or Length?
            var n = articleRatingsU1.Length;
            return (summationNumerator - (summationNumeratorAverageX * summationNumeratorAverageY) / n)
                / (Math.Sqrt(summationDenominatorLeft - Math.Pow(summationDenominatorAverageX, 2) / n) * Math.Sqrt(summationDenominatorRight - Math.Pow(summationDenominatorAverageY, 2) / n));
        }
    }
}
