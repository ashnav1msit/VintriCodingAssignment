using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using VintriCodingAssignment.Models;

namespace VintriCodingAssignment.Helper
{
    public static class DatabaseReadWrite
    {
        public static string FILE_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database.json");

        public static void AddReview(BeerReview rating)
        {           
            var jsonData = System.IO.File.ReadAllText(FILE_PATH);

            var Ratings = JsonConvert.DeserializeObject<List<BeerReview>>(jsonData)
                                  ?? new List<BeerReview>();
            Ratings.Add(rating);
            jsonData = JsonConvert.SerializeObject(Ratings);
            System.IO.File.WriteAllText(FILE_PATH, jsonData);

        }

        public static List<BeerReview> FetchAllReviews()
        {
            var jsonData = System.IO.File.ReadAllText(FILE_PATH);

            var Ratings = JsonConvert.DeserializeObject<List<BeerReview>>(jsonData)
                                  ?? new List<BeerReview>();

            return Ratings;
        }
    }
}