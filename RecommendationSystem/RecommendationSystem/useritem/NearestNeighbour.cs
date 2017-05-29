using RecommendationSystem.model;
using RecommendationSystem.similaritystrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationSystem.useritem
{
    class NearestNeighbour
    {
        // TODO: check for additional items with respect to the target user
        /// <summary>
        /// Gets an array of the top users most similar to the target user, sorted from high to low similarity
        /// </summary>
        /// <param name="targetUser">The target user that will be compared to the other users</param>
        /// <param name="userPool">The user pool that the target user will be compared against</param>
        /// <param name="maxNeighbours">The amount of neighbours the algorithm should try to find</param>
        /// <param name="similarityThreshold">The start threshold for similarity</param>
        /// <param name="similarityStrategy">The strategy used to compose the similarity between users</param>
        /// <returns></returns>
        public User[] ComputeNearestNeighbour(User targetUser, User[] userPool, int maxNeighbours, double similarityThreshold, ISimilarityStrategy similarityStrategy)
        {
            // Optional: Check if the target user exists in the user pool
            if (userPool.Contains(targetUser))
                throw new Exception("[UserItem NearestNeighbour] Target user exists in the userpool.");

            var nearestNeighbours = new User[maxNeighbours];
            for (int i = 0; i < userPool.Length; i++)
            {
                var similarity = similarityStrategy.ComputeSimilarity(targetUser, userPool[i]);
                userPool[i].Similarity = similarity;

                // If the similarity between the user and target is lower than the threshold, skip to the next iteration
                if (similarity < similarityThreshold)
                    continue;

                // If the nearest neighbour list is not yet full, insert the user
                var nearestNeighboursFound = nearestNeighbours.Count(u => u != null);
                if(nearestNeighboursFound < nearestNeighbours.Length)
                {
                    var firstEmptySpot = Array.FindIndex(nearestNeighbours, n => n == null);
                    nearestNeighbours[firstEmptySpot] = userPool[i];
                }
                // The list is full, replace the user with the lowest similarity
                else
                {
                    // Could be made a 'single-pass'
                    var lowestSimilarity = nearestNeighbours.Min(u => u.Similarity);
                    var lowestSimilarityIndex = Array.FindIndex(nearestNeighbours, n => n.Similarity == lowestSimilarity);
                    nearestNeighbours[lowestSimilarityIndex] = userPool[i];

                    // Raise threshold to lowest similarity in the list
                    similarityThreshold = nearestNeighbours.Min(u => u.Similarity);
                }
            }

            return nearestNeighbours.Where(u => u != null).OrderByDescending(u => u.Similarity).ToArray();
        }
    }
}
