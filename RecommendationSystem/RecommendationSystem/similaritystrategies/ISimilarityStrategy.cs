using RecommendationSystem.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationSystem.similaritystrategies
{
    public interface ISimilarityStrategy
    {
        double ComputeSimilarity(User u1, User u2);
    }
}
