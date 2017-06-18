using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentBased.model
{
    public class ContentBasedDataWrapper
    {
        public Dictionary<string, User> UserData { get; private set; }
        public DeviationMatrix DeviationMatrix { get; private set; }

        public ContentBasedDataWrapper(Dictionary<string, User> userData, DeviationMatrix deviationMatrix)
        {
            UserData = userData;
            DeviationMatrix = deviationMatrix;
        }

        public void AddRatings(User user, ArticleRating[] ratings)
        {
            var newUserRatings = new ArticleRating[user.articleRatings.Length + ratings.Length];

            // Copy existing ratings
            for (int i = 0; i < user.articleRatings.Length; i++)
            {
                newUserRatings[i] = user.articleRatings[i];
            }
            // Add new onces
            for (int i = user.articleRatings.Length; i < newUserRatings.Length; i++)
            {
                var rating = ratings[i - user.articleRatings.Length];
                newUserRatings[i] = rating;

                // Update deviation matrix
                var distinctArticles = UserData.SelectMany(n => n.Value.articleRatings).Select(r => r.ArticleNumber).Distinct();

                // Compare ratings from the same user against the newly add rating
                foreach (var aRating in user.articleRatings)
                {                    
                    var deviation = new Deviation(1, aRating.Rating - rating.Rating);
                    DeviationMatrix.Add(aRating.ArticleNumber, rating.ArticleNumber, deviation);                    
                }
            }
        }
    }
}
