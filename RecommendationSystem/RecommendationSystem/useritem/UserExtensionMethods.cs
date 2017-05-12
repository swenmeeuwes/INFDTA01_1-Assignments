using RecommendationSystem.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationSystem.useritem
{
    static class UserExtensionMethods
    {
        public static ArticleRating[] PredictRatings(this User[] users)
        {
            var predictions = new List<ArticleRating>();

            var availableRatings = users.SelectMany(u => u.articleRatings).Select(r => r.ArticleNumber).Distinct().ToArray();
            for (int i = 0; i < availableRatings.Length; i++)
            {
                var numeratorSummation = 0d;
                for (int j = 0; j < users.Length; j++)
                {
                    var userRating = users[j].articleRatings.SingleOrDefault(r => r.ArticleNumber == availableRatings[i]);
                    if (userRating == null)
                        continue;

                    numeratorSummation += users[j].Similarity * userRating.Rating;
                }
                var predictionRating = numeratorSummation / users.Sum(u => u.Similarity);
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
