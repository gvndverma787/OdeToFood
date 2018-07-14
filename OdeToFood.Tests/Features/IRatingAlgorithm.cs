using System.Collections.Generic;
using System.Linq;
using OdeToFood.Models;

namespace OdeToFood.Tests.Features
{
    public interface IRatingAlgorithm
    {
        RatingResult Compute(List<RestaurantReview> reviews);
    }
    public class SimpleRatingAlgorithm : IRatingAlgorithm
    {
        public RatingResult Compute(List<RestaurantReview> reviews)
        {
            var result = new RatingResult();
            result.Rating = (int)reviews.Average(r => r.Rating);
            return result;
        }
    }
    public class WeightedRatingAlgorithm : IRatingAlgorithm
    {
        public RatingResult Compute(List<RestaurantReview> reviews)
        {
            var result = new RatingResult();
            int counter = 0;
            int total = 0;
            for (int i = 0; i < reviews.Count(); i++)
            {
                if (i < reviews.Count() / 2)
                {
                    counter += 2;
                    total += reviews[i].Rating * 2;
                }
                else
                {
                    counter += 1;
                    total += reviews[i].Rating;
                }
            }
            result.Rating = total / counter;
            return result;
        }
    }
}