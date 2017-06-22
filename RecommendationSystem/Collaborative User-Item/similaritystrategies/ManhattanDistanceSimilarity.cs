using UserItem.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserItem.similaritystrategies
{
    public class ManhattanDistanceSimilarity : ISimilarityStrategy
    {
        public double ComputeSimilarity(User u1, User u2)
        {
            var articleNumbersU1 = u1.articleRatings.Select(r => r.ArticleNumber);
            var articleNumbersU2 = u2.articleRatings.Select(r => r.ArticleNumber);

            // Filter out empty fields
            var articleRatingsU1 = u1.articleRatings.Where(r => articleNumbersU2.Contains(r.ArticleNumber)).ToArray();
            var articleRatingsU2 = u2.articleRatings.Where(r => articleNumbersU1.Contains(r.ArticleNumber)).ToArray();

            var sumation = 0d;
            for (int i = 0; i < articleRatingsU1.Length; i++)
                sumation += Math.Abs(articleRatingsU1[i].Rating - articleRatingsU2[i].Rating);

            return 1 / (sumation + 1);
        }
    }
}
