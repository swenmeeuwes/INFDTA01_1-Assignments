using UserItem.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserItem.useritem
{
    public static class UserExtensionMethods
    {
        public static ArticleRating[] PredictRatings(this User[] users)
        {
            var predictions = new List<ArticleRating>();

            var availableRatings = users.SelectMany(u => u.articleRatings).Select(r => r.ArticleNumber).Distinct().ToArray();
            for (int i = 0; i < availableRatings.Length; i++)
            {
                // Can be optimized by implementing it single-pass
                var numeratorSummation = users.Where(u => u.articleRatings.Any(r => r.ArticleNumber == availableRatings[i]))
                                                .Sum(u => u.Similarity * u.articleRatings.First(r => r.ArticleNumber == availableRatings[i]).Rating);
                var denominatorSummation = users.Where(u => u.articleRatings.Any(r => r.ArticleNumber == availableRatings[i]))
                                                .Sum(u => u.Similarity);
                var predictionRating = numeratorSummation / denominatorSummation;
                predictions.Add(new ArticleRating()
                {
                    ArticleNumber = availableRatings[i],
                    Rating = predictionRating
                });
            }

            return predictions.ToArray();
        }
    }
}
