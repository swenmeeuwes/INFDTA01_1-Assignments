using ItemItem.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemItem.controller
{
    public class DeviationMatrixFactory
    {
        private static DeviationMatrixFactory PInstance { get; set; }
        public static DeviationMatrixFactory Instance
        {
            get
            {
                if (PInstance == null)
                    PInstance = new DeviationMatrixFactory();
                return PInstance;
            }
        }

        private DeviationMatrixFactory() { }

        public DeviationMatrix Create(Dictionary<string, User> from)
        {
            Console.WriteLine("Constructing deviation matrix...");

            var distinctArticles = from.SelectMany(n => n.Value.articleRatings).Select(r => r.ArticleNumber).Distinct();
            var workMatrix = new DeviationMatrix(distinctArticles.ToArray());

            var progressCounter = -1;

            // Per user
            foreach (var item in from)
            {
                progressCounter++;
                Console.Write("\r{0}%   ", Math.Round(((float)progressCounter / from.Count) * 100f));
                var user = item.Value;
                if (user.articleRatings.Length < 2)
                    continue;

                // Compare ratings from the same user against eachother
                foreach (var aRating in user.articleRatings)
                {
                    foreach (var bRating in user.articleRatings)
                    {
                        var deviation = new Deviation(1, aRating.Rating - bRating.Rating);
                        workMatrix.Add(aRating.ArticleNumber, bRating.ArticleNumber, deviation);
                    }
                }
            }

            Console.WriteLine("Done constructing the deviation matrix!");

            return workMatrix;
        }
    }
}
