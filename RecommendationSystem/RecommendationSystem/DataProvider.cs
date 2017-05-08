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
        /// <returns>A dictionary with the userId as the key and a dictionary as value, containing the articleId as key and rating as value</returns>
        public static Dictionary<string, Dictionary<string, double>> GetData()
        {
            Dictionary<string, Dictionary<string, double>> ratings = new Dictionary<string, Dictionary<string, double>>();
            using (StreamReader streamReader = new StreamReader(DATASET_FILEPATH))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] dimensions = line.Split(DELIMITER);
                    if(ratings.ContainsKey(dimensions[USER_ID]))
                    {
                        // UserID exists in dictionary => add the article and rating to that user
                        ratings[dimensions[USER_ID]].Add(dimensions[ARTICLE_ID], double.Parse(dimensions[RATING], CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        // UserID doesn't exist in dictionary => create a new entry
                        Dictionary<string, double> rating = new Dictionary<string, double>();
                        rating.Add(dimensions[ARTICLE_ID], double.Parse(dimensions[RATING], CultureInfo.InvariantCulture));

                        ratings.Add(dimensions[USER_ID], rating);
                    }                      
                }
            };
            return ratings;
        }
    }
}
