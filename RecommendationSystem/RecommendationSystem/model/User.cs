﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationSystem.model
{
    class User
    {
        public ArticleRating[] articleRatings;

        public User(ArticleRating[] articleRatings)
        {
            this.articleRatings = articleRatings;
        }
    }
}
