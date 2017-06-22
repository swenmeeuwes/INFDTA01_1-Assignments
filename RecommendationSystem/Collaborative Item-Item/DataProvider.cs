using ItemItem.model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemItem
{
    public class DataProvider
    {
        readonly static char DELIMITER = ',';
        readonly static string DATASET_FILEPATH = "assets/userItem.data";

        // Columns of the 'userItem.data' file.
        readonly static int USER_ID = 0;
        readonly static int ARTICLE_ID = 1;
        readonly static int RATING = 2;

        public static Dictionary<string, User> GetData(string filePath, char delimiter)
        {
            Console.WriteLine("Reading '{0}' and spliting on '{1}'.", filePath, delimiter);

            Dictionary<string, List<ArticleRating>> tempRatings = new Dictionary<string, List<ArticleRating>>();
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] dimensions = line.Split(delimiter);

                    ArticleRating articleRating = new ArticleRating()
                    {
                        ArticleNumber = dimensions[ARTICLE_ID],
                        Rating = float.Parse(dimensions[RATING], CultureInfo.InvariantCulture)
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
                ratings.Add(rating.Key, new User(rating.Key, rating.Value.OrderBy(x => x.ArticleNumber).ToArray()));

            Console.WriteLine("Done reading '{0}'!", filePath);

            return ratings;
        }
    }
}
