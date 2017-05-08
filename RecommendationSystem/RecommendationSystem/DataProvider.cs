using RecommendationSystem.model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationSystem
{
    class DataProvider
    {
        readonly static char DELIMITER = ',';
        readonly static string DATASET_FILEPATH = "assets/userItem.data";

        // Columns of the 'userItem.data' file.
        readonly static int USER_ID = 0;
        readonly static int ARTICLE_ID = 1;
        readonly static int RATING = 2;

        /// <summary>
        /// Retrieves data from the userItem.data file.
        /// Columns of this file consist of 'userId', 'articleId' and the 'rating'.
        /// Ex: user 2 has rated 3.5 the article 105
        /// </summary>
        /// <returns>A dictionary with userId as keys and the user objects as values</returns>
        public static Dictionary<string, User> GetData()
        {
            Dictionary<string, List<ArticleRating>> tempRatings = new Dictionary<string, List<ArticleRating>>();
            using (StreamReader streamReader = new StreamReader(DATASET_FILEPATH))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] dimensions = line.Split(DELIMITER);

                    ArticleRating articleRating = new ArticleRating()
                    {
                        ArticleNumber = dimensions[ARTICLE_ID],
                        Rating = double.Parse(dimensions[RATING], CultureInfo.InvariantCulture)
                    };

                    if (tempRatings.ContainsKey(dimensions[USER_ID]))
                    {
                        // UserID exists in dictionary => add the article and rating to that user
                        tempRatings[dimensions[USER_ID]].Add(articleRating);
                    }
                    else
                    {
                        // UserID doesn't exist in dictionary => create a new entry
                        List<ArticleRating> articleRatings = new List<ArticleRating>();
                        articleRatings.Add(articleRating);

                        tempRatings.Add(dimensions[USER_ID], articleRatings);
                    }                      
                }
            };

            Dictionary<string, User> ratings = new Dictionary<string, User>();
            foreach (var rating in tempRatings)
                ratings.Add(rating.Key, new User(rating.Value.OrderBy(x => x.ArticleNumber).ToArray()));

            return ratings;
        }
    }
}
