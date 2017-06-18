using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentBased.model
{
    public class DeviationMatrix
    {
        private Deviation[,] matrix;

        private Dictionary<string, int> labelLookupDictionary; // For optimisation

        public DeviationMatrix(string[] labels)
        {
            matrix = new Deviation[labels.Length, labels.Length];

            labelLookupDictionary = new Dictionary<string, int>();
            for (int i = 0; i < labels.Length; i++)
            {
                labelLookupDictionary.Add(labels[i], i);
            }
        }

        public void Set(string xLabel, string yLabel, Deviation deviation)
        {
            var xIndex = labelLookupDictionary[xLabel];
            var yIndex = labelLookupDictionary[yLabel];

            matrix[xIndex, yIndex] = deviation;
        }

        public void Add(string xLabel, string yLabel, Deviation deviation)
        {
            var xIndex = labelLookupDictionary[xLabel];
            var yIndex = labelLookupDictionary[yLabel];

            var current = matrix[xIndex, yIndex];
            if (current != null)
            {
                var mergedDeviation = new Deviation(current.Readings + deviation.Readings, current.Value + deviation.Value);
                matrix[xIndex, yIndex] = mergedDeviation;
            }
            else
            {
                matrix[xIndex, yIndex] = deviation;
            }
        }

        public Deviation Get(string xLabel, string yLabel)
        {
            var xIndex = labelLookupDictionary[xLabel];
            var yIndex = labelLookupDictionary[yLabel];

            return matrix[xIndex, yIndex];
        }

        public ArticleRating Predict(ArticleRating baseRating, string targetRatingNumber)
        {            
            var deviation = Get(baseRating.ArticleNumber, targetRatingNumber);
            return new ArticleRating() {
                ArticleNumber = targetRatingNumber,
                Rating = baseRating.Rating + deviation.Average
            };
        }

        public ArticleRating Predict(User user, string targetRatingNumber)
        {
            var userRatings = user.articleRatings;

            var numerator = 0d;
            var denominator = 0d;
            for (int i = 0; i < userRatings.Length; i++)
            {
                var rating = userRatings[i];
                var deviation = Get(targetRatingNumber, rating.ArticleNumber);

                if (deviation == null)
                    continue;

                numerator += (rating.Rating + deviation.Average) * deviation.Readings;
                denominator += deviation.Readings;
            }

            return new ArticleRating() {
                ArticleNumber = targetRatingNumber,
                Rating = numerator / denominator
            };
        }

        public ArticleRating[] PredictTop(User user, int amountOfItems)
        {
            var recommandations = new List<ArticleRating>();        
            var userRatings = user.articleRatings;
            var articlesToPredict = labelLookupDictionary.Where(x => !user.articleRatings.Select(r => r.ArticleNumber).Contains(x.Key)).ToArray();

            for (int i = 0; i < articlesToPredict.Length; i++)
            {
                var targetRatingNumber = articlesToPredict[i].Key;

                var numerator = 0d;
                var denominator = 0d;
                for (int j = 0; j < userRatings.Length; j++)
                {
                    var rating = userRatings[j];
                    var deviation = Get(targetRatingNumber, rating.ArticleNumber);

                    if (deviation == null)
                        continue;

                    numerator += (rating.Rating + deviation.Average) * deviation.Readings;
                    denominator += deviation.Readings;
                }

                recommandations.Add(new ArticleRating()
                {
                    ArticleNumber = targetRatingNumber,
                    Rating = numerator / denominator
                });
            }
            return recommandations.OrderByDescending(x => x.Rating).Take(amountOfItems).ToArray();
        }
    }
}
