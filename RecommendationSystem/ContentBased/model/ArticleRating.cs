using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentBased.model
{
    public class ArticleRating
    {
        public string ArticleNumber { get; set; }
        public double Rating { get; set; }

        public override string ToString()
        {
            return string.Format("[ArticleRating(ArticleNumber: {0}; Rating: {1})]", ArticleNumber, Rating);
        }
    }
}
