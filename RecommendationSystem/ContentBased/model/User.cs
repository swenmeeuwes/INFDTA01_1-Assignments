using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentBased.model
{
    class User
    {
        public string Id { get; set; }
        public ArticleRating[] articleRatings;

        public User(string id, ArticleRating[] articleRatings)
        {
            this.Id = id;
            this.articleRatings = articleRatings;
        }

        public override string ToString()
        {
            return string.Format("[User(Id: {0}; ArticleRatings: {1})]", Id, articleRatings);
        }
    }
}
