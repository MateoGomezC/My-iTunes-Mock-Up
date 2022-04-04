using System;
using System.Collections.Generic;

namespace MyiTunes.Models
{
    public partial class Song
    {
        public Song()
        {
            Purchases = new HashSet<Purchase>();
            Ratings = new HashSet<Rating>();
            Refunds = new HashSet<Refund>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public float Price { get; set; }
        public float? AverageRating { get; set; }
        public int ArtistId { get; set; }

        public virtual Artist Artist { get; set; } = null!;
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Refund> Refunds { get; set; }

        public void CalculateAverage()
        {
            AverageRating = (float?)Ratings.Average(r => r.Rate);
        }

        public void AddNewRating(Rating newRating)
        {
            Ratings.Add(newRating);
            CalculateAverage();
        }
    }
}
