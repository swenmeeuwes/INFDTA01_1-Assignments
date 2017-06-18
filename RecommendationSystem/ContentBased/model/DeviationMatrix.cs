using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentBased.model
{
    class DeviationMatrix
    {
        private Deviation[,] matrix;
        private string[] labels;

        public DeviationMatrix(string[] labels)
        {
            matrix = new Deviation[labels.Length, labels.Length];
            this.labels = labels;
        }

        public void Set(string xLabel, string yLabel, Deviation deviation)
        {
            var xIndex = Array.FindIndex(labels, l => l == xLabel);
            var yIndex = Array.FindIndex(labels, l => l == yLabel);

            matrix[xIndex, yIndex] = deviation;
        }

        public void Add(string xLabel, string yLabel, Deviation deviation)
        {
            var xIndex = Array.FindIndex(labels, l => l == xLabel);
            var yIndex = Array.FindIndex(labels, l => l == yLabel);

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
            var xIndex = Array.FindIndex(labels, l => l == xLabel);
            var yIndex = Array.FindIndex(labels, l => l == yLabel);

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
                numerator += (rating.Rating + deviation.Average) * deviation.Readings;
                denominator += deviation.Readings;
            }

            return new ArticleRating() {
                ArticleNumber = targetRatingNumber,
                Rating = numerator / denominator
            };
        }
    }
}
